using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using PRORAM.DataValidation;
using PRORAM.Models;
using PRORAM.Notifications;
using PRORAM.ResourcesFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
//using static PRORAM.Models.RadarConfigurationModel;
namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase RadarDevicesViewModel, contiene la logica y metodos que controlan la vista RadarDevicesView
    /// </summary>
    public class RadarConfigurationViewModel : ValidatableBindableBase, IInteractionRequestAware
    {
        #region Private
        private RadarConfigurationModel _RadarConfigurationModel;
        private IEventAggregator _ea;
        private IList<SelectedItem> _Errors;
        private IRadarConfigurationNotification _notification;
        private string _tittle;
        private string _tituloError;
        private object _mensaje;
        private bool _modeloSelected;
        #endregion


        /// <summary>
        /// Propiedad ModeloSelected, define si esta seleccionado un modelo de radar        
        /// </summary>
        public bool ModeloSelected
        {
            get { return _modeloSelected; }
            set { SetProperty(ref _modeloSelected, value); }
        }

        /// <summary>
        /// Propiedad Title, titulo
        /// </summary>
        public string Tittle
        {
            get { return _tittle; }
            set { SetProperty(ref _tittle, value); }
        }


        /// <summary>
        /// Metodo que busca por errores en las propiedades del modelo
        /// </summary>
        /// <returns> lista con los errores encontrados</returns>
        private IList<SelectedItem> FlattenErrors()
        {
            IList<SelectedItem> errors = new List<SelectedItem>();
            Dictionary<string, List<string>> allErrors = RadarConfigurationModel_.GetAllErrors();
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
        /// Propiedad Errors, lista de errores ocuridos de la vista
        /// </summary>
        public IList<SelectedItem> Errors
        {
            get { return _Errors; }
            set { SetProperty(ref _Errors, value); }
        }

        public DelegateCommand SetIdCommand { get; }
        public DelegateCommand SubmitCommand { get; set; }
        public DelegateCommand SubmitRadarCommand { get; }

        public DelegateCommand SelectionChangedCommand { get; set; }

        /// <summary>
        /// Propiedad RadarConfigurationModel, modelo para la vista
        /// </summary>
        public RadarConfigurationModel RadarConfigurationModel_
        {
            get { return _RadarConfigurationModel; }
            set { SetProperty(ref _RadarConfigurationModel, value); }
        }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public RadarConfigurationViewModel(IEventAggregator ea)
        {

            _ea = ea;
            RadarConfigurationModel_ = new RadarConfigurationModel();
            RadarConfigurationModel_.Azimuth = 2;
            RadarConfigurationModel_.ChannelFrec = new ObservableCollection<Channels>();
            RadarConfigurationModel_.ChannelObject = new ObservableCollection<Objetivo>();
            ModeloSelected = false;
            RadarConfigurationModel_.Port = 0;
            RadarConfigurationModel_.Elevation = 0;
            RadarConfigurationModel_.ErrorsChanged += (s, e) => Errors = FlattenErrors();
            GetListModel();
            SetIdCommand = new DelegateCommand(SetId);
            SubmitCommand = new DelegateCommand(SubmitInsRadar);
            SubmitRadarCommand = new DelegateCommand(SubmitRadar);
            CancelCommand = new DelegateCommand(CancelInteraction);

            SelectionChangedCommand = new DelegateCommand(SelectionChanged);
            CustomPopupRequest = new InteractionRequest<INotification>();
            CustomPopupCommand = new DelegateCommand(RaiseCustomPopup);
            OnLoadScreenCommand = new DelegateCommand(OnLoadScreen);
            RadarConfigurationModel_.PropertyChanged += CheckSpanader;
        }


        /// <summary>
        /// Metodo CheckSpanader, revisa si las campos de entrada de la vista son los correctos para expandir el panel
        /// </summary>
        /// <param name="sender">objeto que activa el evento</param>
        /// <param name="e">argunmento de cambio de propiedad</param>
        private void CheckSpanader(object sender, PropertyChangedEventArgs e)
        {
            if (RadarConfigurationModel_.IpAddress == null || RadarConfigurationModel_.IpAddress == string.Empty || RadarConfigurationModel_.IpAddress == "0.0.0.0" ||
                RadarConfigurationModel_.SModelo == null || RadarConfigurationModel_.RadarName == null || RadarConfigurationModel_.RadarName == string.Empty)
            {
                RadarConfigurationModel_.Expander = false;
            }

        }
        /// <summary>
        /// Metodo OnLoadScreen, metodo que configura la vista una ves se ha cargado
        /// </summary>
        private void OnLoadScreen()
        {
            ConfigurationData();
        }
        /// <summary>
        /// Metodo ConfigurationData, configuracion de los datos de la vista
        /// </summary>
        private void ConfigurationData()
        {
            if (RadarConfigurationModel_ == null)
            {
                RadarConfigurationModel_ = new RadarConfigurationModel();
                RadarConfigurationModel_.ChannelFrec = new ObservableCollection<Channels>();
                RadarConfigurationModel_.ChannelObject = new ObservableCollection<Objetivo>();


                var modelos = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ModelosRardar;

                RadarConfigurationModel_.ChannelFrec.Clear();
                RadarConfigurationModel_.ChannelObject.Clear();

                foreach (var i in modelos.Modelo2.ChannelFrec)
                {
                    RadarConfigurationModel_.ChannelFrec.Add(new Channels
                    {
                        Id = i.Channel,
                        Frecuency = i.Frec,
                        DisplayName = i.Channel + " - " + i.Frec + " GHz"
                    });
                }

                //Agregue esto
                foreach (var i in modelos.Modelo2.ChannelObject)
                {
                    RadarConfigurationModel_.ChannelObject.Add(new Objetivo
                    {
                        Value = i.value,
                        Object = i.objecto,
                        DisplayNameO = i.value + " - " + i.objecto

                    });

                }
                //Fin

                RadarConfigurationModel_.Elevation = 0;
            }

        }

        /// <summary>
        /// Metodo SelectionChanged, controla el cambio de los combo box de canales de frecuencia 
        /// </summary>
        private void SelectionChanged()
        {
            
            if (RadarConfigurationModel_.SModelo != null)
            {
                var modelos = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ModelosRardar;
                
                RadarConfigurationModel_.ChannelFrec.Clear();
                RadarConfigurationModel_.ChannelObject.Clear();

                ModeloSelected = true;
                if (RadarConfigurationModel_.SModelo.Id == 1)
                {
                    foreach (var i in modelos.Modelo1.ChannelFrec)
                    {
                        RadarConfigurationModel_.ChannelFrec.Add(new Channels
                        {
                            Id = i.Channel,
                            Frecuency = i.Frec,
                            DisplayName = i.Channel + " - " + i.Frec + " GHz"
                        });
                    }
                    ModeloSelected = true;
                    RadarConfigurationModel_.Modelo = RadarConfigurationModel_.SModelo.Id;
                }
                if (RadarConfigurationModel_.SModelo.Id == 2)
                {                  
                    //Aca agrega los valores del combobox de la frecuencia
                    foreach (var i in modelos.Modelo2.ChannelFrec)
                    {
                        RadarConfigurationModel_.ChannelFrec.Add(new Channels
                        {
                            Id = i.Channel,
                            Frecuency = i.Frec,
                            DisplayName = i.Channel + " - " + i.Frec + " GHz"
                            
                    });
                    }


                    //Prueba para cargar el combobox de objetos
                    foreach (var i in modelos.Modelo2.ChannelObject)
                    {
                        RadarConfigurationModel_.ChannelObject.Add(new Objetivo
                        {
                            Value = i.value,
                            Object = i.objecto,
                            DisplayNameO = i.value + " - " + i.objecto
                            
                        });
                        
                    }


                    RadarConfigurationModel_.Modelo = RadarConfigurationModel_.SModelo.Id;

                }
            }
        }
        /// <summary>
        /// Metodo SetId, configura el id del dispositivo radar que se va a agregar
        /// </summary>
        private void SetId()
        {
            RadarConfigurationModel_.Id = Convert.ToInt16(Notification.Content);

        }
        /// <summary>
        /// Metodo GetListModel, obitiene los modelos de dispositivos radar 
        /// </summary>
        private void GetListModel()
        {

            RadarConfigurationModel_.Modelos = new ObservableCollection<Modelo>()
            {

                new Modelo()
                {
                    Id=TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ModelosRardar.Modelo2.Id,
                    Name = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ModelosRardar.Modelo2.Name,
                    
                },
            };
        }

        /// <summary>
        /// Metodo RaiseCustomPopup, invoca la ventana de notificación
        /// </summary>
        private void RaiseCustomPopup()
        {
            CustomPopupRequest.Raise(new Notification { Title = _tituloError, Content = new { Text = _mensaje, Show = false } }, r => Tittle = "PRORAM Consola de monitoreo");
        }

        

        /// <summary>
        /// Metodo SubmitInsRadar, valida los datos de ser correctos confirma la informacion y cierra la vista
        /// </summary>
        private void SubmitInsRadar()
        {
            RadarConfigurationModel_.Azimuth = 2;
            RadarConfigurationModel_.ValidateProperties();
            Errors = FlattenErrors();
            RadarConfigurationModel_.IdTextColor = GetColorById(RadarConfigurationModel_.Id);
            var Altitude = Errors.Where(p => p.Property.Contains("Altitude"));
            var TXPower = Errors.Where(p => p.Property.Contains("TXPower"));
            var InstallationAngle = Errors.Where(p => p.Property.Contains("InstallationAngle"));
            var NorthHeiding = Errors.Where(p => p.Property.Contains("NorthHeiding"));
            var ChannelFrec = Errors.Where(p => p.Property.Contains("SchannelFrec"));
            var ChannelObject = Errors.Where(p => p.Property.Contains("SchannelObject"));
            var Latitud = Errors.Where(p => p.Property.Contains("Latitud"));
            var Longitud = Errors.Where(p => p.Property.Contains("Longitud"));
            var _isWithinArea = IsWithinArea();
            if (!RadarConfigurationModel_.HasErrors && _isWithinArea == false)
            {
                _tituloError = "Alerta";
                _mensaje = "El radar se debe encontrar dentro del área definida";
                RaiseCustomPopup();
                _tituloError = "";
                _mensaje = "";
            }
            if (Errors.Count > 0)
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

                else if (ChannelObject.Count() != 0)
                {
                    _mensaje = "El campo tipo de objetivo es obligatorio";
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
            if (!RadarConfigurationModel_.HasErrors && _isWithinArea == true)
            {
                CustomPopupRequest.Raise(new Notification { Title = "Notificación", Content = new { Text = "Dispositivo radar agregado satisfactoriamente", Show = true, ShowAlert = false } }, r => Tittle = "PRORAM Consola de monitoreo");
                _notification.RadarConfigurationModelI_ = RadarConfigurationModel_;
                _notification.Confirmed = true;

                RadarConfigurationModel_.Expander = false;
                RadarConfigurationModel_ = null;
                FinishInteraction?.Invoke();
            }




        }

        /// <summary>
        /// Metodo CancelInteraction, cancel los cambios en la vista y la cierra
        /// </summary>
        private void CancelInteraction()
        {
            ///RadarConfigurationModel_ = new RadarConfigurationModel();
            _notification.RadarConfigurationModelI_ = null;
            _notification.Confirmed = false;
            RadarConfigurationModel_.Expander = false;
            RadarConfigurationModel_ = null;
            FinishInteraction?.Invoke();
        }

        /// <summary>
        /// Agrega el radar si el formulario no contiene errores
        /// </summary>
        private void SubmitRadar()
        {
            RadarConfigurationModel_.ValidateProperties();
            Errors = FlattenErrors();
            var NameRadar = Errors.Where(p => p.Property.Contains("RadarName"));
            var errorIp = Errors.Where(p => p.Property.Contains("IpAddress"));
            var modelo = Errors.Where(p => p.Property.Contains("SModelo"));

            if (errorIp.Count() == 0 && modelo.Count() == 0)
            {
                RadarConfigurationModel_.Expander = true;
            }
            if (errorIp.Count() != 0 || modelo.Count() != 0 || NameRadar.Count() !=0)
            {

                if (NameRadar.Count() != 0)
                {
                    _tituloError = "Alerta";
                    _mensaje = "Ingrese un nombre de radar entre 3 y 10 caracteres de longitud";
                }
                else if (modelo.Count() != 0)
                {
                    _tituloError = "Alerta";
                    _mensaje = "Seleccione un modelo de radar";
                }
                else if (errorIp.Count() != 0)
                {
                    _tituloError = "Alerta";
                    _mensaje = "Ingrese la IP de forma correcta";
                }
                RaiseCustomPopup();
                _tituloError = "";
                _mensaje = "";
            }
        }
        /// <summary>
        /// Metodo IsWithinArea, valida si el radar que se desea agregar se encuentra dentro del area de vigilancia
        /// </summary>
        /// <returns></returns>
        private bool IsWithinArea()
        {
            if (RadarConfigurationModel_.Latitud <= _notification.Point1.Latitude && RadarConfigurationModel_.Longitud >= _notification.Point1.Longitude)
            {
                if (RadarConfigurationModel_.Latitud >= _notification.Point2.Latitude && RadarConfigurationModel_.Longitud <= _notification.Point2.Longitude)
                {
                    return true;
                }

            }
            return false;
        }



        public InteractionRequest<INotification> CustomPopupRequest { get; set; }

        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand CustomPopupCommand { get; }
        public DelegateCommand OnLoadScreenCommand { get; set; }
        public Action FinishInteraction { get; set; }
        /// <summary>
        /// Propiedad Notification
        /// </summary>
        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (IRadarConfigurationNotification)value); }
        }

        public static SolidColorBrush GetColorById(int i)
        {
            switch (i)
            {
                case 1: return Brushes.DeepSkyBlue;
                case 2: return Brushes.DarkOrange;
                case 3: return Brushes.Gold;
                case 4: return Brushes.LightCoral;
                case 5: return Brushes.LimeGreen;
                case 7: return Brushes.Orchid;
                case 8: return Brushes.Plum;
                case 9: return Brushes.RoyalBlue;
                case 10: return Brushes.Tomato;

                default: return Brushes.Beige;
            }
        }
    }
}
