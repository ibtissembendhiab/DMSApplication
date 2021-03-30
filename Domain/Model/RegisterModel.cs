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

    }
}
