using Domain.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PFE.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private UserManager<User> _userManager;
        //private SignInManager<FullNameDbColumn> _signInManager;
        private readonly ApplicationSettings _appSettings;
        private readonly IAuth _authServices;
        public UserController(
            UserManager<User> userManager,
           // SignInManager<User> signInManager,
            IOptions<ApplicationSettings> appSettings,
            IAuth authServices

                )
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            _appSettings = appSettings.Value;
            _authServices = authServices;

        }

        /* [HttpPost]
         [Route("Register")]
         //POST : /api/ApplicationUser/Register
         public async Task<Object> PostApplicationUser(UserModel model)
         {
             var applicationUser = new FullNameDbColumn
             {
                 UserName = model.UserName,
                 Email = model.Email,
                 FullName = model.FullName
             };

             try
             {
                 var result = await _userManager.CreateAsync(applicationUser, model.Password);
                 return Ok(result);
             }
             catch (Exception)
             {

                 throw;
             }
         }


             [HttpPost]
             [Route("Login")]
             //POST :/api/ApplicationUser/Login
             public async Task<IActionResult> Login(LoginModel model)
             {
                 var user = await _userManager.FindByNameAsync(model.Username);
                 if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                 {
                     var tokenDescriptor = new SecurityTokenDescriptor
                     {
                         Subject = new ClaimsIdentity(new Claim[]
                             {
                             new Claim("UserID",user.Id.ToString())
                             }),
                         Expires = DateTime.UtcNow.AddDays(1),
                         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                     };

                     var tokenHandler = new JwtSecurityTokenHandler();
                     var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                     var token = tokenHandler.WriteToken(securityToken);
                     return Ok(new { token });
                 }
                 else return BadRequest(new { message = "Username or password is incorrect !" });
             }

             [HttpGet]
             [Authorize]
             //GET : /api/UserProfile
             public async Task<Object> GetUserProfile()
             {
                 string userId = User.Claims.First(c => c.Type == "UserID").Value;
                 var user = await _userManager.FindByIdAsync(userId);
                 return new
                 {
                     user.Id,
                     user.FullName,
                     user.Email,
                     user.UserName,

                 };
             }

             /*[HttpPost]
             [Authorize]
             //POST : /api/UpdateProfile
             public async Task<Object> UpdateProfile(UserModel model)
             {
                 string userId = User.Claims.First(c => c.Type == "UserID").Value;
                 var user = await _userManager.FindByIdAsync(userId);

                 if (!string.IsNullOrEmpty(model.FullName))
                     user.FullName = model.FullName;
                 user.Email = model.Email;
                 user.UserName = model.Email;
                 user.PhoneNumber = model.Email;


             }
        */




        //[HttpGet]
        //[Authorize]
        ////GET : /api/UserProfile
        //public async Task<Object> GetUserProfile() => await _authServices.GetUserProfile();

        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Register
        public async Task<Object> Register(RegisterModel model) => await _authServices.Register(model);

        [HttpPost]
        [Route("Login")]
        //POST :/api/ApplicationUser/Login
        public async Task<Object> Login(LoginModel model)
        {
            Object token = await _authServices.Login(model);
            if (token != null)
            {
                return Ok(new { token });
            }
            else return BadRequest(new { message = "Username or password is incorrect !" });
        }
        /*
        [HttpGet]
        [Authorize]
        //GET : /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.UserName,

            };
        }

        [HttpPost]
        [Route("Update")]
        //POST :/api/users/edit/id
        public async Task<Object> UpdateAccount(string id, UserModel model)
        {
            Object user = await _authServices.Update(id, model);
            if (user != null)
            {
                return Ok(new { user });
            }
            else return BadRequest(new { message = "Account not Found !" });
        }*/
    }
}
