using PRORAM.Models.TPC;
using PRORAM.ServicesTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.Models.Shared
{
    /// <summary>
    /// Clase MessageConnect, modelo para los mensajes de la consola al radar
    /// </summary>
    public class MessageConnect
    {
        public byte[] Data { get; set; }
        public Guid GuidRadar { get; set; }
        public string Actions { get; set; }
    }
    /// <summary>
    /// Clase EventsDataSet, modelo para los evento de la consola al dataset
    /// </summary>
    public class EventsDataSet
    {
        public string evento { get; set; }
    }
    /// <summary>
    /// Clase DetailPanel, modelo para los eventos de los paneles de la aplicación
    /// </summary>
    public class DetailPanel
    {
        public Tracks Track { get; set; }
        public RadarDevicesModel Device { get; set; }
        public Plots Plots { get; set; }
        public string Action { get; set; }
        public string Guid_ { get; set; }
        public string Target { get; set; }
        public int Id { get; set; }
    }
    /// <summary>
    /// Clase TargetEvents, modelo de las acciones realizadas sobre los tracks
    /// </summary>
    public class TargetEvents
    {
        public string Action { get; set; }
        public string Target { get; set; }
        public Tracks Track { get; set; }
    }
    /// <summary>
    /// Clase ToolsSelect, modelo para los eventos de las herramientas de la vista de la consola
    /// </summary>
    public class ToolsSelect
    {
        public string EventName { get; set; }
    }
    /// <summary>
    /// Clase BandTargets, modelo para los eventos de baneo de tracks
    /// </summary>
    public class BandTargets
    {
        public int Id { get; set; }
        public int RadarOrigin { get; set; }
    }
    /// <summary>
    /// Clase ActionPlots, modelo de los eventos disparados por los Plots
    /// </summary>
    public class ActionPlots
    {
        public Guid PlotGuid { get; set; }
        public string Actions { get; set; }
        public int IdRadar { get; set; }

    }
}
