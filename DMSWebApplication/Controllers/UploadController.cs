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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;

namespace DMSWebApplication.Controllers
{
    [ApiController]
    [Route("api")]
    public class UploadController : ControllerBase
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UploadController(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("upload")]
         public IActionResult Upload(IFormFile file)
         {
             {

                 File f = new File();
                 var filePath = Path.Combine(@"ressources", file.FileName);

<<<<<<< HEAD
                 if (file.Length > 0)
                 {
                     var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                     //  var fullPath = Path.Combine(pathToSave, fileName);
                     var dbPath = Path.Combine(fileName);
                     var serverpath = Path.Combine(fileName);
                     using (var fileStream = new FileStream(filePath, FileMode.Create))
                     {
                         file.CopyTo(fileStream);
                     }

                     //f.FileExtension = Path.GetExtension(serverpath);
                     f.FileName = fileName;
                     f.FilePath = serverpath;

                     //f.FileDiscription = "file uploaded successfully ";
                     f.FileSize = file.Length;
                     f.FileVersion = 1;

                   // var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "UserId").SingleOrDefault().Value;
                   // var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();

                     // var folder = _context.Folder.Where(folderid => folderid.FolderId == idfolder).FirstOrDefault();
                     //folder.ElementNumber = folder.ElementNumber += 1;
                     //folder.FolderSize = folder.FolderSize + file.Length;
                     //f.FileFolder = folder;
                    //  f.FileOwner = user;


                     var datenow = DateTime.Now.Date;
                     var date = datenow.ToString("dd/MM/yyyy");

                     f.UploadDate = DateTime.Now.Date;
                     f.FileOwner = null;
                     f.FileFolder = null;
                     f.FileStatut = "";

                     _context.Add(f);

                     _context.SaveChanges();


                     return Ok(new { dbPath });
                 }
                 else
                 {
                     return BadRequest();
                 }


             }
         }
=======
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var dbPath = Path.Combine(fileName);
                    var serverpath = Path.Combine(fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    f.FileName = fileName;
                    f.FilePath = serverpath;
                    f.FileSize = file.Length;
                    f.FileVersion = 1;

                    // var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "UserId").SingleOrDefault().Value;
                    // var user = _context.Users.Where(u => u.Id == "UserId").FirstOrDefault();

                    var folder = _context.Folder.FirstOrDefault();
                    folder.ElementNumber = folder.ElementNumber += 1;
                    folder.FolderSize = folder.FolderSize + file.Length;
                    f.FileFolder = folder;
                   //f.FileOwner = user;
                    f.UploadDate = DateTime.Now.Date;
                    f.FileOwner = null;
                    f.FileFolder = null;
                    f.FileStatut = 0;

                    _context.Add(f);
                    _context.SaveChanges();

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
        }
>>>>>>> 8fb899ff33e8d57d8fd4eaad632117041bc679f4

        [HttpGet("download")]
        public async Task<IActionResult> Download([FromQuery] string FileName)
        {
            var filePath = Path.Combine(@"ressources", FileName);
            if (!System.IO.File.Exists(filePath))

                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);  
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), FileName);
        }
            private string GetContentType(string path)
            {
                var provider = new FileExtensionContentTypeProvider();
                string contentType;
                if (!provider.TryGetContentType(path, out contentType))
                {
                    contentType = "application/octet-stream";
                }
                return contentType;
            }
    }
}




