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
using WafferAPIs.Utilites;

namespace WafferAPIs.Controllers
{

    // [Authorize(Roles = "Admin")]
    [Route("api/sellers")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IEmailSender _emailSender;
        private readonly ISMSSender _smsSender;
        public SellersController(ISellerRepository sellerRepository, IEmailSender emailSender, ISMSSender smsSender)
        {
            _sellerRepository = sellerRepository;
            _emailSender = emailSender;
            _smsSender = smsSender;
        }

       // [Authorize(Roles = "Admin")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get all sellers")]
        public async Task<ActionResult<List<SellerData>>> GetSellers()
        {
            try
            {
                return Ok(await _sellerRepository.GetSellers());

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

        [SwaggerOperation(Summary = "Get seller by id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SellerData>> GetSeller(Guid id)
        {
            try
            {
                return Ok(await _sellerRepository.GetSellerById(id));
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

       // [Authorize(Roles = "Admin,User")]
        [SwaggerOperation(Summary = "Update seller")]
        [HttpPut("{id}")]
        public async Task<ActionResult<SellerData>> PutSeller(Guid id, SellerData sellerData)
        {
            try
            {
                return Ok(await _sellerRepository.UpdateSeller(id, sellerData));

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

        [SwaggerOperation(Summary = "Create new seller(Register New Seller)")]
        [HttpPost]
        public async Task<ActionResult<SellerData>> PostSeller(SellerData sellerData)
        {
            try
            {
                SellerData createdSeller = await _sellerRepository.CreateSeller(sellerData);


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
       // [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete seller by Id")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeller(Guid id)
        {
            try
            {
                await _sellerRepository.DeleteSeller(id);
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

       // [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Get pending sellers(Not Verifyed or Rejected Yet)")]
        [HttpGet("pending-sellers")]
        public async Task<ActionResult<List<SellerData>>> GetPendingVerificationSellers()
        {
            try
            {
                return Ok(await _sellerRepository.GetPendingVerficationSellers());

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

        //[Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Get all verified sellers")]
        [HttpGet("verified-sellers")]
        public async Task<ActionResult<List<SellerData>>> GetVerifiedSellers()
        {
            try
            {
                return Ok(await _sellerRepository.GetVerifiedSellers());

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

      //  [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Verify seller then send sms & email with password")]
        [HttpPost("verify-seller")]
        public async Task<IActionResult> VerifySeller(Guid sellerId)
        {

            try
            {
                var password = "W" + new RandomPasswordGenerator().Password + "@";

                #region verify seller
                SellerData seller;
                try
                {
                    seller = await _sellerRepository.VerifySeller(sellerId, password);
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
                emailRequest.Password = password;


                try
                {
                    await _emailSender.SendEmailAsync(emailRequest);
                }
                catch (Exception e) { throw new Exception("User verified but error while sending email due to " + e.Message); }
                #endregion
                #region Create sms data and send req
                SMSRequestData smsRequest = new SMSRequestData();
                smsRequest.From = "Wffer";
                //   smsRequest.Text = $"Welcome to Waffer, your request has been accepted.Please login to activate your account. Your password is: { password}\n أهلاً بك في موقع وفر، تم قبول طلبك الرجاء تسجيل الدخول لتفعيل حسابك رقمك السري هو{password} ";
                smsRequest.Text = $"Welcome to waffer, your request has been accepted.Please login to activate your account. Your password is: { password}";


                smsRequest.To = "+972" + seller.ContactPhoneNumber.ToString().Substring(1);

                try
                {
                    await _smsSender.SendSMSAsync(smsRequest);
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

      //  [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Reject seller then send sms with the reason")]
        [HttpPost("reject-seller")]
        public async Task<IActionResult> RejectSeller(Guid sellerId, string reason)
        {

            try
            {

                if (reason == null || reason.Length == 0)
                    reason = "Undefiend Reason";

                #region reject seller
                SellerData seller;
                try
                {
                    seller = await _sellerRepository.RejectSellerRequest(sellerId);
                   
                }
                catch (Exception ex)
                {
                    throw new Exception("Error at rejecting user due to " + ex.Message);

                }
                #endregion
                #region  Send Sms

                SMSRequestData smsRequest = new SMSRequestData();
                smsRequest.From = "Waffer";
                smsRequest.Text = $"Your registration request at Waffer was decliend due to: {reason}, please try to register again!";


                smsRequest.To = "+972" + seller.ContactPhoneNumber.ToString().Substring(1);

                try
                {
                    await _smsSender.SendSMSAsync(smsRequest);
                }
                catch (Exception e) { throw new Exception("User rejected but sms was failed due to  " + e.Message); }
                #endregion

                return Ok("User has been rejected and sms was sent successfully ");
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

        [HttpPost("xx-seller")]
        public async Task<IActionResult> xx()
        {
            #region Create sms data and send req
            SMSRequestData smsRequest = new SMSRequestData();
            smsRequest.From = "Wffer";
            //   smsRequest.Text = $"Welcome to Waffer, your request has been accepted.Please login to activate your account. Your password is: { password}\n أهلاً بك في موقع وفر، تم قبول طلبك الرجاء تسجيل الدخول لتفعيل حسابك رقمك السري هو{password} ";
            smsRequest.Text = $"Welcome to waffer, your request has been accepted.Please login to activate your account. Your password is";


            smsRequest.To = "+972599646099";

            try
            {
                await _smsSender.SendSMSAsync(smsRequest);
                return Ok();
            }
            catch (Exception e) { throw new Exception("User verified and Email has been sent but error while sending sms due to " + e.Message); }
            #endregion


        }

    }



}

