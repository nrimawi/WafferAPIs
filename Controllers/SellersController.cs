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
using WafferAPIs.DAL.Helpers.EmailAPI.Service;
using WafferAPIs.DAL.Helpers.SMSAPI;
using WafferAPIs.DAL.Helpers.SMSAPI.Model;
using WafferAPIs.DAL.Repositories;
using WafferAPIs.Dbcontext;
using WafferAPIs.Models;
using WafferAPIs.Utilites;

namespace WafferAPIs.Controllers
{
    // [Authorize(Roles = "Admin")]
    [Route("api/sellers")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ISellerRepository _studentRepository;
        //  private readonly IMailService _mailService;
        private readonly IEmailSender _emailSender;
        private readonly ISMSSender _smsSender;
        public SellersController(AppDbContext context, ISellerRepository sellerRepository, IEmailSender emailSender, ISMSSender smsSender)
        {
            _context = context;
            _studentRepository = sellerRepository;
            _emailSender = emailSender;
            _smsSender = smsSender;
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

                #region verify seller
                SellerData seller;
                try
                {
                    seller = await _studentRepository.VerifySeller(sellerId, generetedpassword);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error at verifying user due to " + ex.Message);

                }
                #endregion
                #region Create email data and send req
                MailReqestData emailRequest = new MailReqestData();
                emailRequest.ToEmail = seller.Email;
                emailRequest.Subject = "Waffer - Activate your account";
                emailRequest.Name = seller.Name;
                emailRequest.Link = "https://www.youtube.com/watch?v=Ik_OFtkTGtY&list=RDYhylTnTvBow&index=2";
                emailRequest.Password = generetedpassword;


                try
                {
                    await _emailSender.SendEmailAsync(emailRequest);
                }
                catch (Exception e) { throw new Exception("User verified but error while sending email due to " + e.Message); }
                #endregion
                #region Create sms data and send req
                SMSRequestData smsRequest = new SMSRequestData();
                smsRequest.NameToShow = "WafferServices";
                smsRequest.Message = "أهلاً بك في موقع وفر";
                smsRequest.Number = "+972" + seller.ContactPhoneNumber.ToString();

                try
                {
                    //       await _smsSender.SendSMSAsync(smsRequest);
                }
                catch (Exception e) { throw new Exception("User verified and Email has been sent but error while sending sms due to " + e.Message); }
                #endregion

                return Ok("Seller has been verfied successfully, email has been sent and sms has been sent ");
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


//        [HttpPost("send")]
//        public async Task<IActionResult> SendMail([FromForm] WelcomeRequest request)
//        {
//            try
//            {
//                request.Email = "nazeehRimawi";
//                request.Password = "sdsdsdsds";
//                request.Link = "sdsdsdsd";
//                request.Name = "tets";

//                await _emailSender.SendEmailAsync(request);
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }

//        }


//}
//}
