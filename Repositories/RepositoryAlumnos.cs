using MvcCore.Helpers;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCore.Repositories
{
    public class RepositoryAlumnos
    {
        PathProvider pathprovider;
        private XDocument docxml;
        private string path;
        public RepositoryAlumnos (PathProvider pathprovider)
        {
            this.pathprovider = pathprovider;
            this.path = this.pathprovider.MapPath("alumnos.xml", Folders.Documents);
            this.docxml = XDocument.Load(path);
        }

        public List<Alumno> GetAlumnos()
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           select new Alumno
                           {
                               IdAlumno = int.Parse(datos.Element("idalumno").Value),
                               Nombre = datos.Element("nombre").Value,
                               Apellidos = datos.Element("apellidos").Value,
                               Nota = int.Parse(datos.Element("nota").Value)
                           };
            return consulta.ToList();
        }

        public Alumno BuscarAlumno (int idalumno)
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           where datos.Element("idalumno").Value == idalumno.ToString()
                           select new Alumno
                           {
                               IdAlumno = int.Parse(datos.Element("idalumno").Value),
                               Nombre = datos.Element("nombre").Value,
                               Apellidos = datos.Element("apellidos").Value,
                               Nota = int.Parse(datos.Element("nota").Value)
                           };
            return consulta.FirstOrDefault();
        }
        public void EliminarAlumno (int idalumno)
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           where datos.Element("idalumno").Value == idalumno.ToString()
                           select datos;
            XElement elementalumno = consulta.FirstOrDefault();
            elementalumno.Remove();
            this.docxml.Save(this.path);
        }
        public void InsertarAlumno(Alumno alumno)
        {
            XElement elementalumno = new XElement("alumno");
            XElement elementidalumno = new XElement("idalumno", alumno.IdAlumno);
            XElement elementnombre = new XElement("nombre", alumno.Nombre);
            XElement elementapellido = new XElement("apellidos", alumno.Apellidos);
            XElement elementnota = new XElement("nota", alumno.Nota);
            elementalumno.Add(elementidalumno);
            elementalumno.Add(elementnombre);
            elementalumno.Add(elementapellido);
            elementalumno.Add(elementnota);
            //debemos agregar el xelement al documento en la etiqueta que corresponda
            this.docxml.Element("alumnos").Add(elementalumno);
            this.docxml.Save(this.path);
        }

        public void ModificarAlumno (Alumno alumno)
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           where datos.Element("idalumno").Value == alumno.IdAlumno.ToString()
                           select datos;
            XElement element = consulta.FirstOrDefault();
            element.Element("nombre").Value = alumno.Nombre;
            element.Element("apellidos").Value = alumno.Apellidos;
            element.Element("nota").Value = alumno.Nota.ToString();
            this.docxml.Save(this.path);
        }
    }
}
