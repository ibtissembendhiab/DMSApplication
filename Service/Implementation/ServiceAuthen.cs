using Domain.Data;
using Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ServiceAuthen : IAuth
    {
        private IPasswordHasher<User> _passwordHasher;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly ApplicationSettings _appSettings;
        private readonly RoleManager<IdentityRole> _roleManager;
        private Context _context;

        public ServiceAuthen(
            UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher,
            RoleManager<IdentityRole> roleManager,
         SignInManager<User> signInManager,
        IOptions<ApplicationSettings> appSettings,
        Context context
                )

        {
            _passwordHasher = passwordHasher;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _context = context;

        }
        public async Task<object> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            //var role = await _userManager.GetRolesAsync(user);
            //var v = role[0];
            // var signingKey = Convert.FromBase64String(_appSettings.JWT_Secret);
            //var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (user == null)
            {
                return null;

            }
            // var result = await _userManager.CheckPasswordAsync(user, model.Password);

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return "Password Incorrect";
                //return "Password not correct";
            }

            return (new { 
                result = result,
                token = JwtTokenGenerator(user)
           });      
        }

        private async Task<string> JwtTokenGenerator(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var roles =await _userManager.GetRolesAsync(user);

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

            }

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = credentials

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<object> Register(Register model)
        {
           // model.Role = "Admin";
            var cuser = new User
            {
                FirstName = model.FirstName,
                LastName  = model.LastName,
                UserName  = model.Username,
                Email     = model.Email,
            };
            var result = await _userManager.CreateAsync(cuser, model.Password);

            var dbUser = await _userManager.FindByNameAsync(cuser.UserName);
            await _userManager.AddToRoleAsync(dbUser, model.Role);

            if (!result.Succeeded)
            {
                return "failed";
            }
       
            if (!_context.Folder.Any())
            {
                Folder folderMySpace = new Folder()
                {
                    FolderName = "My space",
                    FolderPath = "",
                    DateOfCreate = DateTime.Now.Date,
                    FolderOwner= cuser
                };

                _context.Folder.Add(folderMySpace);
                _context.SaveChanges();
            }
            return result;
        }

        public async Task<Object> Update(string id, UserUpdate model)
        {
            User user = await _userManager.FindByIdAsync(id);
            var pwCheck = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (user != null)
            {
                if (model.FirstName != "" && model.FirstName != user.FirstName) { user.FirstName = model.FirstName; }

                if (model.LastName != "" && model.LastName != user.LastName) { user.LastName = model.LastName; }

                if (model.UserName != "" && model.UserName != user.UserName) { user.UserName = model.UserName; }

                if (model.Email != "" && model.Email != user.Email) { user.Email = model.Email; }
                


                if (model.Password != "" && model.Password!= user.PasswordHash)
               {
                    if (pwCheck != PasswordVerificationResult.Failed)
                    { user.PasswordHash = _passwordHasher.HashPassword(user, model.Password); }
                }

                IdentityResult result = await _userManager.UpdateAsync(user);
                return result;
            }
            else
            {
                return null;
            }

        }
        public async Task<Object> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);

                }
                return user;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<User> GetAll()
        {
            try
            {
                return _context.Users.OrderBy(x => x.FirstName).ToList();
            }
            catch (Exception ex)
            {
                //ErrorManager.ErrorHandler.HandleError(ex);
                throw;
            }
        }
    }
}
