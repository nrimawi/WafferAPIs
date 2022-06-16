using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WafferAPIs.DAL.Entities;
using WafferAPIs.DAL.Repositories;
using WafferAPIs.Dbcontext;
using WafferAPIs.Models;


namespace WafferAPIs.Controllers
{

    // [Authorize(Roles = "Admin")]
    [Route("api/Items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _ItemRepository;
        public ItemsController(AppDbContext context, IItemRepository ItemRepository)
        {
            _ItemRepository = ItemRepository;

        }


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


        [HttpPost]
        public async Task<ActionResult<ItemData>> PostItem(ItemData ItemData)
        {
            try
            {
                ItemData createdItem = await _ItemRepository.CreateItem(ItemData);


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



    }
}


