using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Common;
using System;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Group = Domain.Model.Group;
using Domain.Data;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Domain.Model;

namespace DMSWebApplication.Controllers
{
    // [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api")]

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

