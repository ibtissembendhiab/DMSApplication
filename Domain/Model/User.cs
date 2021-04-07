using Domain.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class User : IdentityUser
    {
        //public override string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string UserName { get; set;  }

       // public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public override string Email { get; set; }

        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Folder> Folders { get; set; }

        public List<IdentityUserRole<string>> Roles { get; set; }

        public User() { }

      
    }
}
