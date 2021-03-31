using Domain.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public FileController(Context _context)
        {
            this._context = _context;
        }

        [HttpGet("searchfiles")]
        [Authorize]
        [AllowAnonymous]
        public IActionResult searchfiles()
        {
            var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "UserId").SingleOrDefault().Value;
            var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();

            var listfile = _context.File.Where(f => f.FileOwner == user).ToList();

            return Ok(listfile);
        }

        [HttpPost("deletefile")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteFile(int FileId)
        {
            var file = await _context.File.FindAsync(FileId);

            _context.File.Remove(file);
            await _context.SaveChangesAsync();
            return Ok("File Deleted successfully");
        }

    }
}
