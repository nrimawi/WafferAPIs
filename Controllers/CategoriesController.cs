using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WafferAPIs.DAL.Entites;
using WafferAPIs.DAL.Entities;
using WafferAPIs.DAL.Helpers.EmailAPI;
using WafferAPIs.DAL.Helpers.EmailAPI.Model;
using WafferAPIs.DAL.Helpers.EmailAPI.Service;
using WafferAPIs.DAL.Helpers.SMSAPI;
using WafferAPIs.DAL.Helpers.SMSAPI.Model;
using WafferAPIs.DAL.Repositories;
using WafferAPIs.Dbcontext;
using WafferAPIs.Models;
using WafferAPIs.Models.Dtos;
using WafferAPIs.Utilites;

namespace WafferAPIs.Controllers
{

    // [Authorize(Roles = "Admin")]
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(AppDbContext context, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }


        [HttpGet]
        public async Task<ActionResult<List<CategoryData>>> GetCategories()
        {
            try
            {
                return Ok(await _categoryRepository.GetCategories());

            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryData>> GetCategory(Guid id)
        {
            try
            {
                return Ok(await _categoryRepository.GetCategoryById(id));
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryData>> PutCategory(Guid id, CategoryData categoryData)
        {
            try
            {
                return Ok(await _categoryRepository.UpdateCategory(id, categoryData));

            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }


        [HttpPost]
        public async Task<ActionResult<CategoryData>> PostCategory(CategoryData categoryData)
        {
            try
            {
                CategoryData createdCategory = await _categoryRepository.CreateCategory(categoryData);


                return CreatedAtAction("GetCategory", new { id = createdCategory.Id }, createdCategory);
            }

            catch (NullReferenceException e)
            {
                return NotFound(e.Message);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {
                await _categoryRepository.DeleteCategory(id);
                return Ok("Deleted Sucsessfully");

            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


    }

}





