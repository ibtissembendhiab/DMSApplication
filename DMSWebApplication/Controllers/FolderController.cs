using Domain.Data;
using Domain.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous] 
        //AddFolder(Folder model)
        public IActionResult AddFolder([FromBody] JObject folderData)
        {
            var userIdClaim = HttpContext.User.Claims.Where(x => x.Type == "UserId").SingleOrDefault().Value;
            var user = _context.Users.Where(u => u.Id == userIdClaim).FirstOrDefault();


            var folderName = folderData["folderName"].ToString();
            var folderPath = folderData["folderPath"].ToString();

            var datenow = DateTime.Now.Date;
            var date = datenow.ToString("dd/MM/yyyy");
            Folder newfolder = new Folder()
            {
                FolderName = folderName,
                FolderSize = 0,
                FolderPath = folderPath,
                FolderOwner = user,
                ElementNumber = 0,
                DateOfCreate = DateTime.Now.Date
 };
            _context.Add(newfolder);
            _context.SaveChanges();
              return Ok(newfolder);
        }

    }
}
