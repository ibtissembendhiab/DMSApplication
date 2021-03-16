using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class File
    {
        [Key]
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Double FileSize { get; set; }
        public int FileVersion { get; set; }
        public DateTime UploadDate { get; set; }
        public User FileOwner { get; set; }
        public Folder FileFolder { get; set; } 
        public statut FileStatut { get; set; }

        public File()
        {

        }
  
    }
    public enum statut
    {
        notarchived,
        archived,
        locked,
        Unlocked
    }
}
