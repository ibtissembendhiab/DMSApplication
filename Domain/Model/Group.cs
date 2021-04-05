﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
   public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public User GroupOwner { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Folder> GroupFolders { get; set; }


    }
}