using Prism.Events;
using Prism.Mvvm;
using PRORAM.Models;
using PRORAM.Models.Layers;
using PRORAM.Models.Shared;
using PRORAM.ResourcesFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace PRORAM.DSconnection
{
    /// <summary>
    /// Clase DSConnection continene los metodos y rutinas del dataset de la aplicación
    /// </summary>
    public static class DSConnection
    {
        /// <summary>
        /// Propiedad estatica DS dataset de la aplicación
        /// </summary>
        static PRORAMDATASET DS = new PRORAMDATASET();
        /// <summary>
        /// Propiedad bool Isdefinited define si esta definido un area de vigilancia
        /// </summary>
        static bool Isdefinited = false;

        /// <summary>
        /// Metodo GetLayers obtiene las capas definidas en el dataset
        /// </summary>
        /// <returns>Las capas definidas para visualizarse en la aplicación</returns>
        public static ObservableCollection<LayersModel> GetLayers()
        {
            DataTable layerTable = DS.Layers;
            DataRow[] rows = layerTable.Select();
            ObservableCollection<LayersModel> layers = new ObservableCollection<LayersModel>();

            for (int i = 0; i < rows.Length; i++)
            {

                layers.Add(new LayersModel()
                {
                    Id = Convert.ToInt32(rows[i]["Id"]),
                    Name = Convert.ToString(rows[i]["Name"]),
                    State = Convert.ToBoolean(rows[i]["State"]),
                    Sigla = Convert.ToString(rows[i]["Sigla"])

                });
            }
            return layers;
        }
        /// <summary>
        /// Metodo AddLayers añade al dataset las capas que se visualizaran en la alicación
        /// </summary>
        public static void AddLayers()
        {
            DS.Layers.AddLayersRow(Name: "Dispositivos radar", State: false, Sigla: "Devices");
            DS.Layers.AddLayersRow(Name: "Tracks", State: true, Sigla: "Track");
        }
        /// <summary>
        /// Metodo AddRadarDeviceRow, añade un dispositivo radar al dataset
        /// </summary>
        /// <param name="obj">objeto RadarDevicesModel que se va agregar al dataset</param>
        public static void AddRadarDeviceRow(RadarDevicesModel obj)
        {
            DS.RadarDeviceTable.AddRadarDeviceTableRow(
                IdRadar: obj.IdRadar,
                RadarName: obj.RadarName,                
                Azimuth: obj.Azimuth,
                Coordinates: obj.Coordinates,                   
                StateConnection: obj.StateConnection,                
                Latitud: obj.Latitud,
                Longitud: obj.Longitud, 
                Elevation: obj.Elevation, 
                Port: obj.Port,
                IdModelo: obj.IdModelo,
                IpAddress: obj.IpAddress,
                SchannelFrec: obj.SChannelFrec.Id,
                InstallationAngle: obj.InstallationAngle,
                TXPower: obj.TXPower,
                Radiation: obj.Radiation,
                Guid: obj.GuidRadar,
                IdChannel: obj.SChannelFrec.Id,
                Frec: obj.SChannelFrec.Frecuency,
                MClutter: obj.MClutter,
                NorthHeiding: obj.NorthHeiding,
                Altitude: obj.Altitude, 
                IsSaveComplete: obj.IsSaveComplete, 
                IsSaveCompleteT: obj.IsSaveCompleteT,
                IsSaving: obj.IsSaving, 
                IsSavingT: obj.IsSavingT, 
                SaveProgress: obj.SaveProgress, 
                SaveProgressT: obj.SaveProgressT
               );

        }
        /// <summary>
        /// Metodo GetDevicesList, retorna la lista con los dispositivos radar agregados
        /// </summary>
        /// <returns>retorna la colleccion de dispositivos radar</returns>
        public static ObservableCollection<RadarDevicesModel> GetDevicesList()
        {
            DataTable radarTable = DS.RadarDeviceTable;
            int id;
            byte idradar;
            string radarname;
            double azimuth;
            string coordinates;
            bool stateConnection;
            double latitud;
            double longitud;
            double elevation;
            int idmodelo;
            string ipAddress;
            int port;
            double installationAngle;
            int txPower;
            bool radiation;
            Guid guid;
            double frec;
            int idChannel;
            double northHeiding;
            int altitude;
            DataRow[] rows = radarTable.Select();
            ObservableCollection<RadarDevicesModel> device = new ObservableCollection<RadarDevicesModel>();
            for (int i = 0; i < rows.Length; i++)
            {

                id = Convert.ToInt32(rows[i]["Id"]);
                idradar = Convert.ToByte(rows[i]["IdRadar"]);
                radarname = Convert.ToString(rows[i]["RadarName"]);
                azimuth = Convert.ToDouble(rows[i]["Azimuth"]);
                coordinates = Convert.ToString(rows[i]["Coordinates"]);
                stateConnection = Convert.ToBoolean(rows[i]["StateConnection"]);
                latitud = Convert.ToDouble(rows[i]["Latitud"]);
                longitud = Convert.ToDouble(rows[i]["Longitud"]);
                elevation = Convert.ToDouble(rows[i]["Elevation"]);
                idmodelo = Convert.ToInt32(rows[i]["IdModelo"]);

                ipAddress = Convert.ToString(rows[i]["IpAddress"]);
                port = Convert.ToInt32(rows[i]["Port"]);
                installationAngle = Convert.ToDouble(rows[i]["InstallationAngle"]);
                txPower = Convert.ToInt32(rows[i]["TXPower"]);
                radiation = Convert.ToBoolean(rows[i]["Radiation"]);
                guid = new Guid(Convert.ToString(rows[i]["Guid"]));
                frec = Convert.ToDouble(rows[i]["Frec"]);
                idChannel = Convert.ToInt32(rows[i]["IdChannel"]);
                northHeiding = Convert.ToDouble(rows[i]["NorthHeiding"]);
                altitude = Convert.ToInt32(rows[i]["Altitude"]);
                bool isSaving = Convert.ToBoolean(rows[i]["IsSaving"]);
                bool isSavingt = Convert.ToBoolean(rows[i]["IsSavingT"]);
                bool isSaveComplete = Convert.ToBoolean(rows[i]["IsSaveComplete"]);
                bool isSaveCompleteT = Convert.ToBoolean(rows[i]["IsSaveCompleteT"]);
                double saveProgress = Convert.ToDouble(rows[i]["SaveProgress"]);
                double saveProgressT = Convert.ToDouble(rows[i]["SaveProgressT"]);

                Channels channel = new Channels()
                {

                    Id = idChannel,
                    Frecuency = frec,
                    DisplayName = idChannel + " - " + frec
                };

                device.Add(
                    new RadarDevicesModel()
                    {
                        Id = id,
                        IdRadar = idradar,
                        RadarName = radarname,
                        Azimuth = azimuth,
                        Coordinates = coordinates,
                        StateConnection = stateConnection,
                        Latitud = latitud,
                        Longitud = longitud,
                        Elevation = elevation,
                        IdModelo = idmodelo,

                        IpAddress = ipAddress,
                        Port = port,
                        InstallationAngle = installationAngle,
                        TXPower = txPower,
                        Radiation = radiation,
                        GuidRadar = guid,
                        SChannelFrec = channel,
                        NorthHeiding = northHeiding,
                        Altitude = altitude,
                        IsSaveComplete = isSaveComplete,
                        IsSaveCompleteT = isSaveCompleteT,
                        IsSaving = isSaving,
                        IsSavingT = isSavingt,
                        SaveProgress = saveProgress,
                        SaveProgressT = saveProgressT
                    });
            }
            return device;
        }

        /// <summary>
        /// Metodo CountDevices
        /// </summary>
        /// <returns>el numero de dispositivos agregados</returns>
        public static int CountDevices()
        {
            return DS.RadarDeviceTable.Count;
        }

        /// <summary>
        /// Metodo UpdateDevice, actualiza el estado de conexion de un dispositivo
        /// </summary>
        /// <param name="obj">radardevicemodel con la informacion del radar</param>
        public static void UpdateDevice(RadarDevicesModel obj)
        {

            DataRow[] rows = DS.RadarDeviceTable.Select("Guid = '" + obj.GuidRadar.ToString() + "'");
            int i = 0;
            rows[0]["Id"] = obj.Id;
            rows[i]["IdRadar"] = obj.IdRadar;
            rows[i]["RadarName"] = obj.RadarName;
            rows[i]["Azimuth"] = obj.Azimuth;
            rows[i]["Coordinates"] = obj.Latitud + " " + obj.Longitud + " " + obj.Elevation;
            rows[i]["StateConnection"] = obj.StateConnection;
            rows[i]["Latitud"] = obj.Latitud;
            rows[i]["Longitud"] = obj.Longitud;
            rows[i]["Elevation"] = obj.Elevation;
            rows[i]["IdModelo"] = obj.IdModelo;
            rows[i]["IpAddress"] = obj.IpAddress;
            rows[i]["InstallationAngle"] = obj.InstallationAngle;
            rows[i]["TXPower"] = obj.TXPower;
            rows[i]["Radiation"] = obj.Radiation;
            rows[i]["Guid"] = obj.GuidRadar;
            rows[i]["Frec"] = obj.SChannelFrec.Frecuency;
            rows[i]["IdChannel"] = obj.SChannelFrec.Id;
            rows[i]["SchannelFrec"] = obj.SChannelFrec.Id;
            rows[i]["NorthHeiding"] = obj.NorthHeiding;
            rows[i]["Altitude"] = obj.Altitude;
            rows[i]["IsSaving"] = obj.IsSaving;
            rows[i]["IsSavingT"] = obj.IsSavingT;
            rows[i]["IsSaveComplete"] = obj.IsSaveComplete;
            rows[i]["IsSaveCompleteT"] = obj.IsSaveCompleteT;
            rows[i]["SaveProgress"] = obj.SaveProgress;
            rows[i]["SaveProgressT"] = obj.SaveProgressT;

            DS.RadarDeviceTable.AcceptChanges();
        }
        /// <summary>
        /// Metodo UpdateLayers actualiza los estados de las capas "Oculta o muestra las capas sobre la aplicación"
        /// </summary>
        /// <param name="obj">Objeto LayersModel con la información de la capa</param>
        public static void UpdateLayers(LayersModel obj)
        {
            DataRow[] row = DS.Layers.Select("Id = '" + obj.Id.ToString() + "'");
            row[0]["State"] = obj.State;
        }
        /// <summary>
        /// Metodo DeleteDevice, remueve un elemento especifico del dataset
        /// </summary>
        /// <param name="obj">radardevicemodel con la informacion del radar</param>
        public static void DeleteDeivice(RadarDevicesModel obj)
        {
            foreach (DataRow orow in DS.RadarDeviceTable.Select())
            {
                if (orow["Guid"].ToString().Equals(obj.GuidRadar.ToString()))
                {
                    DS.RadarDeviceTable.Rows.Remove(orow);
                }
            }

            DS.RadarDeviceTable.AcceptChanges();
        }
        /// <summary>
        /// Metodo AddSurveillanceArea añade la informacion de un área de vigilancia al dataset
        /// </summary>
        /// <param name="lat1">Latitud del punto superior izquierdo</param>
        /// <param name="long1">Longitud del punto superior izquierdo</param>
        /// <param name="lat2">Latitud del punto inferior derecho</param>
        /// <param name="long2">Longitud del punto inferior derecho</param>
        /// <param name="nombreArea">Nombre del área definida</param>
        public static void AddSurveillanceArea(double lat1, double long1, double lat2, double long2, string nombreArea)
        {
            if (DS.TargetArea.Count <= 0)
            {
                DS.TargetArea.AddTargetAreaRow(LatitudP1: lat1, LongitudP1: long1, LatitudP2: lat2, LongitudP2: long2, NombreArea: nombreArea);
                Isdefinited = true;
            }
            else
            {
                DataRow[] row = DS.TargetArea.Select();

                row[0]["LatitudP1"] = lat1;
                row[0]["LatitudP2"] = lat2;
                row[0]["LongitudP1"] = long1;
                row[0]["LongitudP2"] = long2;
                row[0]["NombreArea"] = nombreArea;
                DS.RadarDeviceTable.AcceptChanges();
                Isdefinited = true;
            }
        }

        /// <summary>
        /// Metodo GetSurveillanceAreaModel consulta la informacion del área definida
        /// </summary>
        /// <returns>Retorna modelo con la informacion del área definida</returns>
        public static LoadStageModel GetSurveillanceAreaModel()
        {
            DataRow[] rows = DS.TargetArea.Select();

            LoadStageModel loadStageModel = new LoadStageModel();
            loadStageModel.SurveillanceArea = new SurveillanceAreaModel
            {
                LatitudP1 = Convert.ToDouble(rows[0]["LatitudP1"]),
                LatitudP2 = Convert.ToDouble(rows[0]["LatitudP2"]),
                LongitudP1 = Convert.ToDouble(rows[0]["LongitudP1"]),
                LongitudP2 = Convert.ToDouble(rows[0]["LongitudP2"])
            };

            loadStageModel.Devices = GetDevicesList();

            return loadStageModel;
        }
        /// <summary>
        /// Metodo Reset Clear toda la información del dataset
        /// </summary>
        public static void Reset()
        {
            DS.TargetArea.Clear();
            DS.RadarDeviceTable.Clear();
        }
    }
}
