using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;
using System.Text;
using System.Threading.Tasks;
using PRORAM.DataValidation;
using System.Collections.ObjectModel;
using PRORAM.ResourcesFiles;
using System.ComponentModel.DataAnnotations;
using Prism.Events;

namespace PRORAM.Models
{
    /// <summary>
    /// Modelo de la clase dispositivo radar
    /// </summary    
    public class RadarConfigurationModel : ValidatableBindableBase
    {
        #region Propiedades Privadas
        private int _Modelo;
        private Modelo _SModelo;
        private string _ipAddress;
        private double? _Altitude;
        private int? _TXPower;
        private double? _installationAngle;
        private double? _Azimuth;
        private double? _Latitud;
        private double? _Longitud;
        private double? _Elevation;
        private Channels _SChannelFrec;
        private bool _radiation;
        private int _Id;
        private Guid _guid;
        private int _index;
        private double? _northHeiding;
        private string _radarName;
        private bool _expander;
        private int _port;
        private ObservableCollection<Channels> _ChannelFrec;



        #endregion
        /// <summary>
        /// Propiedad Port int, puerto del dispositivo radar
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { SetProperty(ref _port, value); }
        }
        /// <summary>
        /// Propiedad Expander bool, controla la visualizacion del panel de configuración
        /// </summary>
        public bool Expander
        {
            get { return _expander; }
            set { SetProperty(ref _expander, value); }
        }

        /// <summary>
        /// Propiedad NorthHeiding double, angulo con respecto al norte
        /// </summary>
        [Required]
        [Range(0.0, 360.0, ErrorMessage = "Ingrese un valor valido de {0}, entre {1}  y {2} ")]
        public double? NorthHeiding
        {
            get { return _northHeiding; }
            set { SetProperty(ref _northHeiding, value); }
        }

        /// <summary>
        /// Propiedad RadarName string, nombre del radar
        /// </summary>        
        [Display(Name = "Nombre del radar")]
        [Required(ErrorMessage = "Ingrese un Nombre del área")]        
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Ingrese el {0} entre {2} y {1} caracteres de longitud")]
        public string RadarName
        {
            get { return _radarName; }
            set { SetProperty(ref _radarName, value); }
        }
        /// <summary>
        /// Propiedad IndexChannel int, posicion del canal de transferencia del radar
        /// </summary>
        public int IndexChannel
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }
        }

        /// <summary>
        /// Propiedad Guid, indentificador unico
        /// </summary>
        public Guid Guid
        {
            get { return _guid; }
            set { SetProperty(ref _guid, value); }
        }


        /// <summary>
        /// Propiedad Id Radar
        /// </summary>       
        public int Id
        {
            get { return _Id; }
            set { SetProperty(ref _Id, value); }
        }


        /// <summary>
        /// Propiedad Radiation, define si el radar esta radiando o no
        /// </summary>
        public bool Radiation
        {
            get { return _radiation; }
            set { SetProperty(ref _radiation, value); }
        }
        /// <summary>
        /// Propiedad SModelo de tipo Modelo, contendra la información del modelo seleccionado
        /// </summary>
        [Required]
        public Modelo SModelo
        {
            get { return _SModelo; }
            set { SetProperty(ref _SModelo, value); }
        }
        /// <summary>
        /// Propiedad SchannelFrec de tipo Channels, contendra la información del canal seleccionado
        /// </summary>}
        [Required]
        [Display(Name = "Canal de frecuencia")]
        public Channels SchannelFrec
        {
            get { return _SChannelFrec; }
            set { SetProperty(ref _SChannelFrec, value); }
        }
        /// <summary>
        /// Propiedad ChannelFrec, collección de canales
        /// </summary>
        public ObservableCollection<Channels> ChannelFrec
        {
            get { return _ChannelFrec; }
            set { SetProperty(ref _ChannelFrec, value); }
        }

        /// <summary>
        /// Propiedad Modelos, collección de modelos
        /// </summary>
        public ObservableCollection<Modelo> Modelos { get; set; } = new ObservableCollection<Modelo>()
        {

                new Modelo()
                {
                    Id=TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ModelosRardar.Modelo2.Id,
                    Name = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ModelosRardar.Modelo2.Name
                },
        };

        /// <summary>
        /// Propiedad elevación que almacena los valores de elvacion del radar
        /// </summary>
        [Required]
        [Display(Name = "Elevación")]
        public double? Elevation
        {
            get { return _Elevation; }
            set { SetProperty(ref _Elevation, value); }
        }
        /// <summary>
        /// Propiedad longitud, valores de longitud de la ubicación del radar
        /// </summary>
        [Required]
        [Range(-90.0, 90.0, ErrorMessage = "Ingrese un valor válido de {0}, entre {1}  y {2} ")]
        public double? Longitud
        {
            get { return _Longitud; }
            set { SetProperty(ref _Longitud, value); }
        }
        /// <summary>
        /// Propiedad latitud, valores de latitud
        /// </summary>
        [Required]
        [Range(-180.0, 180.0, ErrorMessage = "Ingrese un valor válido de {0}, entre {1}  y {2} ")]
        public double? Latitud
        {
            get { return _Latitud; }
            set { SetProperty(ref _Latitud, value); }
        }

        /// <summary>
        /// Propiedad Azimuth, Ángulo con respecto al norte geografico
        /// </summary>
        [Required]
        [Range(0, 360, ErrorMessage = "Ingrese un valor válido de {0}, entre {1}  y {2} ")]
        public double? Azimuth
        {
            get { return _Azimuth; }
            set { SetProperty(ref _Azimuth, value); }
        }
        /// <summary>
        /// Propiedad InstallationAngle, Ángulo de instalacion respecto al poste
        /// </summary>
        [Required]
        [Display(Name = "Ángulo de instalación")]
        public double? InstallationAngle
        {
            get { return _installationAngle; }
            set { SetProperty(ref _installationAngle, value); }
        }
        /// <summary>
        /// Propiedad TXPower, potencia de transmisión
        /// </summary>
        [Required]
        [Display(Name = "Potencia de transmisión")]
        [Range(0, 100, ErrorMessage = "Ingrese un valor válido de {0}, entre {1}  y {2} ")]
        public int? TXPower
        {
            get { return _TXPower; }
            set { SetProperty(ref _TXPower, value); }
        }
        /// <summary>
        /// Propiedad Altitude, valores de altitud de instalación del sensor
        /// </summary>
        [Required]
        [Display(Name = "Altitud")]
        [Range(1, 6, ErrorMessage = "Ingrese un valor válido de {0}, entre {1}  y {2} ")]
        public double? Altitude
        {
            get { return _Altitude; }
            set { SetProperty(ref _Altitude, value); }
        }
        /// <summary>
        /// Propiedad IpAddress, Ip ó HostName del sensor en red
        /// </summary>
        [Required]
        [Display(Name = "Ip ó HostName")]
        [RegularExpression(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", ErrorMessage = "Ingrese una direccion Ip válida")]
        public string IpAddress
        {
            get { return _ipAddress; }
            set { SetProperty(ref _ipAddress, value); }
        }
        /// <summary>
        /// Propiedad Modelo, tipo de modelo a usasr
        /// </summary>
        [Required]
        public int Modelo
        {
            get { return _Modelo; }
            set { SetProperty(ref _Modelo, value); }
        }
        /// <summary>
        /// Constructor por defecto 
        /// </summary>
        public RadarConfigurationModel()
        {
            CreateGuid();
        }
        /// <summary>
        /// Metodo CreateGuid, genera un nuevo Guid para el radar
        /// </summary>
        private void CreateGuid()
        {
            Guid = Guid.NewGuid();
        }
    }
    /// <summary>
    /// Clase channels, contiene la estructura para los canales de frecuencia
    /// </summary>
    public class Channels : ValidatableBindableBase
    {
        #region Private
        private int _id;
        private double _frecuency;
        private string _DisplayName;
        #endregion
        /// <summary>
        /// Propiedad DisplayName
        /// </summary>
        public string DisplayName
        {
            get { return _DisplayName; }
            set { SetProperty(ref _DisplayName, value); }
        }
        /// <summary>
        /// Propiedad Id
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        /// <summary>
        /// Propiedd Frecuency
        /// </summary>
        public double Frecuency
        {
            get { return _frecuency; }
            set { SetProperty(ref _frecuency, value); }
        }
    }
    /// <summary>
    /// Clase con la estructura de la lista de modelos
    /// </summary>
    public class Modelo : ValidatableBindableBase
    {
        private int _Id;
        private string _Name;
        /// <summary>
        /// Propiedad nombre del modelo
        /// </summary>
        [Required(ErrorMessage = "Seleccione un modelo")]
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }
        /// <summary>
        /// Propiedad Id
        /// </summary>
        [Required(ErrorMessage = "Seleccione un modelo")]
        public int Id
        {
            get { return _Id; }
            set { SetProperty(ref _Id, value); }
        }

    }
}
