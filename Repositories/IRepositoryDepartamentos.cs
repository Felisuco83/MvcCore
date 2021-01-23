using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public interface IRepositoryDepartamentos
    {

        List<Departamento> GetDepartamentos();

        Departamento BuscarDepartamento(int iddept);

        void EliminarDepartamento(int iddept);

        void InsertarDepartamento(Departamento dept);

        void ModificarDepartamento(Departamento dept);

    }
}
