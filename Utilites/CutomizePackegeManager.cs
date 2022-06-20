using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WafferAPIs.DAL.Entities;
using WafferAPIs.DAL.Repositories;
using WafferAPIs.Models.Others;

namespace WafferAPIs.Utilites
{

    public interface ICutomizePackegeManager
    {
        Task<List<ItemData>> createCustomizePackage(CustomizePackageRequest customizePackageRequest);
    }
    public class CutomizePackegeManager : ICutomizePackegeManager
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IItemRepository _itemRepository;
        public CutomizePackegeManager(ISubCategoryRepository subCategoryRepository, IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task<List<ItemData>> createCustomizePackage(CustomizePackageRequest customizePackageRequest)
        {
            try
            {
                #region

                #endregion

                return null;
            }
            catch { throw; }
        }



    }
}
