using MvcCore.Helpers;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCore.Repositories
{
    public class RepositoryDepartamentosXML: IRepositoryDepartamentos
    {
        PathProvider pathprovider;
        private XDocument docxml;
        private string path;
        public RepositoryDepartamentosXML(PathProvider pathprovider)
        {
            this.pathprovider = pathprovider;
            this.path = this.pathprovider.MapPath("departamentos.xml", Folders.Documents);
            this.docxml = XDocument.Load(path);
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.docxml.Descendants("DEPARTAMENTO")
                           select new Departamento
                           {
                               Numero = int.Parse(datos.Attribute("NUMERO").Value),
                               Nombre = datos.Element("NOMBRE").Value,
                               Localidad = datos.Element("LOCALIDAD").Value,
                           };
            return consulta.ToList();
        }

        public Departamento BuscarDepartamento(int iddept)
        {
            var consulta = from datos in this.docxml.Descendants("DEPARTAMENTO")
                           where datos.Attribute("NUMERO").Value == iddept.ToString()
                           select new Departamento
                           {
                               Numero = int.Parse(datos.Attribute("NUMERO").Value),
                               Nombre = datos.Element("NOMBRE").Value,
                               Localidad = datos.Element("LOCALIDAD").Value,
                           };
            return consulta.FirstOrDefault();
        }
        public void EliminarDepartamento(int iddept)
        {
            var consulta = from datos in this.docxml.Descendants("DEPARTAMENTO")
                           where datos.Attribute("NUMERO").Value == iddept.ToString()
                           select datos;
            XElement elementalumno = consulta.FirstOrDefault();
            elementalumno.Remove();
            this.docxml.Save(this.path);
        }
        public void InsertarDepartamento(Departamento dept)
        {
            XElement elementdepartamento = new XElement("DEPARTAMENTO");
            XAttribute numdept = new XAttribute("NUMERO", dept.Numero);
            XElement elementnombre = new XElement("NOMBRE", dept.Nombre);
            XElement elementlocalidad = new XElement("LOCALIDAD", dept.Localidad);
            elementdepartamento.Add(numdept);
            elementdepartamento.Add(elementnombre);
            elementdepartamento.Add(elementlocalidad);

            this.docxml.Element("DEPARTAMENTOS").Add(elementdepartamento);
            this.docxml.Save(this.path);
        }

        public void ModificarDepartamento(Departamento dept)
        {
            var consulta = from datos in this.docxml.Descendants("DEPARTAMENTO")
                           where datos.Attribute("NUMERO").Value == dept.Numero.ToString()
                           select datos;
            XElement element = consulta.FirstOrDefault();
            element.Attribute("NUMERO").Value = dept.Numero.ToString();
            element.Element("NOMBRE").Value = dept.Nombre;
            element.Element("LOCALIDAD").Value = dept.Localidad;
            this.docxml.Save(this.path);
        }

        public void InsertarDepartamento(Departamento dept, string imagen)
        {
            throw new NotImplementedException();
        }

        public void ModificarDepartamento(Departamento dept, string imagen)
        {
            throw new NotImplementedException();
        }
    }
}
