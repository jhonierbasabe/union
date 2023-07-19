using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.Models.Shared
{
    /// <summary>
    /// Clase RadarActions, Modelo para los eventos realizados sobre los dispositivos radar
    /// </summary>
    public class RadarActions
    {        
        public string Action { get; set; }
        public byte Idradar { get; set; }
        public RadarDevicesModel RadarDevice { get; set; }
        public Guid  GuidRadar;
        public string Logs { get; set; }
                
    }
}
