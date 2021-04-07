using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class Register
    {
       

        [Required]
        public string FirstName { get; set; }
         public string LastName { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // [Required]
        //[DataType(DataType.Password)]
        //[Compare("Password")]
        //public string ConfirmPassword { get; set; }

       // [DisplayName("Role")]
       // [Required(ErrorMessage = "Choose Role")]
        public string Role { get; set; }

       // public RegisterModel(string _firstName, string _lastName, string _userName, string _email, string _password, string _role)

       // {
            //FirstName = _firstName;
            //LastName = _lastName;
          //  Username = _userName;
          //  Email = _email;
           // Password = _password;
           // Role = _role;

       // }
        public Register() { }
    }
}


/*        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)

 *             Task.Run(() => this.CreateRoles(roleManager)).Wait();

 * private async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (string rol in this.Configuration.GetSection("Roles").Get<List<string>>())
            {
                if (!await roleManager.RoleExistsAsync(rol))
                {
                    await roleManager.CreateAsync(new IdentityRole(rol));
                }
            }
        }
*/