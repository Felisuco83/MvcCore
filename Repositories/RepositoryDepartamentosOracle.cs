using MvcCore.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public class RepositoryDepartamentosOracle : IRepositoryDepartamentos
    {
        OracleDataAdapter addept;
        DataTable tabladept;
        OracleCommandBuilder builder;
        public RepositoryDepartamentosOracle(string cadenaoracle)
        {
            this.addept = new OracleDataAdapter("select * from dept", cadenaoracle);
            this.builder = new OracleCommandBuilder(addept);
            this.tabladept = new DataTable();
            this.addept.Fill(this.tabladept);
        }
        public Departamento BuscarDepartamento(int iddept)
        {
            var consulta = from datos in this.tabladept.AsEnumerable()
                           where datos.Field<int>("DEPT_NO") == iddept
                           select new Departamento
                           {
                               Numero = datos.Field<int>("DEPT_NO"),
                               Nombre = datos.Field<string>("DNOMBRE"),
                               Localidad = datos.Field<string>("LOC")
                           };
            return consulta.FirstOrDefault();
        }

        private DataRow GetDataRowId(int iddept)
        {
            DataRow fila = this.tabladept.AsEnumerable()
                .Where(z => z.Field<int>("DEPT_NO") == iddept).FirstOrDefault();
                return fila;
        }

        public void EliminarDepartamento(int iddept)
        {
            //PARA ELIMINAR DEBEMOS HACERLO CON COMMANDO O SOBRE EL OBJETO DATATABLE
            //BUSCAMOS LA FILA (DataRow) QUE CORRESPONDA CON EL ID, LA FILA TIENE UN MÉTODO DELETE QUE MARCARÁ EN LA TABLA EL VALOR PARA ELIMINAR 
            //POSTERIORMENTE EL ADAPTADOR AL IGUAL QUE TIENE UN MÉTODO PARA TRAER LOS DATOS (fill) HAY UNO PARA AUTOMATIZAR LOS CAMBIOS (update)
            DataRow row = this.GetDataRowId(iddept);
            row.Delete();
            this.addept.Update(tabladept);
            this.tabladept.AcceptChanges();
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.tabladept.AsEnumerable()
                           select new Departamento
                           {
                               Numero = datos.Field<int>("DEPT_NO"),
                               Nombre = datos.Field<string>("DNOMBRE"),
                               Localidad = datos.Field<string>("LOC")
                           };
            return consulta.ToList();
        }

        public void InsertarDepartamento(Departamento dept)
        {
            DataRow row = this.tabladept.NewRow();
            row["DEPT_NO"] = dept.Numero;
            row["DNOMBRE"] = dept.Nombre;
            row["LOC"] = dept.Localidad;
            this.tabladept.Rows.Add(row);
            this.addept.Update(this.tabladept);
            this.tabladept.AcceptChanges();
        }

        public void ModificarDepartamento(Departamento dept)
        {
            DataRow row = this.GetDataRowId(dept.Numero);
            row["DNOMBRE"] = dept.Nombre;
            row["LOC"] = dept.Localidad;
            this.addept.Update(this.tabladept);
            this.tabladept.AcceptChanges();
        }
    }
}
