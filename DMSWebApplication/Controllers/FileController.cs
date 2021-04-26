using Domain.Data;
using Domain.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using File = Domain.Model.File;

namespace DMSWebApplication.Controllers
{
   // [EnableCors("MyPolicyCors")]
    [Route("file")]
    [ApiController]
    public class FileController : ControllerBase
    {
       private readonly Context _context;
       private readonly IFile _serviceFile;

        public FileController(Context _context, IFile serviceFile)
        {
            this._serviceFile = serviceFile;
            this._context = _context;
        }

        //GET filles 
        [HttpGet("GetAllFiles")]
        public List<File> GetAll() => _serviceFile.GetAllFiles();


        //Delete File
        [HttpDelete("DeleteFile{FileId}")]
        public bool DeleteFile(int FileId) => _serviceFile.DeleteFile(FileId);
      
        //Archive File
        [HttpPost("archivefile")]
        [AllowAnonymous]     
        public async Task<IActionResult> Archivefile(int FileId)
        {
            var file = await _context.Files.FindAsync(FileId);

            file.FileStatut = Statut.archived;
            _context.Files.Update(file);
            await _context.SaveChangesAsync();
            return Ok("Archived");
        }

        //Restore archived files
        [HttpPost("restorefile")]
        [AllowAnonymous]
        public async Task<IActionResult> Restorefile(int FileId)
        {
            var file = await _context.Files.FindAsync(FileId);
            file.FileStatut = Statut.notarchived;
            _context.Files.Update(file);
            await _context.SaveChangesAsync();
            return Ok("restored");
        }

        [HttpGet("archivedfiles")]
        [AllowAnonymous]
        public async Task<IActionResult> Getarchivedfiles()
        {
            try
            { 
                var files = _context.Files.Include(f => f.FileOwner).Where(f => f.FileStatut == Statut.archived).ToList();
                return Ok(files);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }


       [HttpGet("searchfiles")]
       [Authorize]
       [AllowAnonymous]
       public IActionResult Searchfiles()
       {
         //  var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "UserId").SingleOrDefault().Value;
           var user = _context.Users.Where(u => u.Id == "userId").FirstOrDefault();
           var listefile = _context.Files.Where(f => f.FileOwner == user).ToList();

           return Ok(listefile);
       }

    }
}
