using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Models
{
    [Serializable]
    public class Persona
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Hora { get; set; }
    }
}
