using Domain.Data;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DMSWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private Context _context;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminController(UserManager<IdentityUser> userManager, Context context)
        {
            _userManager = userManager;
            _context = context;
        }

        



    }
}
