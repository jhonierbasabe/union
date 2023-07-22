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
            string Error = "";
            string NombreArea = "NombreArea:";
            string LatitudP2 = "LatitudP2:";
            string LongitudP1 = "LongitudP1:";
            string LongitudP2 = "LongitudP2:";
            string LatitudP1 = "LatitudP1:";
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
            if (Errors.Count > 0)
            {
                Error = Errors[0].ToString();
                if (Error.StartsWith(NombreArea))
                {
                    Error = Errors[0].ToString();
                }
                else
                {
                    Error = Errors[Errors.Count - 1].ToString();
                }
            }
            else
            {
                Error = "Sin Errores";
            }
            if (vCordenate == false)
            {
                if (Error.StartsWith(NombreArea))
                {
                    CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "Ingrese un nombre de area entre 4 y 20 caracteres de longitud", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
                }

                else if (Error.StartsWith(LatitudP1))
                {
                    CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "Ingrese un valor valido de Latitud del punto superior izquierdo, entre -180 y 180  ", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
                }

                else if (Error.StartsWith(LongitudP1))
                {
                    CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "Ingrese un valor valido de longitud del punto superior izquierdo, entre - 90 y 90 ", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
                }

                else if (Error.StartsWith(LatitudP2))
                {
                    CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "Ingrese un valor valido de Latitud del punto inferior derecho, entre -180 y 180  ", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
                }

                else if (Error.StartsWith(LongitudP2))
                {
                    CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "Ingrese un valor valido de longitud del punto inferior derecho, entre - 90 y 90 ", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
                }

                else
                {
                    if(TargetAreaMod.LatitudP1 == TargetAreaMod.LatitudP2)
                    {
                        CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "La latitud de los puntos no puede ser igual", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
                    }

                    else if (TargetAreaMod.LatitudP1 < TargetAreaMod.LatitudP2)
                    {
                        CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "La latitud del punto superior izquierdo tiene que ser mayor a la del punto inferior derecho", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
                    }

                    else if (TargetAreaMod.LongitudP1 == TargetAreaMod.LongitudP2)
                    {
                        CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "La longitud de los puntos no puede ser igual", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
                    }

                    else if (TargetAreaMod.LongitudP1 > TargetAreaMod.LongitudP2)
                    {
                        CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "La longitud del punto inferior derecho tiene que ser mayor a la del punto superior izquierdo", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
                    }
                }

                //CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "", Show = true, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
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
