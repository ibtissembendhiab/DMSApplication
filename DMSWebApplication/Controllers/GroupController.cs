using Domain.Data;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using File = Domain.Model.File;

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

            //var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault().Value;
            //var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();

            // group.GroupOwner = user;
            group.CreatedDate = DateTime.Now.Date;

            Folder parent = _context.Folder.Where(f => f.FolderId == 1).FirstOrDefault();

              Folder Foldercontainer = new Folder()
              {
                  FolderName = group.GroupName + "checkpoint",
                  FolderPath = group.GroupName + "/",
                 // FolderGroup = group,
                 // FolderOwner = user,
                  ParentFolder = parent,
                  DateOfCreate = DateTime.Now.Date
              };
            // _context.Folder.Add(Foldercontainer);

            if (ModelState.IsValid)
            {
                _context.Group.Add(group);
                _context.SaveChanges();

                return Ok("group was added ");
            }

            return Ok("adding group was failed");
        }

        [HttpPost("deletegroup")]
        [AllowAnonymous]

        public async Task<IActionResult> Deletegroup(int id)
        {
            var group = await _context.Group.Include(g => g.GroupOwner).Where(g => g.GroupId == id).FirstAsync();
            var files = _context.Files.Where(f => f.FileFolder.FolderGroup.GroupId == id).ToList();
            foreach (var f in files)
            {
                _context.Files.Remove(f);

            }
            var folders = _context.Folder.Where(f => f.FolderGroup.GroupId == id).ToList();
            foreach (var f in folders)
            {
                _context.Folder.Remove(f);

            }
            var relations = _context.GroupUser.Where(r => r.GroupId == id).ToList();

            foreach (var r in relations)
            {
                _context.GroupUser.Remove(r);

            }
            _context.Group.Remove(group);
            await _context.SaveChangesAsync();
            return Ok("deleted");
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

        

        [HttpPost("uploadfileingroup"), DisableRequestSizeLimit]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult UploadfileInGroup(IFormFile file,[FromForm] int idfolder, int idgroup)
        {
                var f = new File();
                // var file = Request.Form.Files[0];
                // var FID = Request.Form.Files[1].ContentDisposition;
                var folderName = Path.Combine("ressources");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    var serverpath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    f.FileName = fileName;
                    f.FilePath = serverpath;
                    f.FileSize = file.Length;
                    f.FileVersion = 1;

                  //  var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault().Value;
                   // var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();

                    //var idfolder = Request.Form.files;

                    var folder = _context.Folder.Where(f => f.FolderId == idfolder && f.FolderGroup.GroupId == idgroup).FirstOrDefault();
                    f.FileFolder = folder;
                   // f.FileOwner = user;
                    f.UploadDate = DateTime.Now;

                    _context.Add(f);
                    _context.SaveChanges();


                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            
        }

        [HttpGet("filesingroup")]
        [AllowAnonymous]
        public async Task<IActionResult> GetfilesInGroup(int idgroup, int idfolder)
        {
            try
            {
               // var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault().Value;
              //  var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();

                //  var folder=_context.Folder.Where(f=>f.FolderId== idfolder).FirstOrDefault();
                // var f = _context.File.ToList();

                var files = _context.Files.Where(f => f.FileFolder.FolderGroup.GroupId == idgroup && f.FileFolder.FolderId == idfolder && f.FileStatut == Statut.notarchived).ToList();
                foreach (var file in files)
                {
                    file.FileFolder = _context.Folder.Where(f => f.FolderId == idfolder).SingleOrDefault();

                }
                return Ok(files);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("foldersingroup")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<IActionResult> GetfoldersIngroup(int idfolder, int idgroup)
        {
            try
            {
               // var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault().Value;
               // var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();
                // var folder=_context.Folder.Where(f=>f.FolderID== idfolder).FirstOrDefault();
                // var f = _context.File.ToList();
                // var ismyspce = _context.Folder.Where(f => f.ParentFolder.FolderId == 1).SingleOrDefault(); 
                var folders = _context.Folder.Include(f => f.FolderOwner).Where(f => f.ParentFolder.FolderId == idfolder && f.FolderGroup.GroupId == idgroup).ToList();
                foreach (var f in folders)
                {
                    f.FolderGroup = _context.Group.Where(f => f.GroupId == idgroup).SingleOrDefault(); ;
                    f.ParentFolder = _context.Folder.Where(f => f.FolderId == idfolder).SingleOrDefault();

                }
                return Ok(folders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
