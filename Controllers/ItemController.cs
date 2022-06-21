using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WafferAPIs.DAL.Entities;
using WafferAPIs.DAL.Repositories;
using WafferAPIs.Dbcontext;
using WafferAPIs.Models;
using WafferAPIs.Models.Others;
using WafferAPIs.Utilites;

namespace WafferAPIs.Controllers
{

    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _ItemRepository;
        private readonly ICustomizedPackegeManager _customizedPackegeManager;
        public ItemsController(AppDbContext context, IItemRepository ItemRepository, ICustomizedPackegeManager customizedPackegeManager)
        {
            _ItemRepository = ItemRepository;
            _customizedPackegeManager = customizedPackegeManager;
        }
        [SwaggerOperation(Summary = "Get all items")]
        [HttpGet]
        public async Task<ActionResult<List<ItemData>>> GetItems([FromQuery] string searchFor, [FromQuery] string sortBy)
        {
            try
            {
                return Ok(await _ItemRepository.GetItems(searchFor,sortBy));

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

        [SwaggerOperation(Summary = "Get item by id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemData>> GetItem(Guid id)
        {
            try
            {
                return Ok(await _ItemRepository.GetItemById(id));
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

        // [Authorize(Roles = "Admin,User")]
        [SwaggerOperation(Summary = "Create new item")]
        [HttpPost]
        public async Task<ActionResult<ItemData>> PostItem(ItemData itemData)
        {
            try
            {
                ItemData createdItem = await _ItemRepository.CreateItem(itemData);


                return CreatedAtAction("GetItem", new { id = createdItem.Id }, createdItem);
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

        // [Authorize(Roles = "Admin,User")]
        [SwaggerOperation(Summary = "Delete an item")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            try
            {
                await _ItemRepository.DeleteItem(id);
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

        [SwaggerOperation(Summary = "Get items by seller ids")]
        [HttpGet("seller/{id}")]
        public async Task<ActionResult<ItemData>> GetItemsBySeller(Guid id)
        {
            try
            {

                return Ok(await _ItemRepository.GetItemsBySeller(id));

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


        [SwaggerOperation(Summary = "Get items by sub category id")]
        [HttpGet("subcategory/{id}")]
        public async Task<ActionResult<ItemData>> GetItemsBySubCategory([FromRoute] Guid id,[FromQuery] string searchFor, [FromQuery]  string sortBy)
        {
            try
            {

                return Ok(await _ItemRepository.GetItemsBySubCategory(id,searchFor,sortBy));

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


        [SwaggerOperation(Summary = "Get items by sub category id and brand name")]
        [HttpGet("subcategory/{id}/{brandname}")]
        public async Task<ActionResult<ItemData>> GetItemsByBrandAndSubCategory(Guid id, string brandname)
        {
            try
            {


                return Ok(await _ItemRepository.GetItemsByBrandAndSubCategory(id, brandname));

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

        // [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Get all pending items")]
        [HttpGet("pending")]
        public async Task<ActionResult<ItemData>> GetPendingItems()
        {
            try
            {

                return Ok(await _ItemRepository.GetPendingItems());

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

        //  [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Approve pending item by Id")]
        [HttpPost("approve/{id}")]
        public async Task<IActionResult> ApprovePendingItem(Guid id)
        {
            try
            {
                await _ItemRepository.ApprovePendingItem(id);
                return Ok("item Has been approved successfully");

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

        //  [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Reject pending item by Id")]
        [HttpPost("reject/{id}")]
        public async Task<IActionResult> RejectPendingItem(Guid id)
        {
            try
            {
                await _ItemRepository.RejectPendingItem(id);
                return Ok("item Has been approved successfully");

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


        [SwaggerOperation(Summary = "Customize Package")]
        [HttpPost("customized-package")]
        public async Task<IActionResult> createCusomizedPackage(CustomizePackageRequest customizePackageRequest)
        {
            try
            {
               
                return Ok(await _customizedPackegeManager.createCustomizedPackage(customizePackageRequest));

            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }




    }
}


