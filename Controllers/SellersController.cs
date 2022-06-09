using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WafferAPIs.DAL.Entites;
using WafferAPIs.DAL.Helpers.EmailAPI;
using WafferAPIs.DAL.Helpers.EmailAPI.Model;
using WafferAPIs.DAL.Repositories;
using WafferAPIs.Dbcontext;
using WafferAPIs.Models;
using WafferAPIs.Utilites;

namespace WafferAPIs.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/sellers")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ISellerRepository _studentRepository;
        private readonly IMailService _mailService;

        public SellersController(AppDbContext context, ISellerRepository sellerRepository, IMailService mailService)
        {
            _context = context;
            _studentRepository = sellerRepository;
            _mailService = mailService;
        }


        [HttpGet]
        public async Task<ActionResult<List<SellerData>>> GetSellers()
        {
            try
            {
                return Ok(await _studentRepository.GetSellers());

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
        public async Task<ActionResult<SellerData>> GetSeller(Guid id)
        {
            try
            {
                return Ok(await _studentRepository.GetSellerById(id));
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
        public async Task<ActionResult<SellerData>> PutSeller(Guid id, SellerData sellerData)
        {
            try
            {
                return Ok(await _studentRepository.UpdateSeller(id, sellerData));

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
        public async Task<ActionResult<SellerData>> PostSeller(SellerData sellerData)
        {
            try
            {
                SellerData createdSeller = await _studentRepository.CreateSeller(sellerData);


                return CreatedAtAction("GetSeller", new { id = createdSeller.Id }, createdSeller);
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
        public async Task<IActionResult> DeleteSeller(Guid id)
        {
            try
            {
                await _studentRepository.DeleteSeller(id);
                return Ok();

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


        [HttpGet("/pending-sellers")]
        public async Task<ActionResult<List<SellerData>>> GetPendingVerificationSellers()
        {
            try
            {
                return Ok(await _studentRepository.GetPendingVerficationSellers());

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

        [HttpGet("/verified-sellers")]
        public async Task<ActionResult<List<SellerData>>> GetVerifiedSellers()
        {
            try
            {
                return Ok(await _studentRepository.GetVerifiedSellers());

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

        [HttpPost("/verify-seller")]
        public async Task<IActionResult> VerifySeller(Guid sellerId)
        {

            try
            {
                var generetedpassword = "W" + new RandomPasswordGenerator().Password + "@";

                var seller = await _studentRepository.VerifySeller(sellerId, generetedpassword);

                WelcomeRequest request = new WelcomeRequest();
                request.Email = seller.Email;
                request.Name = seller.Name;
                request.Link = "www.google.com";
                request.Password = generetedpassword;

                try
                {
                    await _mailService.SendWelcomeEmailAsync(request);
                }
                catch (Exception e) { throw new Exception("User verified but Error while sending email"); }

                return Ok("Seller has been verfied successfully and email has been sent");
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


        //    [HttpPost("send")]
        //    public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        //    {
        //        try
        //        {
        //            await _mailService.SendEmailAsync(request);
        //            return Ok();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }

        //    }
        //}
    }
}
