using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class MailService
    {
        private IConfiguration Configuration;
        private PathProvider PathProvider;
        private UploadService UpService;
        public MailService(IConfiguration configuration, PathProvider provider, UploadService upService)
        {
            this.Configuration = configuration;
            this.PathProvider = provider;
            this.UpService = upService;
        }

        public async Task SendEmail(string receptor, string asunto, string mensaje, IFormFile fichero)
        {
            MailMessage mail = new MailMessage();
            string usermail = this.Configuration["usuariomail"];
            string passwordmail = this.Configuration["passwordmail"];
            mail.From = new MailAddress(usermail);
            mail.To.Add(new MailAddress(receptor));
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            if (fichero != null)
            {
                //string filename = fichero.FileName;
                //string path = this.PathProvider.MapPath(filename, Folders.Temp);
                //using (var stream = new FileStream(path, FileMode.Create))
                //{
                //    await fichero.CopyToAsync(stream);
                //}
                string path = await this.UpService.UploadFile(fichero, Folders.Temp);
                Attachment attachment = new Attachment(path);
                mail.Attachments.Add(attachment);
            }
            string smtpserver = this.Configuration["host"];
            int port = int.Parse(this.Configuration["port"]);
            bool ssl = bool.Parse(this.Configuration["ssl"]);
            bool defaultcredentials = bool.Parse(this.Configuration["defaultcredentials"]);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = smtpserver;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
            smtpClient.UseDefaultCredentials = false;
            NetworkCredential usercredential = new NetworkCredential(usermail, passwordmail);
            smtpClient.Credentials = usercredential;
            smtpClient.Send(mail);
        }
    }
}
