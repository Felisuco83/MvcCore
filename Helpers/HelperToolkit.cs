using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class HelperToolkit
    {
        public static bool CompararArrayBytes(byte[] a, byte[]b)
        {
            bool iguales = true;
            if (a.Length != b.Length)
            {
                iguales = false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if(a[i].Equals(b[i]) == false)
                {
                    iguales = false;
                    break;
                }
            }
            return iguales;
        }

        public string SanitizeFileName(string filename)
        {
            var removeChar = false;
            for (var i=0; i< filename.Length; i++)
            {
                if(Char.IsSymbol(filename[i])){
                    removeChar = true;
                }
                if (Char.IsWhiteSpace(filename[i]))
                {
                    removeChar = true;
                }
                if (Char.IsPunctuation(filename[i]))
                {
                    if (filename[i] != '.' || (filename[i] == '.' && filename.LastIndexOf('.') != i))
                    {
                        removeChar = true;
                    }
                }
                if(removeChar)
                    filename.Remove(i, 1);
            }
            return filename;
        }
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }

        }

        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                Object obj = (Object)binForm.Deserialize(memStream);
                return obj;
            }
        }

        //METODO QUE RECIBIRA UN OBJETO Y LOTRANSFORMARA A STRING JSON 
        public static string SerializeJsonObject (object objeto)
        {
            return JsonConvert.SerializeObject(objeto);
        }

        public static T DeSerializeJsonObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
