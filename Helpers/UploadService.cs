using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class UploadService
    {
        private PathProvider PathProvider;
        public UploadService(PathProvider provider)
        {
            this.PathProvider = provider;
        }

        public async Task<string> UploadFile (IFormFile fichero, Folders folder) 
        {
            string filename = fichero.FileName;
            string path = this.PathProvider.MapPath(filename, folder);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            return path;
        }
    }
}
