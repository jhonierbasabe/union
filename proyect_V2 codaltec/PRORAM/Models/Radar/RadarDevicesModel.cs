using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Prism.Mvvm;
using Prism.Commands;
using System;

namespace PRORAM.Models
{
    /// <summary>
    /// Clase RadarDevicesModel, con la estructura del dispositivo radar 
    /// </summary>
    public class RadarDevicesModel : BindableBase, INotifyPropertyChanged
    {
        #region private
        private byte _idRadar;
        private string _RadarName;
        private double _Azimuth;
        private string _Coordinates;
        private bool _StateConnection;
        private double _Latitud;
        private double _Longitud;
        private double _Elevation;
        private int _IdModelo;
        private Channels _SChannelFrec;
        private string _ipAddress;
        private double _Altitude;
        private int _TXPower;
        private double _installationAngle;
        private bool _radiation;
        private double _saveProgress;
        private bool _isSaveComplete;
        private bool _isSaving;
        private Guid _guid;
        private bool _isSavingT;
        private bool _isSaveCompleteT;
        private double _saveProgressT;

        private int _id;
        private bool _mClutter;
        private double northHeiding;
        private int _port;
        private bool _Check;
        private int _Orden;
        #endregion

        /// <summary>
        /// Propiedad Port int, Puerto de enlace del dispositivo radar
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { SetProperty(ref _port, value); }
        }
        /// <summary>
        /// Propiedad NorthHeiding double, angulo con respecto al norte
        /// </summary>
        public double NorthHeiding
        {
            get { return northHeiding; }
            set { SetProperty(ref northHeiding, value); }
        }
       
        /// <summary>
        /// Propiedad bool MClutter, define mapa clutter
        /// </summary>
        public bool MClutter
        {
            get { return _mClutter; }
            set { SetProperty(ref _mClutter, value); }
        }

        public bool Check
        {
            get { return _Check; }
            set { SetProperty(ref _Check, value); }
        }
        public int Orden
        {
            get { return _Orden; }
            set { SetProperty(ref _Orden, value); }
        }
        /// <summary>
        /// Propiedad Id int, identificador unico del radar
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        /// <summary>
        /// Propiedad IsSavingt bool, define si esta esperandoo por la respuesta del radar ("Tiempo de espera de la respuesta entre la consola y el radar mensaje de radiación")
        /// </summary>
        public bool IsSavingT
        {
            get { return _isSavingT; }
            set { SetProperty(ref _isSavingT, value); }
        }
        /// <summary>
        /// Propiedad IsSaveCompleteT bool, definie si se completo el tiempo de espera de la respuesta del radar al mensaje de radiación
        /// </summary>
        public bool IsSaveCompleteT
        {
            get { return _isSaveCompleteT; }
            set { SetProperty(ref _isSaveCompleteT, value); }
        }
        /// <summary>
        /// Propiedad SaveProgressT double, % de espera actual en la espera del mensaje de radiación
        /// </summary>
        public double SaveProgressT
        {
            get { return _saveProgressT; }
            set { SetProperty(ref _saveProgressT, value); }
        }

        /// <summary>
        /// Propiedad GuidRadar Guid, El identificador único global del radar
        /// </summary>
        public Guid GuidRadar
        {
            get { return _guid; }
            set { SetProperty(ref _guid, value); }
        }
        /// <summary>
        /// Propiedad IsSaving bool, define si esta esperando por la respuesta del radar ("Tiempo de espera de la respuesta entre la consola y el radar mensaje de GetId")
        /// </summary>
        public bool IsSaving
        {
            get { return _isSaving; }
            set { SetProperty(ref _isSaving, value); }
        }
        /// <summary>
        /// Propiedad IsSaveComplete bool, definie si se completo el tiempo de espera de la respuesta del radar al mensaje de GetId
        /// </summary>
        public bool IsSaveComplete
        {
            get { return _isSaveComplete; }
            set { SetProperty(ref _isSaveComplete, value); }
        }
        /// <summary>
        /// Propiedad SaveProgress double, % de espera actual en la espera del mensaje de GetId
        /// </summary>
        public double SaveProgress
        {
            get { return _saveProgress; }
            set { SetProperty(ref _saveProgress, value); }
        }

        /// <summary>
        /// Propiedad Radiation bool, define si el dispositivo radar esta radiando o no
        /// </summary>
        public bool Radiation
        {
            get { return _radiation; }
            set { SetProperty(ref _radiation, value); }
        }


        /// <summary>
        /// Propiedad IpAddress, contendra la ip o HostName del dispositivo
        /// </summary>
        [Required]
        public string IpAddress
        {
            get { return _ipAddress; }
            set { SetProperty(ref _ipAddress, value); }
        }

        /// <summary>
        /// Propiedad Altitude
        /// </summary>
        [Required]
        public double Altitude
        {
            get { return _Altitude; }
            set { SetProperty(ref _Altitude, value); }
        }
        /// <summary>
        /// Propiedad TXPower, nivel de potencia de transmición
        /// </summary>      
        [Required]
        public int TXPower
        {
            get { return _TXPower; }
            set { SetProperty(ref _TXPower, value); }
        }
        /// <summary>
        /// Propiedad InstallationAngle, angulo de instalación
        /// </summary>
        [Required]
        public double InstallationAngle
        {
            get { return _installationAngle; }
            set { SetProperty(ref _installationAngle, value); }
        }
        /// <summary>
        /// Propiedad SChannelFrec de tipo channels, contendra el canal seleccionado
        /// </summary>        
        public Channels SChannelFrec
        {
            get { return _SChannelFrec; }
            set { SetProperty(ref _SChannelFrec, value); }
        }

        /// <summary>
        /// Propiedad IdModelo
        /// </summary>
        [Required]
        public int IdModelo
        {
            get { return _IdModelo; }
            set { SetProperty(ref _IdModelo, value); }
        }

        /// <summary>
        /// Propiedad IdRadar, id unico generado por cada radar
        /// </summary>
        [DisplayName("Id")]
        public byte IdRadar
        {
            get { return _idRadar; }
            set { SetProperty(ref _idRadar, value); }
        }

        /// <summary>
        /// Propiedad NameRadar, nombre del radar seleccionado
        /// </summary>
        [DisplayName("Nombre")]
        [Required]
        public string RadarName
        {
            get { return _RadarName; }
            set { SetProperty(ref _RadarName, value); }
        }
        /// <summary>
        /// Propiedad Azimuth, angulo azimuth del radar
        /// </summary>
        [Required]
        public double Azimuth
        {
            get { return _Azimuth; }
            set { SetProperty(ref _Azimuth, value); }
        }

        /// <summary>
        /// Propiedad Coordinates, cordenadas del radar
        /// </summary>
        [DisplayName("Cordenadas")]
        public string Coordinates
        {
            get { return $"Lat: { Latitud }, Lon: { Longitud } "; }
            set { SetProperty(ref _Coordinates, value); }
            
        }

        /// <summary>
        /// Propiedad Elevation del radar
        /// </summary>
        [ScaffoldColumn(false)]
        [Required]
        public double Elevation
        {
            get { return _Elevation; }
            set { SetProperty(ref _Elevation, value); }
        }
        /// <summary>
        /// Propiedad Longitud del radar
        /// </summary>
        [ScaffoldColumn(true)]
        [Required]
        public double Longitud
        {
            get { return _Longitud; }
            set { SetProperty(ref _Longitud, value); }
        }
        /// <summary>
        /// Propiedad Latitud del radar
        /// </summary>
        [ScaffoldColumn(true)]
        [Required]
        public double Latitud
        {
            get { return _Latitud; }
            set { SetProperty(ref _Latitud, value); }
        }

        /// <summary>
        /// Propiedad StateConnection, me dice el radar se encuentra en red
        /// </summary>
        [DisplayName("Estado de conexión")]
        public bool StateConnection
        {
            get { return _StateConnection; }
            set { SetProperty(ref _StateConnection, value); }
        }
    }
    /// <summary>
    /// Clase SelectedItem, clase para la seleccion de items en la busqueda de errores 
    /// </summary>
    public class SelectedItem
    {
        public string Property { get; set; }

        public string MessageError { get; set; }
    }

}
