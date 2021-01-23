using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcCore.Data
{
    public class DepartamentosContextMySQL : DbContext
    {
        public DepartamentosContextMySQL(DbContextOptions<DepartamentosContextMySQL> options)
            : base(options) { }
        public DbSet<Departamento> Departamentos { get; set; }
    }
}
