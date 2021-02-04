using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Caching.Memory;
using MvcCore.Data;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public class RepositoryHospitalSql : IRepositoryHospital
    {
        HospitalContext context;
        IMemoryCache Cache;
        public RepositoryHospitalSql (HospitalContext context, IMemoryCache cache)
        {
            this.context = context;
            this.Cache = cache;
        }
        #region departamentos
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
            List<Departamento> lista;
            if (this.Cache.Get("DEPARTAMENTOS") == null)
            {
                var consulta = from datos in this.context.Departamentos select datos;
                lista = consulta.ToList();
                this.Cache.Set("DEPARTAMENTOS", lista);
            }
            else
            {
                lista = this.Cache.Get("DEPARTAMENTOS") as List<Departamento>;
            }
            return lista;
        }

        public void ModificarDepartamento(Departamento dept)
        {
            Departamento depart = this.BuscarDepartamento(dept.Numero);
            depart.Nombre = dept.Nombre;
            depart.Localidad = dept.Localidad;
            this.context.SaveChanges();
        }
        #endregion
        #region departamentos
        public List<Empleado> GetEmpleados()
        {
            return this.context.Empleados.ToList();
        }

        public List<Empleado> BuscarEmpleadosDepartamentos(List<int> iddepartamentos)
        {
            return (from datos in this.context.Empleados where iddepartamentos.Contains(datos.Departamento) select datos).ToList();
        }

        public void InsertarDepartamento(Departamento dept, string imagen)
        {
            dept.Imagen = imagen;
            this.context.Departamentos.Add(dept);
            this.context.SaveChanges();
        }

        public void ModificarDepartamento(Departamento dept, string imagen)
        {
            throw new NotImplementedException();
        }

        public List<Empleado> GetEmpleadosSession(List<int> idEmpleados)
        {
            var consulta = this.context.Empleados.Where(x => idEmpleados.Contains(x.IdEmpleado));
            return consulta.ToList();
        }
        #endregion
    }
}
