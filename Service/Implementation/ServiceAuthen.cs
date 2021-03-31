using Domain.Model;
using Microsoft.AspNetCore.Identity;
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
       // private SignInManager<User> _signInManager;
        private readonly ApplicationSettings _appSettings;
       // private readonly RoleManager<IdentityResult> _roleManager;

        public ServiceAuthen(
            UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher,

        //  SignInManager<User> signInManager,
        IOptions<ApplicationSettings> appSettings
                )

        {
            _passwordHasher = passwordHasher;
            _userManager = userManager;
           // _signInManager = signInManager;
            _appSettings = appSettings.Value;
            
        }
        public async Task<string> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            //var role = await _userManager.GetRolesAsync(user);
            //var v = role[0];
            //var signingKey = Convert.FromBase64String(_appSettings.JWT_Secret);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                           
                    Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("UserId", user.Id.ToString()),
                          //  new Claim("Role", v)

                        }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };

                 var tokenHandler = new JwtSecurityTokenHandler();
                 var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                 var token = tokenHandler.WriteToken(securityToken);
                return token;
            }
            else return null;
        }

        public async Task<object> Register(RegisterModel model)
        {
            var cuser = new User
            {
               // FirstName = model.FirstName,
                //LastName  = model.LastName,
                UserName  = model.Username,
                Email     = model.Email,
            };

            try
            {
               // var r = await _userManager.AddToRoleAsync(cuser, "Employee");
                var result = await _userManager.CreateAsync(cuser, model.Password);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Object> Update(string id, User model)
        {
            User user = await _userManager.FindByIdAsync(id);
           // var pwCheck = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (user != null)
            {

                //if (model.FullName != null) { user.FullName = model.FullName; }

                if (model.UserName != "" && model.UserName != user.UserName) { user.UserName = model.UserName; }

                if (model.Email != "" && model.Email != user.Email) { user.Email = model.Email; }


               // if (model.Password != "")
               // {
               //   if (pwCheck != PasswordVerificationResult.Failed)
               //     user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
               // }


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

    }
}
