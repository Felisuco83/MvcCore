using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Helpers;
using MvcCore.Models;
using MvcCore.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class DepartamentosController : Controller
    {
        private IRepositoryHospital repo;
        PathProvider provider;
        public DepartamentosController (IRepositoryHospital repo, PathProvider provider)
        {
            this.repo = repo;
            this.provider = provider;
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
        public async Task<IActionResult> Create(Departamento dept, IFormFile ficheroimagen)
        {
            if(ficheroimagen != null)
            {
                string filename = ficheroimagen.FileName;
                string path = this.provider.MapPath(filename, Folders.Images);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ficheroimagen.CopyToAsync(stream);
                }
            }
            this.repo.InsertarDepartamento(dept,ficheroimagen.FileName);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int iddept)
        {
            return View(this.repo.BuscarDepartamento(iddept));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Departamento dept, IFormFile ficheroimagen)
        {
            if (ficheroimagen != null)
            {
                string filename = ficheroimagen.FileName;
                string path = this.provider.MapPath(filename, Folders.Images);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ficheroimagen.CopyToAsync(stream);
                }
            }
            this.repo.ModificarDepartamento(dept, ficheroimagen.FileName);
            return RedirectToAction("Index");
        }

        public IActionResult SeleccionMultiple ()
        {
            List<Departamento> departamentos = this.repo.GetDepartamentos();
            List<Empleado> empleados = this.repo.GetEmpleados();
            ViewData["DEPARTAMENTOS"] = departamentos;
            return View(empleados);
        }

        [HttpPost]
        public IActionResult SeleccionMultiple(List<int> iddepartamentos)
        {
            List<Departamento> departamentos = this.repo.GetDepartamentos();
            List<Empleado> empleados = this.repo.BuscarEmpleadosDepartamentos(iddepartamentos);
            ViewData["DEPARTAMENTOS"] = departamentos;
            return View(empleados);
        }
    }
}
