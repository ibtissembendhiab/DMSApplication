using Domain.Data;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Common;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.IO;
=======
using System.IdentityModel.Tokens.Jwt;
>>>>>>> 793b3482870c69992dc705c43f31dee41b0d9643
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DMSWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IRepository<User> _repository;
        private Context _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager, Context context, IRepository<User> repository)
        {
            _repository = repository;
            _userManager = userManager;
            _context = context;
        }
<<<<<<< HEAD

        



    }
=======


       
    }


>>>>>>> 793b3482870c69992dc705c43f31dee41b0d9643
}
