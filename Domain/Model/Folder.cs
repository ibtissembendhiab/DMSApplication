using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class Folder
    {
        [Key]
        public int FolderId { get; set; }
        public string FolderName { get; set; }
        public Double FolderSize { get; set; }
        public string FolderPath { get; set; }
        public User FolderOwner { get; set; }
        public int ElementNumber { get; set; }
        public DateTime DateOfCreate { get; set; }
        public List<File> Files { get; set; }

    }

}
