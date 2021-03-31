using Domain.Data;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSWebApplication.Controllers
{
    [EnableCors("MyPolicyCors")]
    [Route("file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly Context _context;
        private readonly IRepFile<File> _file;


        public FileController(Context _context, IRepFile<File> file)
        {
            this._context = _context;
            this._file = file;
        }

        //GET All Person  
        [HttpGet("GetAllFiles")]
        public IActionResult  GetAllFiles()
       // public Object GetAllFiles()
        {
            try
            {
                var files = _context.Files.ToList();
                return Ok(new { data = new { files } });
                      _file.GetAll().ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /*  [HttpGet("searchfiles")]
          [Authorize]
          [AllowAnonymous]
          public IActionResult searchfiles()
          {
              var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "UserId").SingleOrDefault().Value;
              var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();

              var listfile = _context.File.Where(f => f.FileOwner == user).ToList();

              return Ok(listfile);
          }*/

        //Delete File
        //[HttpDelete("{id}")]

        [HttpDelete("DeleteFile")]
        public bool DeleteFile(int FileId)
        {
            try
            {
                var DataList = _file.GetAll().Where(x => x.FileId == FileId).ToList();
                foreach (var item in DataList)
                {
                    _file.Delete(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }

        /*  [HttpPost("deletefile")]
           [AllowAnonymous]

           public async Task<IActionResult> DeleteFile(int FileId)
            {
                var file = await _context.File.FindAsync(FileId);

                _context.File.Remove(file);
                await _context.SaveChangesAsync();
                return Ok("File Deleted successfully");
            }*/

    }
}
