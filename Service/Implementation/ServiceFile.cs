using Domain.Data;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Model.File;

namespace Service.Implementation
{
    public class ServiceFile : IFile
    {
        private Context _context;
        private readonly IRepFile<File> _file;
        public ServiceFile(Context context, IRepFile<File> file)
        {
            _context = context;
            _file = file;
        }

        public bool DeleteFile(int FileId)
        {
            try
            {
                File file = _file.GetById(FileId);
                _file.Delete(file);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<File> GetAllFiles()
        {
            try
            {
                return _context.Files.Include(f => f.FileOwner).Where(f => f.FileStatut == Statut.notarchived).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /*public async Task<IActionResult> Archivefile(int FileId)
      {
          var file = await _context.Files.FindAsync(FileId);

          file.FileStatut = Statut.archived;
          _context.Files.Update(file);
          await _context.SaveChangesAsync();
          return null;
      }*/
    }
}
