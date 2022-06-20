
using WafferAPIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Models.Auth;
using WafferAPIs.Models.Auth;
using WafferAPIs.DAL.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace WafferAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthController(IAuthenticationRepository authenticationRepository, ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
            _authenticationRepository = authenticationRepository;
        }



        [SwaggerOperation(Summary = "Create new Admin(This method is accessed by super admin only)")]

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterModel model)
        {
            try
            {
                await _authenticationRepository.RegisterAdmin(model);
                return Ok(new Response { Status = "Success", Message = "User created sucessfully" });
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }
        [SwaggerOperation(Summary = "Login for all system users")]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            //In this method there a conflect in return data
            //When Admin is logged in , we need to return its name 
            //When seller we need to return its data
            try
            {
                var res = await _authenticationRepository.Login(model);

                if (res.Roles.Contains("Admin"))
                {
                    res.UserAuthId = "";

                    return Ok(res);
                }
                var seller = await _sellerRepository.GetLoggedInSeller(res.UserAuthId);
                res.Seller = seller;
                res.AdminName = "";
                res.UserAuthId = "";
                return Ok(res);

            }
            catch
            {

                return Unauthorized();
            }
        }
    }
}
