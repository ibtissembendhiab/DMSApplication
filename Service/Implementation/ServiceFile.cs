using Domain.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Service.Implementation
{
   /* public class ServiceFile 
    {
        private readonly IRepFile<File> _file ;
        public ServiceFile(IRepFile<File> file)
        {
            _file = file;
        }

        public ICollection<File> GetAllFiles()
        {
            try
            {
                return _file.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        } 
        //Delete 
        public bool Delete(int FileId)
        {

            try
            {
                var DataList = _file.GetAll().Where(x => x.FileId == FileId).ToList();
                foreach (var item in DataList)
                {
                    _file.Delete(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }*/
    
}
