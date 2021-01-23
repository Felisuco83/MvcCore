using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
using MvcCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class AlumnosController : Controller
    {
        RepositoryAlumnos repo;
        public AlumnosController(RepositoryAlumnos repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View(this.repo.GetAlumnos());
        }
        public IActionResult Details (int idalumno)
        {
            return View(this.repo.BuscarAlumno(idalumno));
        }
        public IActionResult Delete(int idalumno)
        {
            this.repo.EliminarAlumno(idalumno);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create (Alumno alumno)
        {
            this.repo.InsertarAlumno(alumno);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int idalumno)
        {
            return View(this.repo.BuscarAlumno(idalumno));
        }
        [HttpPost]
        public IActionResult Edit(Alumno alumno)
        {
            this.repo.ModificarAlumno(alumno);
            return RedirectToAction("Index");
        }
    }
}
