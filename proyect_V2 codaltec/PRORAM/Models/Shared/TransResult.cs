using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.Models.Shared
{
    /// <summary>
    /// Clase TransResult, modelo de los eventos resultados de respuestas del radar a mensajes de la consola
    /// </summary>
    class TransResult
    {
        public byte[] Buffer { get; internal set; }
        public bool Result { get; internal set; }
        public string State { get; internal set; }
        public string Error { get; set; }

        public string Action { get; set; }
    }

}
