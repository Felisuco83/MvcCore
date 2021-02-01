using Microsoft.AspNetCore.Http;
using MvcCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Extensions
{
    public static class SessionExtension
    {
        //DEBEMOS RECIBIR COMO PRIMER PARÁMETRO OBLIGATORIO EL OBJETO QUE ESTAMOS EXTENDIENDO
        public static void SetObject (this ISession session, String key, object value)
        {
            //CUANDO ALMACENAMOS ALGO EN SESSION QUE NECESITAMOS? => KEY Y VALUE
            string data = HelperToolkit.SerializeJsonObject(value);
            session.SetString(key, data);
        }

        public static T GetObject<T> (this ISession session, string key)
        {
            //TENEMOS UN JSON GUARDADO, DEBEMOS DEVOLVER EL OBJETO MAPEADO
            string data = session.GetString(key);
            if(data == null)
            {
                return default(T);
            }
            return HelperToolkit.DeSerializeJsonObject<T>(data);
        }
    }
}
