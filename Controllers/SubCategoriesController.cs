using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using WafferAPIs.DAL.Entites;
using WafferAPIs.DAL.Helpers.EmailAPI;
using WafferAPIs.DAL.Helpers.EmailAPI.Model;
using WafferAPIs.DAL.Helpers.EmailAPI.Service;
using WafferAPIs.DAL.Helpers.SMSAPI;
using WafferAPIs.DAL.Helpers.SMSAPI.Model;
using WafferAPIs.DAL.Repositories;
using WafferAPIs.Dbcontext;
using WafferAPIs.Models;
using WafferAPIs.Models.Dtos;
using WafferAPIs.Models.Others;
using WafferAPIs.Utilites;

namespace WafferAPIs.Controllers
{

    // [Authorize(Roles = "Admin")]
    [Route("api/subcategories")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryRepository _SubCategoryRepository;
        public SubCategoriesController(AppDbContext context, ISubCategoryRepository SubCategoryRepository)
        {
            _SubCategoryRepository = SubCategoryRepository;

        }

        [SwaggerOperation(Summary = "Get all subCategories")]

        [HttpGet]
        public async Task<ActionResult<List<SubCategoryData>>> GetSubCategories()
        {
            try
            {
                return Ok(await _SubCategoryRepository.GetSubCategories());

            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
        }

        [SwaggerOperation(Summary = "Get subCategory by id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoryData>> GetSubCategory(Guid id)
        {
            try
            {
                return Ok(await _SubCategoryRepository.GetSubCategoryById(id));
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
        }
        [SwaggerOperation(Summary = "Update sub category")]

        [HttpPut("{id}")]
        public async Task<ActionResult<SubCategoryData>> PutSubCategory(Guid id, SubCategoryData SubCategoryData)
        {
            try
            {
                return Ok(await _SubCategoryRepository.UpdateSubCategory(id, SubCategoryData));

            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "Inner Ex: " + e.InnerException.Message);

            }

        }

        [SwaggerOperation(Summary = "Create new sub category")]

        [HttpPost]
        public async Task<ActionResult<SubCategoryData>> PostSubCategory(SubCategoryData SubCategoryData)
        {
            try
            {
                SubCategoryData createdSubCategory = await _SubCategoryRepository.CreateSubCategory(SubCategoryData);


                return CreatedAtAction("GetSubCategory", new { id = createdSubCategory.Id }, createdSubCategory);
            }

            catch (NullReferenceException e)
            {
                return NotFound(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
        }


        [SwaggerOperation(Summary = "Delete sub category")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(Guid id)
        {
            try
            {
                await _SubCategoryRepository.DeleteSubCategory(id);
                return Ok("Deleted Sucsessfully");

            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
        }


        [SwaggerOperation(Summary = " Get sub categories by categoryId")]
        [HttpGet("/category-id/{id}")]
        public async Task<ActionResult<SubCategoryData>> GetSubCategoriesByCategoryId(Guid id)
        {

            try
            {
                return Ok(await _SubCategoryRepository.GetSubCategoriesByCategoryId(id));

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

        [SwaggerOperation(Summary = "Add feature/s at existing subcategory features")]
        [HttpPatch("add-features/{id}")]
        public async Task<IActionResult> AddFeaturesToSubCategory(Guid id, List<SubCategoryFeature> features)
        {

            try
            {
                await _SubCategoryRepository.AddFeaturesToSubCategory(id, features);
                return Ok("Features added sucessfully");

            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
        }

        [SwaggerOperation(Summary = "Update subCategory features")]
        [HttpPut("update-features/{id}")]
        public async Task<IActionResult> UpdateSubCategoryFeatures(Guid id, List<SubCategoryFeature> features)
        {

            try
            {
                await _SubCategoryRepository.UpdateSubCategoryFeatures(id, features);
                return Ok("Features updated sucessfully");

            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
        }

        [SwaggerOperation(Summary = "Get features for specific subCategory")]
        [HttpGet("get-features/{id}")]
        public async Task<ActionResult<List<SubCategoryFeature>>> GetSubCategoryFeatures(Guid id)
        {

            try
            {

                return Ok(await _SubCategoryRepository.GetSubCategoryFeatures(id));

            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
        }

    }
}


