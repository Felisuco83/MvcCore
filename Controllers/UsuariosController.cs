using Microsoft.AspNetCore.Mvc;
using MvcCore.Helpers;
using MvcCore.Models;
using MvcCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class UsuariosController : Controller
    {
        RepositoryUsuarios repo;
        public UsuariosController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registrar(int idusuario, string nombre, string username, string password)
        {
            this.repo.InsertarUsuario(idusuario, nombre, username, password);
            ViewBag.Mensaje = "Datos almacenados";
            return View();
        }

        public IActionResult Credenciales()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Credenciales(string username, string password)
        {
            Usuario user = this.repo.UserLogin(username, password);
            if (user == null)
            {
                ViewBag.Mensaje = "Usuario/Password no validos";
            }else
            {
                ViewBag.Mensaje = "Credenciales correctas";
            }
            return View();
        }
    }
}
