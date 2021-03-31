using Domain.Data;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class FileRepository : IRepFile<File>
    {
        
        Context _context;
        public FileRepository(Context context)
        {
            _context = context;
         }
        public void Delete(File FileId)
        {
            _context.Remove(FileId);
            _context.SaveChanges();
            
        }

        public ICollection<File> GetAll()
        {
            try
            {var f = _context.Files.ToList();
                return f;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public File GetById(int Id)
        {
            return _context.Files.Where(x => x.FileId == Id).FirstOrDefault();
        }

        public void Update(File _object)
        {
            _context.Files.Update(_object);
            _context.SaveChanges();
        }
    }
}
