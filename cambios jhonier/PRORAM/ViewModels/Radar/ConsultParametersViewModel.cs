using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using PRORAM.Models;
using PRORAM.Notifications;
using PRORAM.ResourcesFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.ViewModels
{
    class ConsultParametersViewModel : BindableBase, IInteractionRequestAware
    {
        private float _TemperaturaAlimentacion;
        private float _TemperaturaProcesador;
        private float _TemperaturaAntena;
        public Action FinishInteraction { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        private bool setTx;

        private IConsultParametersNotification _notification;

        public float TemperaturaAlimentacion
        {
            get { return _TemperaturaAlimentacion; }
            set { SetProperty(ref _TemperaturaAlimentacion, value); }
        }

        public float TemperaturaProcesador
        {
            get { return _TemperaturaProcesador; }
            set { SetProperty(ref _TemperaturaProcesador, value); }
        }

        public float TemperaturaAntena
        {
            get { return _TemperaturaAntena; }
            set { SetProperty(ref _TemperaturaAntena, value); }
        }

        public ConsultParametersViewModel()
        {

            setTx = true;
            this.PropertyChanged += (s, e) => SetContent();
            CancelCommand = new DelegateCommand(CancelInteraction);

        }

        private void SetContent()
        {
            if (setTx == true)
            {
                TemperaturaAlimentacion = _notification.TemperaturaAlimentacion;
                TemperaturaProcesador = _notification.TemperaturaProcesador;
                TemperaturaAntena = _notification.TemperaturaAntena;

                setTx = false;
            }
        }

        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (IConsultParametersNotification)value); }
        }

        private void CancelInteraction()
        {
            setTx = true;
            FinishInteraction?.Invoke();
        }



    }
}
