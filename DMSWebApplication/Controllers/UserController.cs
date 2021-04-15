using Domain.Data;
using Domain.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Service;
using Service.Interfaces;
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
        private readonly ApplicationSettings _appSettings;
        private readonly IAuth _authServices;
        private readonly Context _context;
       // private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<ApplicationSettings> appSettings,
            Context context,
            IAuth authServices)

        {
            _userManager = userManager;
            _roleManager = roleManager; 
            _appSettings = appSettings.Value;
            _authServices = authServices;
            _context = context;
        }
     
       // [Authorize(Roles = "Admin")]
       // [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<Object> Register(Register model) => await _authServices.Register(model);

       // [Authorize(Roles = "Admin, Employee")]
        [HttpPost]
        [Route("Login")]
        //POST :/api/User/Login
        public async Task<Object> Login(LoginModel model)
        {
            Object token = await _authServices.Login(model);
            if (token != null)
            {
                return Ok(new { token });
            }
            else return BadRequest(new { message = "Username or password is incorrect !" });
        }

        [HttpPut("{id}")]
        public async Task<Object> Update(string id, UserUpdate model) => await _authServices.Update(id, model);

        [HttpDelete("{id}")]
        public async Task<Object> Delete(string id) => await _authServices.Delete(id);

        [HttpGet]
        [Route("GetAllUsers")]
        public List<User> GetAll() => _authServices.GetAll();


        /*  [HttpGet]
         // [Authorize]
          [Route("GetUserProfile")]
          //GET : /api/UserProfile
          public async Task<Object> GetUserProfile()
          {
              string userId = User.Claims.First(c => c.Type == "UserId").Value;
              var user = await _userManager.FindByIdAsync(userId);
              return new
              {
                  user.FirstName,
                  user.LastName,
                  user.UserName,
                  user.Email 
              };
          }*/

    }
}
