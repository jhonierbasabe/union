using System;
using System.Collections.Generic;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using PRORAM.Models;
using PRORAM.Notifications;

namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase TargetAreaViewModel, controla la interación de la vista modal TargetAreaView
    /// </summary>
    public class TargetAreaViewModel : BindableBase, IInteractionRequestAware
    {
        #region private
        private TargetAreaModel _targetAreaModel = new TargetAreaModel();
        private ITargetAreaNotification _notification;
        private List<string> _Errors;
        private string _title;
        #endregion

        /// <summary>
        /// Propiedad Title, titulo
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        /// <summary>
        /// Metodo FlattenErrors, recore las propiedades y busca los errores de los campos
        /// </summary>
        /// <returns>lista de errores</returns>
        private List<string> FlattenErrors()
        {
            List<string> errors = new List<string>();
            Dictionary<string, List<string>> allErrors = TargetAreaMod.GetAllErrors();
            foreach (string propertyName in allErrors.Keys)
            {
                foreach (var errorString in allErrors[propertyName])
                {
                    errors.Add(propertyName + ": " + errorString);
                }
            }
            return errors;
        }

        /// <summary>
        /// propiedad Errors, lista de errores
        /// </summary>
        public List<string> Errors
        {
            get { return _Errors; }
            set { SetProperty(ref _Errors, value); }
        }
        /// <summary>
        /// Propiedad TargetAreaMod, modelo de la clase  TargetAreaModel con los campos para definir el area objetivo
        /// </summary>
        public TargetAreaModel TargetAreaMod
        {
            get { return _targetAreaModel; }
            set { SetProperty(ref _targetAreaModel, value); }
        }

        /// <summary>
        /// Propiedad  Notification, que contiene la notificación generada al lanzar la vista modal 
        /// </summary>
        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (ITargetAreaNotification)value); }
        }

        /// <summary>
        /// Constructor por defecto de la clase TargetAreaViewModel
        /// </summary>
        public TargetAreaViewModel()
        {
            TargetAreaMod.NombreArea = string.Empty;
            TargetAreaMod.ErrorsChanged += (s, e) => Errors = FlattenErrors();
            TargetAreaMod.PropertyChanged += (s, e) => Errors = FlattenErrors();
            SubmitCommand = new DelegateCommand(SubmitTargeArea);
            CancelCommand = new DelegateCommand(CancelInteraction);
            CustomPopupRequest = new InteractionRequest<INotification>();
            CustomPopupCommand = new DelegateCommand(RaiseCustomPopup);
        }



        /// <summary>
        /// Metodo CancelInteraction, cancela los cambios de la vista y cierra la vista modal
        /// </summary>
        private void CancelInteraction()
        {
            TargetAreaMod.NombreArea = string.Empty;
            _notification.TargetAreaModel = null;
            _notification.Confirmed = false;
            FinishInteraction?.Invoke();
        }
        /// <summary>
        /// Metodo SubmitTargeArea, acepta los cambios de la vista modal
        /// </summary>
        private void SubmitTargeArea()
        {
            TargetAreaMod.ValidateProperties();
            Errors = FlattenErrors();
            var count = DSconnection.DSConnection.CountDevices();
            var vCordenate = ValidateCordenate();
            int _sizeArea = 0;
            if (Errors.Count == 0)
            {
                _sizeArea = ValidateSizeArea();
            }
            bool isCorrectSize = (_sizeArea == 0) ? true : false;
            if (vCordenate == false)
            {
                CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "Ingrese los datos del formulario de manera correcta", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
            }
            if (!isCorrectSize && vCordenate)
            {
                if (_sizeArea == -1)
                {
                    CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "El área ingresada es muy pequeña ingrese un area objetivo entre \n90.000 metros cuadrados y 225 kilometros cuadrados", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
                }
                if (_sizeArea == 1)
                {
                    CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "El área ingresada es muy grande ingrese un area objetivo entre \n90.000 metros cuadrados y 225 kilometros cuadrados", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
                }
            }
            if (!TargetAreaMod.HasErrors && vCordenate && count == 0 && isCorrectSize)
            {
                RaiseCustomPopup();
                _notification.TargetAreaModel = TargetAreaMod;
                _notification.Confirmed = true;
                TargetAreaMod = new TargetAreaModel();
                FinishInteraction?.Invoke();

            }
            if (TargetAreaMod.HasErrors == false && count > 0)
            {
                CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "Ya hay un area definica con dispositivos radares agregados\n remueva los dispositivos si quiere volver a definir una nueva área", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
            }

        }


        private int ValidateSizeArea()
        {
            var gis = new GisLibrary.Proj4();

            double sizeArea = gis.CalculateARea(TargetAreaMod.LatitudP1.Value, TargetAreaMod.LongitudP1.Value, TargetAreaMod.LatitudP2.Value, TargetAreaMod.LongitudP2.Value);

            if (sizeArea < 90000)
            {
                return -1;
            }
            if (sizeArea > 225000000)
            {
                return 1;
            }
            if (sizeArea > 90000 && sizeArea < 225000000)
            {
                return 0;
            }
            return -1;

        }

        /// <summary>
        /// Metodo ValidateCordenate, valida las coordenas ingresadas por el usuario
        /// </summary>
        /// <returns>retorna una bantera booleana define si estan correctos o no</returns>
        private bool ValidateCordenate()
        {
            if (TargetAreaMod.LatitudP1 == null || TargetAreaMod.LatitudP2 == null || TargetAreaMod.LongitudP1 == null || TargetAreaMod.LongitudP2 == null)
            {
                return false;
            }
            if (TargetAreaMod.LatitudP1 == TargetAreaMod.LatitudP2 || TargetAreaMod.LatitudP1 < TargetAreaMod.LatitudP2 || TargetAreaMod.LongitudP1 == TargetAreaMod.LongitudP2 || TargetAreaMod.LongitudP1 > TargetAreaMod.LongitudP2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Metodo RaiseCustomPopup, lanza la vista modal que genera notificaciones de alerta
        /// </summary>
        private void RaiseCustomPopup()
        {
            var _titulo = "Mensaje de notificación";
            var _mensaje = "Área objetivo registrada satisfactoriamente";
            CustomPopupRequest.Raise(new Notification { Title = _titulo, Content = new { Text = _mensaje, Show = true, ShowAlert = false } }, r => Title = "PRORAM Consola de monitoreo");
        }
        /// <summary>
        /// Finaliza la interación de la vista
        /// </summary>
        public Action FinishInteraction { get; set; }

        #region delegados
        public DelegateCommand SubmitCommand { get; private set; }
        public InteractionRequest<INotification> CustomPopupRequest { get; set; }
        public DelegateCommand CustomPopupCommand { get; }
        public DelegateCommand CancelCommand { get; private set; }
        #endregion
    }
}
