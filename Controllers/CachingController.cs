using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache MemoryCache;
        public CachingController (IMemoryCache memoryCache) 
        {
            this.MemoryCache = memoryCache;
        }
        public IActionResult HoraSistema(int? tiempo)
        {
            if(tiempo == null)
            {
                tiempo = 5;
            }
            string fecha = DateTime.Now.ToShortDateString()+", "+ DateTime.Now.ToLongTimeString();
            if(this.MemoryCache.Get("FECHA") == null)
            {
                this.MemoryCache.Set("FECHA", fecha, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.GetValueOrDefault())));
                ViewBag.Fecha = this.MemoryCache.Get("Fecha");
                ViewBag.Mensaje = "Almacenando en Cache";
            } else
            {
                fecha = this.MemoryCache.Get("FECHA").ToString();
                ViewBag.Mensaje = "Recuperando de Cache";
                ViewBag.Fecha = fecha;
            }
            ViewBag.Fecha = fecha;
            return View();
        }

        [ResponseCache(Duration = 15 , VaryByQueryKeys = new string[] { "*"})]
        public IActionResult HoraSistemaDistribuida(string dato)
        {
            string fecha = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString();
            ViewBag.Fecha = fecha;
            return View();
        }
    }
}
