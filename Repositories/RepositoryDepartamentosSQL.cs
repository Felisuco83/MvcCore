using MvcCore.Data;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public class RepositoryDepartamentosSQL: IRepositoryDepartamentos
    {
        private DepartamentosContext context;
        public RepositoryDepartamentosSQL(DepartamentosContext context)
        {
            this.context = context;
        }

        public Departamento BuscarDepartamento(int iddept)
        {
            return this.context.Departamentos.Where(x => x.Numero == iddept).FirstOrDefault();
        }

        public void EliminarDepartamento(int iddept)
        {
            Departamento departamento = this.BuscarDepartamento(iddept);
            this.context.Departamentos.Remove(departamento);
            this.context.SaveChanges();
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.context.Departamentos select datos;
            return consulta.ToList();
            //CON ESTO VALDRIA
            //return this.context.Departamentos.ToList();
        }

        public void InsertarDepartamento(Departamento dept)
        {
            this.context.Departamentos.Add(dept);
            this.context.SaveChanges();
        }

        public void ModificarDepartamento(Departamento dept)
        {
            Departamento depart = this.BuscarDepartamento(dept.Numero);
            depart.Nombre = dept.Nombre;
            depart.Localidad = dept.Localidad;
            this.context.SaveChanges();
        }
    }
}
