using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.Notifications
{
    public interface IConsultParametersNotification : IConfirmation
    {
        float TemperaturaAlimentacion { get; set; }
        float TemperaturaProcesador { get; set; }
        float TemperaturaAntena { get; set; }

    }
}