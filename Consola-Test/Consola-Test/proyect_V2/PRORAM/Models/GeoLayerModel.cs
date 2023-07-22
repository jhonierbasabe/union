using Prism.Mvvm;
using Microsoft.Maps.MapControl.WPF;
using System.Collections;
using Prism.Events;
using System.Collections.ObjectModel;

namespace PRORAM.Models
{
    /// <summary>
    /// clase GeoLayerModel, contiene la estructura de los datos para la capa geografica
    /// </summary>
    public class GeoLayerModel: BindableBase
    {
        #region private
        private Location _Center;
        private double _ZoomLevel;        
        private Location _TargetArea;        
        private string _NameStage;
        private TargetAreaModel _TargetAreaModel;
        private Map _myMap;
        private bool _definedMap;
        #endregion

        /// <summary>
        /// Propiedad DefineMap de tipo bool me dice cuando se ha defindo un area objetivo
        /// </summary>
        public bool DefinedMap
        {
            get { return _definedMap; }
            set {SetProperty(ref _definedMap, value); }
        }

        /// <summary>
        /// Propiedad MyMap de tipo Map, contendra el mapa que se visualizara el sistema de monitoreo
        /// </summary>
        public Map MyMap
        {
            get { return _myMap; }
            set {SetProperty(ref _myMap, value); }
        }

        /// <summary>
        /// Propiedad TargetAreaModel_ de tipo TargetAreaModel, contendra la inforamcion del area objetivo
        /// </summary>
        public TargetAreaModel TargetAreaModel_
        {
            get { return _TargetAreaModel; }
            set { SetProperty(ref _TargetAreaModel, value); }
        }
        /// <summary>
        /// Propiedad NameStage
        /// </summary>
        public string NameStage
        {
            get { return _NameStage; }
            set { SetProperty(ref _NameStage, value); }
        }

        /// <summary>
        /// Propiedad TargetArea
        /// </summary>
        public Location TargetArea
        {
            get { return _TargetArea; }
            set { SetProperty(ref _TargetArea, value); }
        }

        /// <summary>
        /// Propiedad Center, centra el mapa en una cordenada especifica
        /// </summary>
        public Location Center
        {
            get { return _Center; }
            set { SetProperty(ref _Center, value); }
        }
                  
        /// <summary>
        /// Propiedad ZoomLevel 
        /// </summary>
        public double ZoomLevel
        {
            get { return _ZoomLevel; }
            set { SetProperty(ref _ZoomLevel, value); }
        }
    }
}
