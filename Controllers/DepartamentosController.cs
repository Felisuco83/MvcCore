using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
using MvcCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class DepartamentosController : Controller
    {
        private IRepositoryDepartamentos repo;
        public DepartamentosController (IRepositoryDepartamentos repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View(this.repo.GetDepartamentos());
        }
        public IActionResult Details(int iddept)
        {
            return View(this.repo.BuscarDepartamento(iddept));
        }
        public IActionResult Delete(int iddept)
        {
            this.repo.EliminarDepartamento(iddept);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Departamento dept)
        {
            this.repo.InsertarDepartamento(dept);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int iddept)
        {
            return View(this.repo.BuscarDepartamento(iddept));
        }
        [HttpPost]
        public IActionResult Edit(Departamento dept)
        {
            this.repo.ModificarDepartamento(dept);
            return RedirectToAction("Index");
        }
    }
}
