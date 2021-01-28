using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MvcCore.Helpers;

namespace MvcCore.Controllers
{
    public class HomeController : Controller
    {
        PathProvider pathProvider;
        IConfiguration Configuration;
        UploadService UpService;
        MailService MailService;
        public HomeController(PathProvider provider, IConfiguration config, UploadService upService, MailService mailService)
        {
            this.pathProvider = provider;
            this.Configuration = config;
            this.UpService = upService;
            this.MailService = mailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SubirFichero()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            await this.UpService.UploadFile(fichero, Folders.Images);
            ViewBag.Mensaje = "Archivo subido: ";
            return View();
        }
        public IActionResult EjemploMail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EjemploMail(string receptor, string asunto, string mensaje, IFormFile fichero)
        {
            await this.MailService.SendEmail(receptor, asunto, mensaje, fichero);
            return View();
        }

        public IActionResult CifradoHash ()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoHash(string contenido, string resultado, string accion)
        {
            var res = CypherService.EncriptarTextoBasico(contenido);
            if (accion.ToLower() == "cifrar")
            {
                ViewBag.Resultado = res;
            }
            else if (accion.ToLower() == "comparar")
            {
                if (resultado != res)
                {
                    ViewBag.Mensaje = "Resultados NO iguales";
                }
                else
                {
                    ViewBag.Mensaje = "Resultados iguales";
                }
            }
            return View();
        }
        public IActionResult CifradoHashEficiente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoHashEficiente(string contenido, int iteraciones, string salt, string resultado, string accion)
        {
            var res = CypherService.CifrarContenido(contenido,iteraciones, salt);
            if (accion.ToLower() == "cifrar")
            {
                ViewBag.Resultado = res;
            }
            else if (accion.ToLower() == "comparar")
            {
                if (resultado != res)
                {
                    ViewBag.Mensaje = "Resultados NO iguales";
                }
                else
                {
                    ViewBag.Mensaje = "Resultados iguales";
                }
            }
            return View();
        }
    }
}
