using MvcCore.Data;
using MvcCore.Helpers;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public class RepositoryUsuarios
    {
        HospitalContext context;
        public RepositoryUsuarios(HospitalContext context)
        {
            this.context = context;
        }
        public void InsertarUsuario(int idusuario, string nombre, string username, string password)
        {
            Usuario user = new Usuario();
            user.IdUsuario = idusuario;
            user.Nombre = nombre;
            user.UserName = username;
            string salt = CypherService.GetSalt();
            user.Salt = salt;
            byte[] respuesta = CypherService.CifrarContenido(password, salt);
            user.Password = respuesta;
            this.context.Usuarios.Add(user);
            this.context.SaveChanges();
        }

        public Usuario UserLogin (string username, string password)
        {
            Usuario user = this.context.Usuarios.Where(x => x.UserName == username).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            string salt = user.Salt;
            byte[] passddbb = user.Password;
            byte[] passuser = CypherService.CifrarContenido(password, salt);
            bool respuesta = HelperToolkit.CompararArrayBytes(passddbb, passuser);
            if (respuesta)
            {
                return user;
            } else
            {
                return null;
            }
        }
    }
}
