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
        private UserManager<User> _userManager;
        //private SignInManager<User> _signInManager;
        private readonly ApplicationSettings _appSettings;
        
        public ServiceAuthen(
            UserManager<User> userManager,
           // SignInManager<User> signInManager,
            IOptions<ApplicationSettings> appSettings
            


                )
        {
            _userManager = userManager;
           // _signInManager = signInManager;
            _appSettings = appSettings.Value;
            
        }
        public async Task<string> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                        {
                             new Claim("UserID",user.Id.ToString())
                        }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return  token ;
            }
            else return null;
        }

        public async Task<object> Register(RegisterModel model)
        {
            var cUser = new User
            {
               // FirstName = model.FirstName,
               // LastName  = model.LastName,
                UserName  = model.Username,
                Email     = model.Email,
            };

            try
            {
                var result = await _userManager.CreateAsync(cUser, model.Password);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
