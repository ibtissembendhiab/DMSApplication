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
        public Folder ParentFolder { get; set; }

        public int ElementNumber { get; set; }

        public DateTime DateOfCreate { get; set; }

       // public List<File> Files { get; set; }

        public virtual ICollection<Folder> ChildFolders { get; set; }

        public ICollection<File> Files { get; set; }

        public Folder(ICollection<File> Files)
        {
            this.Files = Files;
        }
        public Folder(Folder f, ICollection<Folder> childF)
        {
            ParentFolder = f;
            ChildFolders = childF;
        }
        public Folder(Folder f)
        {
            ParentFolder = f;
        }
        public Folder()
        {
            this.ChildFolders = new List<Folder>();


        }
    }

}
