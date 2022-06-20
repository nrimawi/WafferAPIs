

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

    public interface ICategoryRepository : IDisposable
    {
        Task<CategoryData> CreateCategory(CategoryData CategoryData);
        Task<List<CategoryData>> GetCategories();
        Task<CategoryData> GetCategoryById(Guid id);
        Task<CategoryData> UpdateCategory(Guid id, CategoryData categoryData);

        Task DeleteCategory(Guid id);



    }


    public class CategoryRepository : ICategoryRepository
    {
        #region Inject Services
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        #endregion

        public CategoryRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }
        public async Task<CategoryData> CreateCategory(CategoryData CategoryData)
        {
            if (CategoryData == null)
                throw new ArgumentNullException(nameof(CategoryData));

            try
            {
                Category category = _mapper.Map<Category>(CategoryData);

                category.Status = true;
                _appDbContext.Categories.Add(category);
                
                await _appDbContext.SaveChangesAsync();
                return _mapper.Map<CategoryData>(category);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<CategoryData>> GetCategories()
        {
            try
            {
                var activeCategories = await _appDbContext.Categories.Where(Category => Category.Status == true).ToListAsync();
                return _mapper.Map<List<CategoryData>>(activeCategories);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoryData> GetCategoryById(Guid id)
        {
            
             try
            {


                Category category = await _appDbContext.Categories.Where(s => s.Id == id && s.Status == true).FirstOrDefaultAsync();

                if (category == null)
                {
                    throw new NullReferenceException("Category with id=" + id + " is not found");
                }
                return _mapper.Map<CategoryData>(category);
            }
            catch
            {
                throw;
            }
            }

        public async Task<CategoryData> UpdateCategory(Guid id, CategoryData categoryData)
        {
            if (categoryData == null || id != categoryData.Id)
                throw new NullReferenceException("Category is null or id is incorrect");


            try
            {

                Category Category = await _appDbContext.Categories.FindAsync(id);

                if (Category == null || Category.Status == false)
                {
                    throw new Exception("Category with id=" + id + " is not found");
                }
                Category.Name = categoryData.Name;
                Category.Description = categoryData.Description;

                _appDbContext.Categories.Update(Category);
                await _appDbContext.SaveChangesAsync();
                return _mapper.Map<CategoryData>(Category);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCategory(Guid id)
        {
           
            try
            {

                Category Category = await _appDbContext.Categories.FindAsync(id);

                if (Category == null || Category.Status == false)
                {
                    throw new NullReferenceException("Category with id=" + id + " is not found");
                }
                Category.Status = false;
                _appDbContext.Update(Category);
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
