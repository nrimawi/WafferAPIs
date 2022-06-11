

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WafferAPIs.DAL.Entities;
using WafferAPIs.Dbcontext;
using WafferAPIs.Models.Dtos;
using WafferAPIs.Models.Others;
using WafferAPIs.Utilites;

namespace WafferAPIs.DAL.Repositories
{

    public interface ISubCategoryRepository : IDisposable
    {
        Task<SubCategoryData> CreateSubCategory(SubCategoryData SubCategoryData);
        Task<List<SubCategoryData>> GetSubCategories();
        Task<SubCategoryData> GetSubCategoryById(Guid id);
        Task<SubCategoryData> UpdateSubCategory(Guid id, SubCategoryData SubCategoryData);
        Task DeleteSubCategory(Guid id);
        Task<List<SubCategoryData>> GetSubCategoriesByCategoryId(Guid categoryId);
        Task AddFeaturesToSubCategory(Guid subCategoryId, List<SubCategoryFeature> features);
        Task UpdateSubCategoryFeatures(Guid subCategoryId, List<SubCategoryFeature> features);


    }


    public class SubCategoryRepository : ISubCategoryRepository
    {
        #region Inject Services
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        #endregion

        public SubCategoryRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }
        public async Task<SubCategoryData> CreateSubCategory(SubCategoryData subCategoryData)
        {
            if (subCategoryData == null)
                throw new ArgumentNullException(nameof(subCategoryData));

            try
            {
                SubCategory subCategory = _mapper.Map<SubCategory>(subCategoryData);

                subCategory.Status = true;
                subCategory.Category = null;
                subCategory.CategoryId = subCategoryData.Category.Id;
                _appDbContext.SubCategories.Add(subCategory);

                await _appDbContext.SaveChangesAsync();
                return _mapper.Map<SubCategoryData>(subCategory);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<SubCategoryData>> GetSubCategories()
        {
            try
            {
                var activeSubCategories = await _appDbContext.SubCategories.Where(subCategory => subCategory.Status == true).Include(c => c.Category).ToListAsync();
                return _mapper.Map<List<SubCategoryData>>(activeSubCategories);
            }
            catch
            {
                throw;
            }
        }


        public async Task<SubCategoryData> GetSubCategoryById(Guid id)
        {
            try
            {


                SubCategory SubCategory = await _appDbContext.SubCategories.Where(s => s.Id == id && s.Status == true).FirstOrDefaultAsync();

                if (SubCategory == null)
                {
                    throw new NullReferenceException("SubCategory with id=" + id + " is not found");
                }
                return _mapper.Map<SubCategoryData>(SubCategory);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SubCategoryData> UpdateSubCategory(Guid id, SubCategoryData subCategoryData)
        {
            if (subCategoryData == null || id != subCategoryData.Id)
                throw new NullReferenceException("SubCategory is null or id is incorrect");


            try
            {

                SubCategory SubCategory = await _appDbContext.SubCategories.FindAsync(id);

                if (SubCategory == null || SubCategory.Status == false)
                {
                    throw new Exception("SubCategory with id=" + id + " is not found");
                }
                SubCategory.Name = subCategoryData.Name;
                SubCategory.Description = subCategoryData.Description;
                SubCategory.Category.Id = subCategoryData.Category.Id;
                SubCategory.Fetures = new FeatureMapper().ToEntity(subCategoryData.Fetures);

                _appDbContext.SubCategories.Update(SubCategory);
                await _appDbContext.SaveChangesAsync();
                return _mapper.Map<SubCategoryData>(SubCategory);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteSubCategory(Guid id)
        {

            try
            {

                SubCategory SubCategory = await _appDbContext.SubCategories.FindAsync(id);

                if (SubCategory == null || SubCategory.Status == false)
                {
                    throw new NullReferenceException("SubCategory with id=" + id + " is not found");
                }
                SubCategory.Status = false;
                _appDbContext.Update(SubCategory);
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

        public async Task<List<SubCategoryData>> GetSubCategoriesByCategoryId(Guid categoryId)
        {
            try
            {
                var activeSubCategories = await _appDbContext.SubCategories.Where(subCategory => subCategory.Status == true && subCategory.CategoryId==categoryId).Include(c => c.Category).ToListAsync();
                return _mapper.Map<List<SubCategoryData>>(activeSubCategories);
            }
            catch
            {
                throw;
            }
        }

        public async Task AddFeaturesToSubCategory(Guid subCategoryId, List<SubCategoryFeature> features)
        {
            try
            {

                SubCategory SubCategory = await _appDbContext.SubCategories.FindAsync(subCategoryId);

                if (SubCategory == null || SubCategory.Status == false)
                {
                    throw new Exception("SubCategory with id=" + subCategoryId + " is not found");
                }

                foreach (var feature in features)
                {
                    if (SubCategory.Fetures == "")
                        SubCategory.Fetures = feature.Name + ":" + feature.Type;
                    else
                        SubCategory.Fetures = SubCategory.Fetures + "," + feature.Name + ":" + feature.Type;
                }
                _appDbContext.SubCategories.Update(SubCategory);
                await _appDbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }


        public async Task UpdateSubCategoryFeatures(Guid subCategoryId, List<SubCategoryFeature> features)
        {
            try
            {

                SubCategory SubCategory = await _appDbContext.SubCategories.FindAsync(subCategoryId);

                if (SubCategory == null || SubCategory.Status == false)
                {
                    throw new Exception("SubCategory with id=" + subCategoryId + " is not found");
                }


                SubCategory.Fetures = new FeatureMapper().ToEntity(features);

                _appDbContext.SubCategories.Update(SubCategory);
                await _appDbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

    }
}


