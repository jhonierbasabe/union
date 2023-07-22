using System;
using Prism.Interactivity.InteractionRequest;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.Notifications
{
    public class ConsultParametersNotification : Confirmation, IConsultParametersNotification
    {
        public float TemperaturaAlimentacion { get; set; }
        public float TemperaturaProcesador { get; set; }
        public float TemperaturaAntena { get; set; }

        public ConsultParametersNotification()
        {
            this.TemperaturaAlimentacion = 0;
            this.TemperaturaProcesador = 0;
            this.TemperaturaAntena = 0;
        }
    }
}
