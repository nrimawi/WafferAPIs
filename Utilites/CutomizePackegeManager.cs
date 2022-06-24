using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WafferAPIs.DAL.Entities;
using WafferAPIs.DAL.Repositories;
using WafferAPIs.Models.Dtos;
using WafferAPIs.Models.Others;

namespace WafferAPIs.Utilites
{

    public interface ICustomizedPackegeManager
    {
        Task<List<ItemData>> createCustomizedPackage(CustomizePackageRequest customizePackageRequest);
    }
    public class CustomizedPackegeManager : ICustomizedPackegeManager
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IItemRepository _itemRepository;
        public CustomizedPackegeManager(ISubCategoryRepository subCategoryRepository, IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task<List<ItemData>> createCustomizedPackage(CustomizePackageRequest customizedPackageRequest)
        {
            int retry = 0;

            try
            {
                #region Create required variables
                List<ItemData> customizedPackageresponse = new List<ItemData>();
                List<SubCategoryData> targetedSubCategories = new List<SubCategoryData>();
                List<List<ItemData>> ItemsBySubcategory = new List<List<ItemData>>();
                double currentPackagePrice = 0;

                #endregion

                #region check if request valid and save the targeted subcategories data
                int packageAveragePrice = 0;
                foreach (var subcategoryId in customizedPackageRequest.RequiredItems)
                {
                    var subcategory = await _subCategoryRepository.GetSubCategoryById(subcategoryId);
                    targetedSubCategories.Add(subcategory);
                    packageAveragePrice += subcategory.AveragePrice;
                }

                if (packageAveragePrice * 0.5 > customizedPackageRequest.Budget)
                {
                    throw new Exception("Budget is insufficent");
                }
                #endregion

                #region  Get all targeted items by geting them using their subcategory (offline data)
                foreach (var subcategory in targetedSubCategories)
                {
                    if (customizedPackageRequest.Brand == "Any")
                        ItemsBySubcategory.Add(await _itemRepository.GetItemsBySubCategory(subcategory.Id, "", ""));

                    else
                        ItemsBySubcategory.Add(await _itemRepository.GetItemsByBrandAndSubCategory(subcategory.Id, customizedPackageRequest.Brand));

                }
                #endregion

                #region  Find items that satisify the conditions

                do
                {
                    retry++;
                    customizedPackageresponse.Clear();
                    currentPackagePrice = 0;

                    int index = 0;
                    foreach (var subcategory in targetedSubCategories)
                    {
                        ItemData choosenItem = new ItemData();
                        #region TVs
                        if (subcategory.Name.Equals("TVs", StringComparison.InvariantCultureIgnoreCase))
                        {
                            choosenItem = ItemsBySubcategory[index++].OrderBy(item => new Random().Next()).FirstOrDefault();


                        }
                        #endregion

                        #region Hair Dryers
                        else if (subcategory.Name.Equals("Hair Dryers", StringComparison.InvariantCultureIgnoreCase))
                        {
                            choosenItem = ItemsBySubcategory[index++].OrderBy(item => new Random().Next()).FirstOrDefault();


                        }
                        #endregion

                        #region Vacuum Cleaners
                        else if (subcategory.Name.Equals("Vacuum Cleaners", StringComparison.InvariantCultureIgnoreCase))
                        {
                            int neededCapacity = 2500;

                            if (customizedPackageRequest.FamilyMembers < 3)
                                neededCapacity = 1500;

                            else if (customizedPackageRequest.FamilyMembers >= 3 && customizedPackageRequest.FamilyMembers <= 5)
                                neededCapacity = 2500;

                            else if (customizedPackageRequest.FamilyMembers >= 6 && customizedPackageRequest.FamilyMembers <= 8)
                                neededCapacity = 3500;
                            else if (customizedPackageRequest.FamilyMembers < 5)
                                neededCapacity = 4500;

                            choosenItem = ItemsBySubcategory[index++].Where(item => item.Capacity != null &&
                            item.Capacity < neededCapacity + 500 &&
                            item.Capacity > neededCapacity - 500)
                           .OrderBy(item => new Random().Next()).FirstOrDefault();
                        }
                        #endregion


                        #region Refrigerators
                        else if (subcategory.Name.Equals("Refrigerators", StringComparison.InvariantCultureIgnoreCase))
                        {
                            int neededCapacity = 550;

                            if (customizedPackageRequest.FamilyMembers < 3)
                                neededCapacity = 350;

                            else if (customizedPackageRequest.FamilyMembers >= 3 && customizedPackageRequest.FamilyMembers <= 5)
                                neededCapacity = 550;

                            else if (customizedPackageRequest.FamilyMembers >= 6 && customizedPackageRequest.FamilyMembers <= 8)
                                neededCapacity = 750;
                            else if (customizedPackageRequest.FamilyMembers > 8)
                                neededCapacity = 950;

                            choosenItem = ItemsBySubcategory[index++].Where(item => item.Capacity != null &&
                            item.Capacity < neededCapacity + 100 &&
                            item.Capacity > neededCapacity - 100)
                           .OrderBy(item => new Random().Next()).FirstOrDefault();

                        }
                        #endregion


                        #region Washing machine

                        else if (subcategory.Name.Equals("Washing Mashines", StringComparison.InvariantCultureIgnoreCase))
                        {
                            int neededCapacity = 8;

                            if (customizedPackageRequest.FamilyMembers < 3)
                                neededCapacity = 7;

                            else if (customizedPackageRequest.FamilyMembers >= 3 && customizedPackageRequest.FamilyMembers <= 5)
                                neededCapacity = 9;


                            else if (customizedPackageRequest.FamilyMembers > 8)
                                neededCapacity = 11;

                            choosenItem = ItemsBySubcategory[index++].Where(item => item.Capacity != null &&
                            item.Capacity < neededCapacity + 1 &&
                            item.Capacity > neededCapacity - 1)
                           .OrderBy(item => new Random().Next()).FirstOrDefault();

                        }

                        #endregion

                        if (choosenItem != null)
                        {

                            customizedPackageresponse.Add(choosenItem);

                            currentPackagePrice += choosenItem.Price * choosenItem.SaleRatio == 0 ? 1 : choosenItem.SaleRatio;
                        }
                    }
                    if (retry == 50)
                        throw new Exception("Cant Generate Package");
                }

                while (currentPackagePrice > customizedPackageRequest.Budget && retry < 500);
                #endregion

                return customizedPackageresponse;
            }
            catch { throw; }
        }



    }
}
