using Domain.Data;
using Domain.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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

    }
}
