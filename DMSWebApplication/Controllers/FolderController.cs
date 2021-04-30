using Domain.Data;
using Domain.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSWebApplication.Controllers
{
    [EnableCors("MyPolicyCors")]
    [Route("folder")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly Context _context;
        public FolderController(Context _context)
        {
            this._context = _context;
        }

        [HttpPost("addFolder")]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public IActionResult AddFolder(Folder model)
        {
            // var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "UserId").SingleOrDefault().Value;
            // var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();


            // var folderName = folderData["folderName"].ToString();
            // var folderPath = folderData["folderPath"].ToString();

            var datenow = DateTime.Now.Date;
            var date = datenow.ToString("dd/MM/yyyy");

            Folder newfolder = new Folder()
            {
                FolderName = model.FolderName,
                FolderSize = 0,
                //FolderPath = folderPath,
                // FolderOwner = user,
                ElementNumber = 0,
                DateOfCreate = DateTime.Now.Date
            };
            _context.Add(newfolder);
            _context.SaveChanges();
            return Ok(newfolder);
        }

        [HttpPost("addFolderIngroup")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public IActionResult addFolderIngroup([FromBody] JObject folderData)
        {
           // var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault().Value;
           // var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();
            var folderName = folderData["folderName"].ToString();
            var folderPath = folderData["folderPath"].ToString();
            var foldergroupid = int.Parse(folderData["foldergroupid"].ToString());
            var IdfolderParent = int.Parse(folderData["IdFolderParent"].ToString());
            var FolderParent = _context.Folder.Where(f => f.FolderId == IdfolderParent).FirstOrDefault();
            var foldergroup = _context.Group.Where(g => g.GroupId == foldergroupid).FirstOrDefault();

            Folder thenewfolder = new Folder()
            {
                FolderName = folderName,
                FolderPath = folderPath,
                ParentFolder = FolderParent,
               // FolderOwner = user,
                FolderGroup = foldergroup

            };
            _context.Add(thenewfolder);
            _context.SaveChanges();
            return Ok("newFolder");
        }

        [HttpGet("breadcrumb")]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public IActionResult Breadcrumb(int idfolder)
        {
            List<Folder> breadcrumb = new List<Folder>();
            var lastfolder = _context.Folder.Include(f => f.ParentFolder).Include(g => g.FolderGroup).Where(f => f.FolderId == idfolder).FirstOrDefault();
            findparent(lastfolder, breadcrumb);

            if (lastfolder.FolderGroup != null)

            {
                breadcrumb.Remove(breadcrumb[(breadcrumb.Count) - 1]);
                return Ok(breadcrumb);
            }
            return Ok(breadcrumb);
        }


        void findparent(Folder folder, List<Folder> breadcrumb)
        {
            if (folder != null)
            {
                breadcrumb.Add(folder);
                var fileparent = _context.Folder.Include(f => f.ParentFolder).Where(f => f == folder.ParentFolder).FirstOrDefault();
                findparent(fileparent, breadcrumb);
            }
        }
    }
}
