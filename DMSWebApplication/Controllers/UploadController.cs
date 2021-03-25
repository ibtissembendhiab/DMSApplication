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
                var f = new File();
                var filePath = Path.Combine(@"ressources", file.FileName);

                // var FID = Request.Form.Files[1].ContentDisposition;
                // var folderName = Path.Combine("Resources", "Images");
               
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                  //  var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine( fileName);
                    var serverpath = Path.Combine( fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    //f.FileExtension = Path.GetExtension(serverpath);
                    f.FileName = fileName;
                    f.FilePath = serverpath;
                    // f.FileDiscription = "file uploaded succfully ";
                    f.FileSize = file.Length;
                    f.FileVersion = 1;

                   // var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault().Value;
                   // var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();

                   // var folder = _context.Folder.Where(folderid => folderid.FolderId == idfolder).FirstOrDefault();
                    //folder.ElementNumber = folder.ElementNumber += 1;
                    //folder.FolderSize = folder.FolderSize + file.Length;
                    //f.FileFolder = folder;
                   // f.FileOwner = user;
                    var datenow = DateTime.Now.Date;
                    var date = datenow.ToString("dd/MM/yyyy");
                    //  f.CreatedDate = date;
                    f.UploadDate = DateTime.Now.Date;
                    _context.Add(f);
                  //  _context.Update(folder);
                    _context.SaveChanges();


                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }


            }
        }

        /*[Route("api")]
         [ApiController]
         public class UploadController : ControllerBase
         {
            private IHostingEnvironment _hostingEnvironment;

             public UploadController(IHostingEnvironment environment)
             {
                 _hostingEnvironment = environment;
             }

             [HttpPost]
             [Route("upload")]
             public async Task<IActionResult> Upload(IFormFile file)
             {
                 var path = Path.GetFullPath(file.FileName); 

                 //var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                 //if (!Directory.Exists(uploads))
                 //{
                 //    Directory.CreateDirectory(uploads);
                 //}
                 if (file.Length > 0)
                 {
                     var filePath = Path.Combine(@"ressources", file.FileName);
                     using (var fileStream = new FileStream(filePath, FileMode.Create))
                     {
                         await file.CopyToAsync(fileStream);
                     }
                 }
                 return Ok();
             }*/







     

    }
}



