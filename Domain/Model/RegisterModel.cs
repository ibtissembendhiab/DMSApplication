using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // [Required]
        //[DataType(DataType.Password)]
        //[Compare("Password")]
        //public string ConfirmPassword { get; set; }
        public string UserRole { get; set; }

        public RegisterModel(string _firstName, string _lastName, string _userName, string _email, string _password, string _userRole)

        {
            FirstName = _firstName;
            LastName = _lastName;
            Username = _userName;
            Email = _email;
            Password = _password;
            UserRole = _userRole;

        }
        public RegisterModel() { }
    }
}
