

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WafferAPIs.DAL.Entities;
using WafferAPIs.Dbcontext;
using WafferAPIs.Models.Dtos;

namespace WafferAPIs.DAL.Repositories
{

    public interface IItemRepository : IDisposable
    {
        Task<ItemData> CreateItem(ItemData ItemData);
        Task<List<ItemData>> GetItems();
        Task<ItemData> GetItemById(Guid id);
        Task DeleteItem(Guid id);
        Task<List<ItemData>> GetItemsBySeller(Guid sellerId);
        Task<List<ItemData>> GetItemsBySubCategory(Guid subCategoryId);
        Task<List<ItemData>> GetItemsByBrandAndSubCategory(Guid subCategoryId, string brand);
        Task<List<ItemData>> GetPendingItems();
        Task ApprovePendingItem(Guid ItemId);

    }


    public class ItemRepository : IItemRepository
    {
        #region Inject Services
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        #endregion

        public ItemRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }
        public async Task<ItemData> CreateItem(ItemData ItemData)
        {
            if (ItemData == null)
                throw new ArgumentNullException(nameof(ItemData));

            try
            {

                var subCategory = await _appDbContext.SubCategories.Where(s => s.Id == ItemData.SubCategoryId && s.Status == true).FirstOrDefaultAsync();
                if (subCategory == null)
                    throw new Exception("Cant find Subcategory at item inseration");


                Item item = _mapper.Map<Item>(ItemData);

                item.Status = true;
                item.SellerId = ItemData.SellerId;
                item.SubCategoryId = ItemData.SubCategoryId;
                item.CreatedDate = DateTime.Now;
                if (subCategory.AveragePrice * 2 < ItemData.Price || subCategory.AveragePrice / 2 > ItemData.Price)
                    item.pending = true;

                else
                {
                    item.pending = false;

                    #region Update Subcategory Average Price
                    var ItemsCountInSubCat = await _appDbContext.Items.Where(item => item.Status == true && item.pending != true && item.SubCategoryId == ItemData.SubCategoryId).CountAsync();

                    subCategory.AveragePrice = (int)(ItemsCountInSubCat != 0 ? ((_appDbContext.SubCategories.Count() * subCategory.AveragePrice) + ItemData.Price) / (ItemsCountInSubCat + 1) : (subCategory.AveragePrice + ItemData.Price) / 2);
                    _appDbContext.SubCategories.Update(subCategory);

                    #endregion
                }


                _appDbContext.Items.Add(item);
                await _appDbContext.SaveChangesAsync();
                return _mapper.Map<ItemData>(item);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<ItemData>> GetItems()
        {
            try
            {
                var activeItems = await _appDbContext.Items.Where(i => i.Status == true && i.pending != true).ToListAsync();
                return _mapper.Map<List<ItemData>>(activeItems);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ItemData> GetItemById(Guid id)
        {

            try
            {


                Item item = await _appDbContext.Items.Where(s => s.Id == id && s.Status == true).FirstOrDefaultAsync();

                if (item == null)
                {
                    throw new NullReferenceException("Item with id=" + id + " is not found");
                }
                return _mapper.Map<ItemData>(item);
            }
            catch
            {
                throw;
            }
        }


        public async Task DeleteItem(Guid id)
        {

            try
            {

                Item Item = await _appDbContext.Items.FindAsync(id);

                if (Item == null || Item.Status == false)
                {
                    throw new NullReferenceException("Item with id=" + id + " is not found");
                }
                Item.Status = false;
                _appDbContext.Update(Item);
                await _appDbContext.SaveChangesAsync();

            }
            catch
            {
                throw;
            }
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        public async Task<List<ItemData>> GetItemsBySeller(Guid sellerId)
        {
            try
            {
                var Items = await _appDbContext.Items.Where(s => s.SellerId == sellerId && s.Status == true).ToListAsync();

                if (Items == null)
                {
                    throw new NullReferenceException("Seller has no items");
                }
                return _mapper.Map<List<ItemData>>(Items);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ItemData>> GetItemsBySubCategory(Guid subCategoryId)
        {
            try
            {
                var Items = await _appDbContext.Items.Where(i => i.SubCategoryId == subCategoryId && i.Status == true && i.pending != true).ToListAsync();

                if (Items == null)
                {
                    throw new NullReferenceException("Error in subcategory id");
                }
                return _mapper.Map<List<ItemData>>(Items);
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<ItemData>> GetItemsByBrandAndSubCategory(Guid subCategoryId, string brand)
        {
            try
            {

                var Items = await _appDbContext.Items.Where(i => i.SubCategoryId == subCategoryId && i.Brand.ToLower() == brand.ToLower() && i.Status == true && i.pending != true).ToListAsync();

                if (Items == null)
                {
                    throw new NullReferenceException("Error retrieving items");
                }
                return _mapper.Map<List<ItemData>>(Items);
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<ItemData>> GetPendingItems()
        {
            try
            {
                var items = (await _appDbContext.Items.Where(Item => Item.Status == true && Item.pending == true).ToListAsync());
                if (items == null)
                {
                    throw new NullReferenceException("Error retrieving items");
                }
                return _mapper.Map<List<ItemData>>(items);
            }
            catch { throw; }
        }

        public async Task ApprovePendingItem(Guid ItemId)
        {
            try
            {
                var item = (await _appDbContext.Items.Where(Item => Item.Status == true && Item.pending == true && Item.Id == ItemId).FirstOrDefaultAsync());
                if (item == null)
                {
                    throw new NullReferenceException("Can not Find Item");
                }

                _appDbContext.Items.Update(item);

                #region Update Subcategory Average Price

                var subCategory = await _appDbContext.SubCategories.Where(s => s.Id == item.SubCategoryId && s.Status == true).FirstOrDefaultAsync();

                if (subCategory == null)
                    throw new Exception("Cant find Subcategory at item inseration");

                var ItemsCountInSubCat = await _appDbContext.Items.Where(i => i.Status == true && i.pending != true && i.SubCategoryId == item.SubCategoryId).CountAsync();

                subCategory.AveragePrice = (int)(ItemsCountInSubCat != 0 ? ((_appDbContext.SubCategories.Count() * subCategory.AveragePrice) + item.Price) / (ItemsCountInSubCat + 1) : (subCategory.AveragePrice + item.Price) / 2);
                #endregion

                await _appDbContext.SaveChangesAsync();
            }
            catch { throw; }
        }
    }
}
