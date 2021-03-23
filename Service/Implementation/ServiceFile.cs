using Domain.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
   /* public class ServiceFile : IFile
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ServiceFile(Context context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public object Request { get; private set; }

        public async Task<string> Upload([FromForm] int idfolder)
        {
            try
            {
                var f = new File();

                var file = FormCollection.Files.First();
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

                    return dbPath;
                } 
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }  
    } */
}
