using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
   public interface IFile
    {
        List<File> GetAllFiles();
        bool DeleteFile(int FileId);
        //Task<IActionResult> Archivefile(int FileId);
    }
}
