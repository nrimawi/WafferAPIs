using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WafferAPIs.DAL.Entities;
using WafferAPIs.DAL.Repositories;
using WafferAPIs.Dbcontext;
using WafferAPIs.Models;
using WafferAPIs.Models.Others;

namespace WafferAPIs.Controllers
{

    // [Authorize(Roles = "Admin")]
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _ItemRepository;
        public ItemsController(AppDbContext context, IItemRepository ItemRepository)
        {
            _ItemRepository = ItemRepository;

        }

        [SwaggerOperation(Summary = "Get all items")]
        [HttpGet]
        public async Task<ActionResult<List<ItemData>>> GetItems()
        {
            try
            {
                return Ok(await _ItemRepository.GetItems());

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

        [SwaggerOperation(Summary = "Create new item")]
        [HttpPost]
        public async Task<ActionResult<ItemData>> PostItem(ItemData  itemData)
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


        [SwaggerOperation(Summary = "Get items by seller id ")]
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


        [SwaggerOperation(Summary = "Get items by subCategory id ")]
        [HttpGet("subcategory/{id}")]
        public async Task<ActionResult<ItemData>> GetItemsBySubCategory(Guid id)
        {
            try
            {

                return Ok(await _ItemRepository.GetItemsBySubCategory(id));

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


        [SwaggerOperation(Summary = "Get items by subCategory id and brand name")]
        [HttpGet("subcategory/{id}/{brandname}")]
        public async Task<ActionResult<ItemData>> GetItemsByBrandAndSubCategory(Guid id, string brandname)
        {
            try
            {


                return Ok(await _ItemRepository.GetItemsByBrandAndSubCategory(id,brandname));

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

        [SwaggerOperation(Summary = "Get pending items")]
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


        [SwaggerOperation(Summary = "Approve pending items")]
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
    }
}


