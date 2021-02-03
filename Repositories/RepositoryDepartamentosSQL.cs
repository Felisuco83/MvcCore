using Microsoft.Extensions.Caching.Memory;
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
        private HospitalContext context;
        private IMemoryCache Cache;
        public RepositoryDepartamentosSQL(HospitalContext context, IMemoryCache cache)
        {
            this.context = context;
            this.Cache = cache;
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
            List<Departamento> lista;
            if (this.Cache.Get("DEPARTAMENTOS") == null)
            {
                var consulta = from datos in this.context.Departamentos select datos;
                lista = consulta.ToList();
                this.Cache.Set("DEPARTAMENTOS", lista);
            } else
            {
                lista = this.Cache.Get("DEPARTAMENTOS") as List<Departamento>;
            }
            return lista;
            //CON ESTO VALDRIA
            //return this.context.Departamentos.ToList();
            //RECUPERAMOS DE MEMORYCACHE O DE SQL
        }

        public void InsertarDepartamento(Departamento dept, string imagen)
        {
            dept.Imagen = imagen;
            this.context.Departamentos.Add(dept);
            this.context.SaveChanges();
        }

        public void ModificarDepartamento(Departamento dept, string imagen)
        {
            Departamento depart = this.BuscarDepartamento(dept.Numero);
            depart.Nombre = dept.Nombre;
            depart.Localidad = dept.Localidad;
            depart.Imagen = imagen;
            this.context.SaveChanges();
        }
    }
}
