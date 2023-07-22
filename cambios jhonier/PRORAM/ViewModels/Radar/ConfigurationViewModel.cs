﻿using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using PRORAM.DataValidation;
using PRORAM.Models;
using PRORAM.Notifications;
using PRORAM.ResourcesFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PRORAM.Models.RadarConfigurationModel;

namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase ConfigurationViewModel, controla la interación con la vista ConfigurationView
    /// </summary>
    public class ConfigurationViewModel : ValidatableBindableBase, IInteractionRequestAware
    {


        #region private
        private IRadarConfigurationNotification _notification;
        private RadarConfigurationModel _radarConfigurationMod;
        private IList<SelectedItem> _Errors;
        private string _tituloError;
        private string _mensaje;
        private ObservableCollection<Channels> _channel;
        private ObservableCollection<Objetivo> _objetivo;
        #endregion

        /// <summary>
        /// Propiedad Channels, colección de canales de frecuencia
        /// </summary>
        public ObservableCollection<Channels> Channels
        {
            get { return _channel; }
            set { SetProperty(ref _channel, value); }
        }


        //Agrege
        public ObservableCollection<Objetivo> Objetivo
        {
            get { return _objetivo; }
            set { SetProperty(ref _objetivo, value); }
        }

        //Fin
        /// <summary>
        /// Propiedad RadarConfigurationModel, modelo de radar configuración para la vista
        /// </summary>
        public RadarConfigurationModel RadarConfigurationMod
        {
            get { return _radarConfigurationMod; }
            set { SetProperty(ref _radarConfigurationMod, value); }
        }



        /// <summary>
        /// Metodo que busca por errores en las propiedades del modelo
        /// </summary>
        /// <returns> lista con los errores encontrados</returns>
        private IList<SelectedItem> FlattenErrors()
        {
            IList<SelectedItem> errors = new List<SelectedItem>();
            Dictionary<string, List<string>> allErrors = RadarConfigurationMod.GetAllErrors();
            foreach (string propertyName in allErrors.Keys)
            {
                foreach (var errorString in allErrors[propertyName])
                {
                    //,propertyName + ": " + errorString
                    errors.Add(
                        new SelectedItem
                        {
                            Property = propertyName,
                            MessageError = propertyName + ": " + errorString
                        });
                }
            }

            return errors;
        }


        /// <summary>
        /// Propiedad Errors, contiene los errores de los campos de la vista
        /// </summary>
        public IList<SelectedItem> Errors
        {
            get { return _Errors; }
            set { SetProperty(ref _Errors, value); }
        }
        /// <summary>
        /// Contructor de la clase ConfiguractionViewModel
        /// </summary>
        public ConfigurationViewModel()
        {
            Channels = new ObservableCollection<Channels>();
            Objetivo = new ObservableCollection<Objetivo>();
            RadarConfigurationMod = new RadarConfigurationModel();
            RadarConfigurationMod.ErrorsChanged += (s, e) => Errors = FlattenErrors();
            CustomPopupRequest = new InteractionRequest<INotification>();
            CustomPopupCommand = new DelegateCommand(RaiseCustomPopup);
            OnLoadScreenCommand = new DelegateCommand(OnLoadScreen);
            CancelInteractionCommand = new DelegateCommand(CancelInteraction);

            SubmitCommand = new DelegateCommand(Submit);
        }

        /// <summary>
        /// Propiedad OnLoadScreen, configurala vista cada vez que le carga
        /// </summary>
        private void OnLoadScreen()
        {
            Channels = new ObservableCollection<Channels>();
            Objetivo = new ObservableCollection<Objetivo>();
            RadarConfigurationMod = _notification.RadarConfigurationModelI_;
            RadarConfigurationMod.Elevation = 0;
            RadarConfigurationMod.TXPower = _notification.RadarConfigurationModelI_.TXPower.Value;
            RadarConfigurationMod.SchannelFrec = _notification.RadarConfigurationModelI_.SchannelFrec;
            RadarConfigurationMod.SchannelObject = _notification.RadarConfigurationModelI_.SchannelObject;
            RadarConfigurationMod.ChannelFrec = new ObservableCollection<Channels>();
            RadarConfigurationMod.ChannelObject = new ObservableCollection<Objetivo>();
            var modelos = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ModelosRardar;

            foreach (var i in modelos.Modelo2.ChannelFrec)
            {
                RadarConfigurationMod.ChannelFrec.Add(new Channels
                {
                    Id = i.Channel,
                    Frecuency = i.Frec,
                    DisplayName = i.Channel + " - " + i.Frec + " GHz"
                });
            }

            //Agrege
            foreach (var i in modelos.Modelo2.ChannelObject)
            {
                RadarConfigurationMod.ChannelObject.Add(new Objetivo
                {
                    Value = i.value,
                    Object = i.objecto,
                    DisplayNameO = i.value + " - " + i.objecto
                });
            }

            //Fin

            var chan = RadarConfigurationMod.ChannelFrec.Where(x => x.Frecuency == RadarConfigurationMod.SchannelFrec.Frecuency).FirstOrDefault();
            var chano = RadarConfigurationMod.ChannelObject.Where(x => x.Object == RadarConfigurationMod.SchannelObject.Object).FirstOrDefault();

            RadarConfigurationMod.SchannelFrec = chan;
            RadarConfigurationMod.SchannelObject = chano;
        }
        /// <summary>
        /// Metodo CancelInteraction, cancel los cambios en la vista y la cierra
        /// </summary>
        private void CancelInteraction()
        {

            _notification.RadarConfigurationModelI_ = null;
            _notification.Confirmed = false;

            FinishInteraction?.Invoke();
        }

        /// <summary>
        /// Propiedad FinishInteraction, controla las notificaciones de incio y cierre de la vista
        /// </summary>
        public Action FinishInteraction { get; set; }
        /// <summary>
        /// Propiedad Notification
        /// </summary>
        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (IRadarConfigurationNotification)value); }
        }

        public InteractionRequest<INotification> CustomPopupRequest { get; set; }

        public DelegateCommand CustomPopupCommand { get; set; }
        public DelegateCommand CancelInteractionCommand { get; set; }
        public DelegateCommand OnLoadScreenCommand { get; set; }
        public DelegateCommand SubmitCommand { get; set; }
        public string Tittle { get; private set; }

        /// <summary>
        /// Propiedad Submit, acepta los cambios y cierra la vista modal
        /// </summary>
        private void Submit()
        {
            RadarConfigurationMod.ValidateProperties();
            Errors = FlattenErrors();
            var Altitude = Errors.Where(p => p.Property.Contains("Altitude"));
            var TXPower = Errors.Where(p => p.Property.Contains("TXPower"));
            var InstallationAngle = Errors.Where(p => p.Property.Contains("InstallationAngle"));
            var NorthHeiding = Errors.Where(p => p.Property.Contains("NorthHeiding"));
            var ChannelFrec = Errors.Where(p => p.Property.Contains("SchannelFrec"));
            var Latitud = Errors.Where(p => p.Property.Contains("Latitud"));
            var Longitud = Errors.Where(p => p.Property.Contains("Longitud"));
            if (!RadarConfigurationMod.HasErrors)
            {
                CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "Se ha configurado los parámetros del radar de forma exitosa", Show = true, ShowAlert = false } }, r => Tittle = "PRORAM Consola de monitoreo");
                _tituloError = "";
                _mensaje = "";

                RadarConfigurationMod.TXPower = RadarConfigurationMod.TXPower.Value;
                _notification.RadarConfigurationModelI_ = RadarConfigurationMod;
                _notification.Confirmed = true;
                RadarConfigurationMod = new RadarConfigurationModel();
                FinishInteraction?.Invoke();
            }
            else
            {
                _tituloError = "Alerta";

                if (Altitude.Count() != 0)
                {
                    _mensaje = "Ingrese un valor valido de altitud, entre 1 y 15";
                }

                else if (TXPower.Count() != 0)
                {
                    _mensaje = "Ingrese un valor valido de potencia de transmision, entre 0 y 100";
                }

                else if (InstallationAngle.Count() != 0)
                {
                    _mensaje = "Ingrese un valor valido de angulo de instalacion, entre -30 y 30";
                }

                else if (NorthHeiding.Count() != 0)
                {
                    _mensaje = "Ingrese un valor valido de NorthHeiding, entre 0 y 360";
                }

                else if (ChannelFrec.Count() != 0)
                {
                    _mensaje = "El campo canal de frecuencia es obligatorio";
                }

                else if (Latitud.Count() != 0)
                {
                    _mensaje = "Ingrese un valor valido de Latitud, entre -180 y 180  ";
                }

                else if (Longitud.Count() != 0)
                {
                    _mensaje = "Ingrese un valor valido de longitud, entre -90 y 90 ";
                }

                RaiseCustomPopup();
                _tituloError = "";
                _mensaje = "";
            }
        }

        /// <summary>
        /// Metodo RaiseCustomPopup, invoca la ventana de notificación
        /// </summary>
        private void RaiseCustomPopup()
        {
            CustomPopupRequest.Raise(new Notification { Title = _tituloError, Content = new { Text = _mensaje, Show = false } }, r => Tittle = "PRORAM Consola de monitoreo");
        }
    }
}
