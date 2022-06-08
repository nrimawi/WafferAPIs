using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WafferAPIs.Models;
using WafferAPIs.Models.Auth;



namespace WafferAPIs.DAL.Repositories
{

    public interface IAuthenticationRepository
    {
        Task<ApplicationUser> RegisterUser(string email,string password);
        Task RegisterAdmin(AdminRegisterModel adminRegisterModel);
        Task<LoginResponse> Login(LoginModel loginModel);
    }
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
     
        public AuthenticationRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration ;

        }

        public async Task<LoginResponse> Login(LoginModel loginModel)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);

                if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    #region Validation of user info and genreate token 
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
                };
                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddDays(5),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
                    #endregion

                   
                    #region response
                    LoginResponse response = new LoginResponse();
                    response.UserAuthId=user.Id;
                    response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    response.Roles = (List<string>)_userManager.GetRolesAsync(user).Result;
                   
                    #endregion
                    return response;

                }

                else
                    throw new UnauthorizedAccessException();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RegisterAdmin(AdminRegisterModel adminRegisterModel)
        {
            var userExits = await _userManager.FindByNameAsync(adminRegisterModel.Username);
            if (userExits != null)
                throw new Exception("User already Exists");

            ApplicationUser user = new()
            {
                Email = adminRegisterModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = adminRegisterModel.Username,
            };

            var result = await _userManager.CreateAsync(user, adminRegisterModel.Password);
            if (!result.Succeeded)
                throw new Exception("User creation failed" + result.ToString());


            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);


        }


        public async Task<ApplicationUser> RegisterUser(string email,string password)
        {

            ApplicationUser user = new()
            {
                Email=email,
                UserName =Guid.NewGuid().ToString(),    
                SecurityStamp = Guid.NewGuid().ToString()

            };


            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new Exception("User creation failed" + result.ToString());



            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));


            if (await _roleManager.RoleExistsAsync(UserRoles.User))
                await _userManager.AddToRoleAsync(user, UserRoles.User);



            return user;
        }
    }

}
