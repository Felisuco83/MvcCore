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
using MvcCore.Extensions;
using MvcCore.Helpers;
using MvcCore.Models;

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

        //public IActionResult EjemploSession(string accion)
        //{
        //    if  (accion == "almacenar")
        //    {
        //        Persona person = new Persona();
        //        person.Nombre = "Alumno";
        //        person.Edad = 27;
        //        person.Hora = DateTime.Now.ToLongTimeString();
        //        byte[] data = HelperToolkit.ObjectToByteArray(person);
        //        HttpContext.Session.Set("persona", data);
        //        ViewBag.Mensaje = "Datos almacenados en Session";
        //    } else if(accion == "mostrar")
        //    {
        //        byte[] data = HttpContext.Session.Get("persona");
        //        Persona person = HelperToolkit.ByteArrayToObject(data) as Persona;
        //        ViewBag.Autor = person.Nombre;
        //        ViewBag.Hora = person.Hora;
        //        ViewBag.Mensaje = "Mostrando datos";
        //    }
        //    return View();
        //}
        //public IActionResult MultiplesPersonas(string accion)
        //{
        //    if (accion == "almacenar")
        //    {
        //        List<Persona> severalPersons = new List<Persona>();
        //        Persona persona1 = new Persona();
        //        Persona persona2 = new Persona();
        //        persona1.Nombre = "Alumno";
        //        persona1.Edad = 27;
        //        persona1.Hora = DateTime.Now.ToLongTimeString();
        //        persona2.Nombre = "Alumno 2";
        //        persona2.Edad = 28;
        //        persona2.Hora = DateTime.Now.ToLongTimeString();
        //        severalPersons.Add(persona1);
        //        severalPersons.Add(persona2);
        //        byte[] data = HelperToolkit.ObjectToByteArray(severalPersons);
        //        HttpContext.Session.Set("personas", data);
        //        ViewBag.Mensaje = "Datos almacenados en Session";
        //    }
        //    else if (accion == "mostrar")
        //    {
        //        byte[] data = HttpContext.Session.Get("personas");
        //        List<Persona> person = HelperToolkit.ByteArrayToObject(data) as List<Persona>;
        //        ViewBag.Autor = person[1].Nombre;
        //        ViewBag.Hora = person[1].Hora;
        //        ViewBag.Mensaje = "Mostrando datos";
        //    }
        //    return View();
        //}

        public IActionResult EjemploSession(string accion)
        {
            if (accion == "almacenar")
            {
                Persona person = new Persona();
                person.Nombre = "Alumno";
                person.Edad = 27;
                person.Hora = DateTime.Now.ToLongTimeString();
                HttpContext.Session.SetObject("persona", person);
                ViewBag.Mensaje = "Datos almacenados en Session";
            }
            else if (accion == "mostrar")
            {
                Persona person = HttpContext.Session.GetObject<Persona>("persona");
                ViewBag.Autor = person.Nombre;
                ViewBag.Hora = person.Hora;
                ViewBag.Mensaje = "Mostrando datos";
            }
            return View();
        }

        public IActionResult MultiplesPersonas(string accion)
        {
            if (accion == "almacenar")
            {
                List<Persona> severalPersons = new List<Persona>();
                Persona persona1 = new Persona();
                Persona persona2 = new Persona();
                persona1.Nombre = "Alumno";
                persona1.Edad = 27;
                persona1.Hora = DateTime.Now.ToLongTimeString();
                persona2.Nombre = "Alumno 2";
                persona2.Edad = 28;
                persona2.Hora = DateTime.Now.ToLongTimeString();
                severalPersons.Add(persona1);
                severalPersons.Add(persona2);
                string data = HelperToolkit.SerializeJsonObject(severalPersons);
                HttpContext.Session.SetString("personas", data);
                ViewBag.Mensaje = "Datos almacenados en Session";
            }
            else if (accion == "mostrar")
            {
                string data = HttpContext.Session.GetString("personas");
                List<Persona> person = HelperToolkit.DeSerializeJsonObject<List<Persona>>(data);
                ViewBag.Autor = person[1].Nombre;
                ViewBag.Hora = person[1].Hora;
                ViewBag.Mensaje = "Mostrando datos";
            }
            return View();
        }
    }
}
