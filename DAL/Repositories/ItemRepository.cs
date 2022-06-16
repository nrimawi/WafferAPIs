

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
                Item item = _mapper.Map<Item>(ItemData);

                item.Status = true;
                item.SellerId = ItemData.SellerId;
                item.SubCategoryId = ItemData.SubCategoryId;
                item.CreatedDate = DateTime.Now;
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
                var activeItems = await _appDbContext.Items.Where(Item => Item.Status == true).ToListAsync();
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
                var Items = await _appDbContext.Items.Where(s => s.SubCategoryId == subCategoryId && s.Status == true).ToListAsync();

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

                var Items = await _appDbContext.Items.Where(i => i.SubCategoryId == subCategoryId && i.Brand.ToLower() == brand.ToLower() && i.Status == true).ToListAsync();

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

    }
}
