using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Extensions
{
    public static class Class
    {
        public static void SetObject (this ITempDataDictionary tempdata, string key, Object data)
        {
            tempdata[key] = JsonConvert.SerializeObject(data);
        }
        public static T GetObject<T> (this ITempDataDictionary tempdata, string key)
        {
            if (tempdata[key] == null)
                return default(T);
            return JsonConvert.DeserializeObject<T>(tempdata[key].ToString());
        }
    }
}
