

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
        Task<List<ItemData>> GetItems(string? searchFor, string? sortBy,int count);
        Task<ItemData> GetItemById(Guid id);
        Task DeleteItem(Guid id);
        Task<List<ItemData>> GetItemsBySeller(Guid sellerId);
        Task<List<ItemData>> GetItemsBySubCategory(Guid subCategoryId, string? searchFor, string? sortBy);
        Task<List<ItemData>> GetItemsByBrandAndSubCategory(Guid subCategoryId, string brand);
        Task<List<ItemData>> GetPendingItems();
        Task ApprovePendingItem(Guid ItemId);
        Task RejectPendingItem(Guid ItemId);


    }


    public class ItemRepository : IItemRepository
    {
        #region Inject Services
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
       private readonly ISubCategoryRepository _subCategoryRepository;    

        #endregion

        public ItemRepository(AppDbContext appDbContext, IMapper mapper, ISubCategoryRepository subCategoryRepository)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _subCategoryRepository = subCategoryRepository; 
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
                if (subCategory.AveragePrice * 3 < ItemData.Price || subCategory.AveragePrice / 3 > ItemData.Price)
                    item.pending = true;

                else
                {
                    item.pending = false;

                    #region Update Subcategory Average Price
                    var ItemsCountInSubCat = await _appDbContext.Items.CountAsync(i => i.Status == true && i.pending != true && i.SubCategoryId == item.SubCategoryId);

                    subCategory.AveragePrice = (int)(ItemsCountInSubCat != 0 ? ((ItemsCountInSubCat * subCategory.AveragePrice) + ItemData.Price) / (ItemsCountInSubCat + 1) : (subCategory.AveragePrice + ItemData.Price) / 2);
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
        public async Task<List<ItemData>> GetItems(string? searchFor, string? sortBy,int count)
        {
            try
            {
                var itemsFromQuery = _mapper.Map<List<ItemData>>(await _appDbContext.Items.Where(i => i.Status == true && i.pending != true).ToListAsync());
                var subcategories= _mapper.Map<List<SubCategoryData>>(await _subCategoryRepository.GetSubCategories());
                #region search
                if (!string.IsNullOrEmpty(searchFor))
                {
                    


                    //itemsFromQuery = itemsFromQuery.Where(i => (i.Name != null && searchFor.ToLower().Contains(i.Name.ToLower())) || (i.Brand != null && searchFor.ToLower().ToLower().Contains(i.Brand.ToLower()) )||(i.Color != null && searchFor.ToLower().ToLower().Contains(i.Color.ToLower()))  ||(i.Description != null && searchFor.ToLower().ToLower().Contains(i.Description.ToLower())) ||(i.OtherFeatures!=null&& searchFor.ToLower().ToLower().Contains(i.OtherFeatures.ToLower()))).ToList();
                    itemsFromQuery = itemsFromQuery.Where(i => searchFor.ToLower().Contains(getSubcategoryName(i.SubCategoryId,subcategories))|| getSubcategoryName(i.SubCategoryId, subcategories).Contains(searchFor.ToLower()) || (!string.IsNullOrEmpty(i.Name) && searchFor.ToLower().Contains(i.Name.ToLower())) || (!string.IsNullOrEmpty(i.Brand) && searchFor.ToLower().Contains(i.Brand.ToLower())) || (!string.IsNullOrEmpty(i.Color) && searchFor.ToLower().Contains(i.Color.ToLower())) || (!string.IsNullOrEmpty(i.Description) && searchFor.ToLower().Contains(i.Description.ToLower())) || (!string.IsNullOrEmpty(i.OtherFeatures) && searchFor.ToLower().Contains(i.OtherFeatures.ToLower()))).ToList();

                    var searchKeyWords = searchFor.Trim().Split(" ");
                    foreach (var item in itemsFromQuery)
                    {
                        item.SortPriority = 0;

                        foreach (var keyWord in searchKeyWords)
                        {
                            if (!string.IsNullOrEmpty(item.Name)&&item.Name.ToLower().Contains(keyWord.ToLower()) )
                            
                                item.SortPriority++;
                            

                            if (!string.IsNullOrEmpty(item.Brand)&&item.Brand.ToLower().Contains(keyWord.ToLower()))
                                item.SortPriority = item.SortPriority + 2;


                            if (!string.IsNullOrEmpty(item.Description)&&item.Description.ToLower().Contains(keyWord.ToLower()))
                                item.SortPriority = item.SortPriority + 2;


                            if (!string.IsNullOrEmpty(item.Color)&&item.Color.ToLower().Contains(keyWord.ToLower()))
                                item.SortPriority= item.SortPriority+3;

                            if (!string.IsNullOrEmpty(item.OtherFeatures)&&item.OtherFeatures.ToLower().Contains(keyWord.ToLower()))
                                item.SortPriority++;
                        }

                    }
                }
                #endregion

                #region sort
                switch (sortBy?.ToLower())
                {
                    case "name_desc": itemsFromQuery = itemsFromQuery.OrderByDescending(i => i.SortPriority).ThenByDescending(i => i.Name).ToList(); break;
                    case "name_asc": itemsFromQuery = itemsFromQuery.OrderByDescending(i => i.SortPriority).ThenBy(i => i.Name).ToList(); break;
                    case "price_desc": itemsFromQuery = itemsFromQuery.OrderByDescending(i => i.SortPriority).ThenByDescending(i => i.Price).ToList(); break;
                    case "price_asc": itemsFromQuery = itemsFromQuery.OrderByDescending(i => i.SortPriority).ThenBy(i => i.Price).ToList(); break;
                    case "date_desc": itemsFromQuery = itemsFromQuery.OrderByDescending(i => i.SortPriority).ThenByDescending(i => i.CreatedDate).ToList(); break;
                    case "date_asc": itemsFromQuery = itemsFromQuery.OrderByDescending(i => i.SortPriority).ThenBy(i => i.CreatedDate).ToList(); break;
                    default: itemsFromQuery = itemsFromQuery.OrderByDescending(i => i.SortPriority).ThenBy(item => new Random().Next()).ToList(); break;
                }


                #endregion


                return itemsFromQuery.GetRange(0,count);
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

        public async Task<List<ItemData>> GetItemsBySubCategory(Guid subCategoryId, string? searchFor, string? sortBy)
        {
            try
            {
                var itemsFromQuery = _mapper.Map<List<ItemData>>(await _appDbContext.Items.Where(i => i.SubCategoryId == subCategoryId && i.Status == true && i.pending != true).ToListAsync());

                #region search
                if (!string.IsNullOrEmpty(searchFor))
                {

                    itemsFromQuery = itemsFromQuery.Where(i => searchFor.ToLower().Contains(i.Name.ToLower()) || searchFor.ToLower().ToLower().Contains(i.Brand.ToLower()) || searchFor.ToLower().ToLower().Contains(i.Color.ToLower())).ToList();
                    var searchKeyWords = searchFor.Split(" ");
                    foreach (var item in itemsFromQuery)
                    {
                        item.SortPriority = 0;

                        foreach (var keyWord in searchKeyWords)
                        {
                            if (keyWord.ToLower().Contains(item.Name.ToLower()) || keyWord.ToLower().ToLower().Contains(item.Brand.ToLower()) || keyWord.ToLower().ToLower().Contains(item.Color.ToLower()))
                            {
                                item.SortPriority++;
                            }
                        }

                    }
                }
                #endregion

                #region sort
                switch (sortBy?.ToLower())
                {
                    case "name_desc": itemsFromQuery = itemsFromQuery.OrderBy(i => i.SortPriority).ThenByDescending(i => i.Name).ToList(); break;
                    case "name_asc": itemsFromQuery = itemsFromQuery.OrderBy(i => i.SortPriority).ThenBy(i => i.Name).ToList(); break;
                    case "price_desc": itemsFromQuery = itemsFromQuery.OrderBy(i => i.SortPriority).ThenByDescending(i => i.Price).ToList(); break;
                    case "price_asc": itemsFromQuery = itemsFromQuery.OrderBy(i => i.SortPriority).ThenBy(i => i.Price).ToList(); break;
                    case "date_desc": itemsFromQuery = itemsFromQuery.OrderBy(i => i.SortPriority).ThenByDescending(i => i.CreatedDate).ToList(); break;
                    case "date_asc": itemsFromQuery = itemsFromQuery.OrderBy(i => i.SortPriority).ThenBy(i => i.CreatedDate).ToList(); break;
                    default: itemsFromQuery = itemsFromQuery.OrderBy(i => i.SortPriority).ThenBy(item => new Random().Next()).ToList(); break;
                }


                #endregion

                if (itemsFromQuery == null)
                {
                    throw new NullReferenceException("Error in subcategory id");
                }
                return _mapper.Map<List<ItemData>>(itemsFromQuery);
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
                item.pending = false;
                _appDbContext.Items.Update(item);
                #region Update Subcategory Average Price

                var subCategory = await _appDbContext.SubCategories.Where(s => s.Id == item.SubCategoryId && s.Status == true).FirstOrDefaultAsync();

                if (subCategory == null)
                    throw new Exception("Cant find Subcategory at item inseration");

                var ItemsCountInSubCat = await _appDbContext.Items.Where(i => i.Status == true && i.pending != true && i.SubCategoryId == item.SubCategoryId).CountAsync();

                subCategory.AveragePrice = (int)(ItemsCountInSubCat != 0 ? ((ItemsCountInSubCat * subCategory.AveragePrice) + item.Price) / (ItemsCountInSubCat + 1) : (subCategory.AveragePrice + item.Price) / 2);
                #endregion

                await _appDbContext.SaveChangesAsync();
            }
            catch { throw; }
        }
        public async Task RejectPendingItem(Guid ItemId)
        {
            try
            {
                var item = (await _appDbContext.Items.Where(Item => Item.Status == true && Item.pending == true && Item.Id == ItemId).FirstOrDefaultAsync());
                if (item == null)
                {
                    throw new NullReferenceException("Can not Find Item");
                }
                item.Status = false;
                _appDbContext.Items.Update(item);


                await _appDbContext.SaveChangesAsync();
            }
            catch { throw; }
        }

        public string getSubcategoryName(Guid id,List<SubCategoryData> list)
        {
            return list.Where(sub => sub.Id == id).FirstOrDefault().Name.ToLower();
                

        }
    }
}
