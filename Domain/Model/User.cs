﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string UserName { get; set;  }

        //public string PassWord { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public override string Email { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Folder> Folders { get; set; }

        public User() { }


    }
}
