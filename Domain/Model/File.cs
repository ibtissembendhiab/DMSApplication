using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    [Table("Fichier")]
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
        public Statut FileStatut { get; set; }

    } 
 public enum Statut
    {
        notarchived,
        archived,
        locked,
        Unlocked
    }
}
