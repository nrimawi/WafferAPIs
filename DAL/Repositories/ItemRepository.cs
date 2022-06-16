

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
    }
}
