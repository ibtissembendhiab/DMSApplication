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
   // [EnableCors("MyPolicyCors")]
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

        //GET filles 
        [HttpGet("GetAllFiles")]
        public List<File> GetAllFiles()
        {
            try
            {
                return _context.Files.ToList();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
       /* public IActionResult GetAllFiles()
        {
            try
            {
                var files = _context.Files.ToList();
                return Ok(new { data = new { files } });
                     
            }
            catch (Exception ex)
            {
                throw ;
            }
        }*/


          /*[HttpGet("searchfiles")]
          [Authorize]
          [AllowAnonymous]
          public IActionResult searchfiles()
          {
              var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "UserId").SingleOrDefault().Value;
              var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();

              var listfile = _context.file.Where(f => f.FileOwner == user).ToList();

              return Ok(listfile);
          } */


        //Delete File

        [HttpDelete("DeleteFile{FileId}")]
        public bool DeleteFile(int FileId)
        {
            try
            {
                File file = _file.GetById(FileId);
                _file.Delete(file);
                return true;
            }
            catch (Exception)
            {
                throw ;
            }

        }


    }
}
