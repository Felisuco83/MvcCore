using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class CypherService
    {
        public static string EncriptarTextoBasico(string contenido)
        {
            //NECESITAMOS TRABAJAR A NIVEL DE BYTE, DEBEMOS CONVERTIR A BYTE[] EL CONTENIDO DE ENTRADA QUE NOSOTROS TENGAMOS
            byte[] entrada;
            //EL CIFRADO SE REALIZA A NIVEL DE BYTE[] Y DEVOLVERA OTRO ARRAY DE BYTES DE SALIDA
            byte[] salida;
            //NECESITAMOS UN CONVERSOR PARA TRANSFORMAR BYTE[] A STRING Y VICEVERSA
            UnicodeEncoding encoding = new UnicodeEncoding();
            //necesitamos el objeyto que se encarga de realizar el cifrado
            SHA1Managed sha = new SHA1Managed();
            //DEBEMOS CONVERTIR EL CONTENIDO DE ENTRADA A BYTE[]
            entrada = encoding.GetBytes(contenido);
            //EL OBJETO SHA1MANAGED TIENE UN METODO PARA DEVOLVER LOS BYTES DE SALIDA REALIZANDO EL CIFRADO
            salida = sha.ComputeHash(entrada);
            string res = encoding.GetString(salida);
            return res;
        }
        public static String GetSalt()
        {
            Random random = new Random();
            String salt = "";
            for (int i = 1; i <= 50; i++)
            {
                int aleat = random.Next(0, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }
        public static byte[] CifrarContenido (string contenido, string salt)
        {
            string contenidosalt = contenido + salt;
            SHA256Managed sha = new SHA256Managed();
            byte[] salida;
            salida = Encoding.UTF8.GetBytes(contenidosalt);
            //CIFRAMOS EL NUMERO DE ITERACCIONES QUE NOS INDIQUEN
            for (int i=1; i<=101; i++)
            {
                salida = sha.ComputeHash(salida);
            }
            sha.Clear();
            return salida;
        }

        public static string CifrarContenido(string contenido,int iteraciones, string salt)
        {
            return "";
        }
    }
}
