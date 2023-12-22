using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SMSEmailService.DAL;
using SMSEmailService.DAL.Context;
using SMSEmailService.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SMSEmailService.DAL.IdentityUsers;
using Microsoft.AspNetCore.Cors;

namespace SMSEmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AuthenticateController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ResponseModel> Login([FromBody] UserProfileModel model)
        {
            ResponseModel resmodel = new ResponseModel();
            resmodel.IsSuccess = false;
            var user = await userManager.FindByNameAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                var newtoken = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };
                resmodel.IsSuccess = true;
                resmodel.Message = "Login Successful";
                resmodel.ErrorCode = "";
                resmodel.responseData = newtoken;
                return resmodel;
            }
            resmodel.Message = "Invalid Username or Password";
            resmodel.ErrorCode = "404";
            return resmodel;
        }


        [HttpPost]
        [Route("resetPassword")]
        public async Task<ResponseModel> ResetPassword([FromBody] ResetPasswordModel model)
        {
            ResponseModel resmodel = new ResponseModel();
            resmodel.IsSuccess = false;
            try
            {
                var user = await userManager.FindByNameAsync(model.Email);
                if (user != null && await userManager.CheckPasswordAsync(user, model.OldPassword))
                {
                    string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                    IdentityResult passwordChangeResult = await userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);
                }
                else
                {
                    resmodel.IsSuccess = false;
                    resmodel.ErrorCode = "404";
                    resmodel.Message = "Invalid Username or Password";
                    return resmodel;
                }
            }
            catch(Exception ex)
            {
                resmodel.IsSuccess = false;
                resmodel.ErrorCode = "404";
                resmodel.Message = ex.ToString();
                return resmodel;
            }
            resmodel.IsSuccess = true;
            resmodel.Message = "Changed Password successfully";
            return resmodel;
        }


            //[HttpPost]
            //[Route("register")]
            //public async Task<IActionResult> Register([FromBody] RegisterModel model)
            //{
            //    var userExists = await userManager.FindByNameAsync(model.Email);
            //    if (userExists != null)
            //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            //    ApplicationUser user = new ApplicationUser()
            //    {
            //        Email = model.Email,
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        UserName = model.Email
            //    };
            //    var result = await userManager.CreateAsync(user, model.Password);
            //    if (!result.Succeeded)
            //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            //    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            //}


            //[HttpPost]
            //[Route("register-admin")]
            //public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
            //{
            //    var userExists = await userManager.FindByNameAsync(model.Email);
            //    if (userExists != null)
            //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            //    ApplicationUser user = new ApplicationUser()
            //    {
            //        Email = model.Email,
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        UserName = model.Email
            //    };
            //    var result = await userManager.CreateAsync(user, model.Password);
            //    if (!result.Succeeded)
            //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            //    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            //    if (!await roleManager.RoleExistsAsync(UserRoles.User))
            //        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //    if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            //    {
            //        await userManager.AddToRoleAsync(user, UserRoles.Admin);
            //    }

            //    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            //}
        }
}
