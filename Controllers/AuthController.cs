
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

namespace WafferAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthController(IAuthenticationRepository authenticationRepository, ISellerRepository sellerRepository )
        {
            _sellerRepository = sellerRepository;
            _authenticationRepository = authenticationRepository;
        }


        //[HttpPost]
        //[Route("register")]
        //public async Task<IActionResult> Register([FromBody] AdminRegisterModel model)
        //{
        //    var userExits = await _userManager.FindByNameAsync(model.Username);
        //    if (userExits != null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already Exists" });

        //    ApplicationUser user = new()
        //    {
        //        Email = model.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = model.Username,
        //    };



        //    var result = await _userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed due to" + result.ToString() });



        //    if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        //        await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));


        //    if (await _roleManager.RoleExistsAsync(UserRoles.User))
        //        await _userManager.AddToRoleAsync(user, UserRoles.User);



        //    return Ok(new Response { Status = "Success", Message = "User created sucessfully" });
        //}


        //[HttpPost]
        //[Route("register-admin")]
        //public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterModel model)
        //{
        //    var userExits = await _userManager.FindByNameAsync(model.Username);
        //    if (userExits != null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already Exists" });

        //    ApplicationUser user = new()
        //    {
        //        Email = model.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = model.Username,
        //    };

        //    var result = await _userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed" + result.ToString() });


        //    if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        //        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));



        //    if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        //        await _userManager.AddToRoleAsync(user, UserRoles.Admin);

        //    return Ok(new Response { Status = "Success", Message = "User created sucessfully" });
        //}

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {   
                var res= await _authenticationRepository.Login(model);
               var seller= await _sellerRepository.GetLoggedInSeller(res.UserAuthId) ;
                res.Seller = seller ;
                res.UserAuthId = "";
                return Ok(res);

            }
            catch (Exception ex)
            {

                return Unauthorized();
            }
        }
    }
}
