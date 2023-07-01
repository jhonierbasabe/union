using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PRORAM.Models;
using PRORAM.Models.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase LogsViewModel, controla la interación con la vista LogsView
    /// </summary>
    public class LogsViewModel : BindableBase
    {
        #region private
        private ObservableCollection<EventConsole> _listEvent;
        private IEventAggregator _ea;
        private IRegionManager _regionManager;
        #endregion

        /// <summary>
        /// Propiedad ListEvent, contiene la colección de eventos que se mostraran en el log de la consola
        /// </summary>
        public ObservableCollection<EventConsole> ListEvent
        {
            get { return _listEvent; }
            set { SetProperty(ref _listEvent, value); }
        }
        /// <summary>
        /// Constructor por defecto de la clase LogsViewModel
        /// </summary>
        /// <param name="regionManager">parametro de direcciónamiento de la vista</param>
        /// <param name="ea">objecto referencia a los eventos generados por la aplicación</param>
        public LogsViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _ea = ea;
            _ea.GetEvent<MessageSentEvent>().Subscribe(UpdateLogsEvent);
            _ea.GetEvent<SendEventDataSet>().Subscribe(UpdateLogsEvent);
            _ea.GetEvent<MsmSentEvent>().Subscribe(UpdateLogsEvent);

            _regionManager = regionManager;
            ListEvent = new ObservableCollection<EventConsole>();

            ListEvent.Add(new EventConsole() { TimeEvent = DateTime.Now, MessageEvent = "Consoloa de monitoreo radar PRORAM encendida" });

        }


        /// <summary>
        /// Metodo UpdateLogsEvent controla los eventos recividos de la consola
        /// </summary>
        /// <param name="obj">tipo de objecto que invoca el evento</param>
        private void UpdateLogsEvent(object obj)
        {
            
            if (obj is TargetAreaModel)
            {
                var logs = obj as TargetAreaModel;
                ListEvent.Insert(0, new EventConsole() { TimeEvent = DateTime.Now, MessageEvent = logs.LogsEvent });
                //ListEvent.Add(new EventConsole() { TimeEvent = DateTime.Now, MessageEvent = logs.LogsEvent });
            }
            if (obj is RadarActions)
            {
                var logs = obj as RadarActions;

                if (logs.Action != "Reset")
                {
                    ListEvent.Insert(0, new EventConsole() { TimeEvent = DateTime.Now, MessageEvent = logs.Logs });

                }

            }

        }
    }
}
