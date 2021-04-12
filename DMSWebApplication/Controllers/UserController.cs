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


        /* [HttpGet]
         [Authorize]  //error 401
         [Route("GetAllUserProfile")]
         public IActionResult GetAllUsers()
         {
             try
             {
                 var Users = _context.Users.ToList();
                 return Ok(new { data = new { Users } });

             }
             catch (Exception ex)
             {
                 throw;
             }
         }*/

        [HttpPost("addgroup")]
      //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        //[FromBody]
        public async Task<IActionResult> RegisterGroup([FromBody] Group group)
        {

          //  var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault().Value;
           // var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();

            //group.GroupOwner = user;
            group.CreatedDate = DateTime.Now.Date;

            Folder parent = _context.Folder.Where(f => f.FolderId == 1).FirstOrDefault();

          /*  Folder Foldercontainer = new Folder()
            {
                FolderName = group.GroupName + "checkpoint",
                FolderPath = group.GroupName + "/",
               // FolderGroup = group,
               // FolderOwner = user,
                ParentFolder = parent,
                DateOfCreate = DateTime.Now.Date
            };*/
           // _context.Folder.Add(Foldercontainer);

            if (ModelState.IsValid)
            {
                _context.Group.Add(group);
                _context.SaveChanges();

                return Ok("group was added ");
            }

            return Ok("adding group was failed");
        }

        [HttpGet]
        [Route("getgroups")]
        [AllowAnonymous]
        public async Task<IActionResult> Getallgroups()
        {
            try
            {
                var groups = _context.Group.ToList();

                return Ok(groups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        [HttpPost("addusertogroup")]
        [AllowAnonymous]
        public async Task<IActionResult> AddUsertoGroup()
        {

            //var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault().Value;
            var user = _context.Users.Where(u => u.Id == "b06ccfdd-f7f3-47ec-aaba-81efbb01b700").FirstOrDefault();

            var group = _context.Group.Where(u => u.GroupId == 1).FirstOrDefault();

            GroupUser gu = new GroupUser();
            gu.Group = group;
            gu.User = user;
            gu.Id = user.Id;

            if (ModelState.IsValid)
            {
                _context.GroupUser.Add(gu);
                _context.SaveChanges();

                return Ok("user was to group ");
            }
            return Ok("adding user to the group  was faild");
        }

    }
}
