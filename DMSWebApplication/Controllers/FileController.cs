using Domain.Data;
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
    }
}
