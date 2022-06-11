using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [Route("api/subCategories")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryRepository _SubCategoryRepository;
        public SubCategoriesController(AppDbContext context, ISubCategoryRepository SubCategoryRepository)
        {
            _SubCategoryRepository = SubCategoryRepository;

        }


        [HttpGet]
        public async Task<ActionResult<List<SubCategoryData>>> GetSubCategories()
        {
            try
            {
                return Ok(await _SubCategoryRepository.GetSubCategories());

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
        public async Task<ActionResult<SubCategoryData>> GetSubCategory(Guid id)
        {
            try
            {
                return Ok(await _SubCategoryRepository.GetSubCategoryById(id));
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
        public async Task<ActionResult<SubCategoryData>> PutSubCategory(Guid id, SubCategoryData SubCategoryData)
        {
            try
            {
                return Ok(await _SubCategoryRepository.UpdateSubCategory(id, SubCategoryData));

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
        public async Task<ActionResult<SubCategoryData>> PostSubCategory(SubCategoryData SubCategoryData)
        {
            try
            {
                SubCategoryData createdSubCategory = await _SubCategoryRepository.CreateSubCategory(SubCategoryData);


                return CreatedAtAction("GetSubCategory", new { id = createdSubCategory.Id }, createdSubCategory);
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
        public async Task<IActionResult> DeleteSubCategory(Guid id)
        {
            try
            {
                await _SubCategoryRepository.DeleteSubCategory(id);
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

        [HttpGet("/categoryId/{id}")]
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

        [HttpPatch("/addFeatures/{id}")]
        public async Task<IActionResult> AddFeaturesToSubCategory(Guid id, List<SubCategoryFeature> features)
        {

            try
            {
                await _SubCategoryRepository.AddFeaturesToSubCategory(id, features);
                return Ok("Features added sucessfully");

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


        [HttpPut("/UpdateFeatures/{id}")]
        public async Task<IActionResult> UpdateSubCategoryFeatures(Guid id, List<SubCategoryFeature> features)
        {

            try
            {
                await _SubCategoryRepository.UpdateSubCategoryFeatures(id, features);
                return Ok("Features updated sucessfully");

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


