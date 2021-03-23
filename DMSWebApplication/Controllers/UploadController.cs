using Domain.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Domain.Model;
using File = Domain.Model.File;
using Service.Implementation;

namespace DMSWebApplication.Controllers
{
     

        [ApiController]
        //[EnableCors("MyPolicyCors")]
        [Route("api/upload")]
        public class UploadController : ControllerBase
        {
            private readonly Context _context;
            private readonly IHttpContextAccessor _httpContextAccessor;
           //private readonly ServiceFile _serviceFile;
           //ServiceFile serviceFile 
            public UploadController(Context context, IHttpContextAccessor httpContextAccessor )
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
               // _serviceFile = serviceFile;
            }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var files = Request.Form.Files;
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (files.Any(f => f.Length == 0))
                {
                    return BadRequest();
                }
                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName); //you can add this path to a list and then return all dbPaths to the client if require
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok("All the files are successfully uploaded.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // [HttpPost, DisableRequestSizeLimit]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[AllowAnonymous]
        // public async Task<string> Upload([FromForm] int idfolder) => await _serviceFile.Upload(idfolder);


        /* public async Task<IActionResult> Upload([FromForm] int idfolder)
        {
            try
            {
                var f = new File();
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Images");
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

                    var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault().Value;
                    var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();

                    var folder = _context.Folder.Where(folderid => folderid.FolderId == idfolder).FirstOrDefault();

                    folder.ElementNumber = folder.ElementNumber += 1;
                    folder.FolderSize = folder.FolderSize + file.Length;
                    f.FileFolder = folder;
                    f.FileOwner = user;

                    var datenow = DateTime.Now.Date;
                    var date = datenow.ToString("dd/MM/yyyy");

                    f.UploadDate = DateTime.Now.Date;
                    _context.Add(f);
                   _context.Update(folder);
                    _context.SaveChanges();

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        } */
    }
    }
