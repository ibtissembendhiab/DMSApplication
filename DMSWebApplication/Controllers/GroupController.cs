using Domain.Data;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSWebApplication.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private Context _context;
        public GroupController(Context context)
        {
            _context = context;
        }

        [HttpPost("addgroup")]
        //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        //[FromBody]
        public async Task<IActionResult> RegisterGroup([FromBody] Group group)
        {

            var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault().Value;
            var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();
            group.GroupOwner = user;

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

        //Get All Groups
        [HttpGet("allgroups")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllgroups()
        {
            try
            {
              //  var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault().Value;
              //  var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();

                var groups = _context.Group.ToList();
               // foreach (var g in groups)
               // {
                 //   g.ListInterUsers = _context.GInteru.Where(u => u.Group == g).ToList();
                  //  g.GroupOwner = _context.User.Where(u => u == g.GroupOwner).SingleOrDefault();
               // }
                return Ok(groups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpPost("addusertogroup")]
        [AllowAnonymous]
        public async Task<IActionResult>AddUsertoGroup()
        {

            var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault().Value;
            var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();

            var group = _context.Group.Where(u => u.GroupId == 1).FirstOrDefault();

            GroupUser gu = new GroupUser();
            gu.Group = group;
            gu.User = user;
            gu.Id = user.Id;

            if (ModelState.IsValid)
            {
                _context.GroupUser.Add(gu);
                _context.SaveChanges();

                return Ok("user was added to group ");
            }
            return Ok("adding user to the group  was failed");
        }
    }
}
