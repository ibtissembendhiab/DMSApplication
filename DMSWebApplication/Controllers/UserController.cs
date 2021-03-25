using Domain.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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



        //[HttpGet]
        //[Authorize]
        ////GET : /api/UserProfile
        //public async Task<Object> GetUserProfile() => await _authServices.GetUserProfile();

        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<Object> Register(RegisterModel model) => await _authServices.Register(model);

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
      
    }
}
