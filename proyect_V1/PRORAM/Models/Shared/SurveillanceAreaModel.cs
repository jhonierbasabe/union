using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.Models.Shared
{
    /// <summary>
    /// Clase SurveillanceAreaModel, modelo para la definición del área de vigilancia
    /// </summary>
    public class SurveillanceAreaModel : BindableBase
    {
        #region Propiedades privadas
        private double _latitudP1;
        private double _longitudP1;
        private double _latitudP2;
        private string _nombreArea;
        private double _longitudP2;
        #endregion

        /// <summary>
        /// Propiedad NomrbeArea, nombre del área de vigilancia
        /// </summary>
        public string NombreArea
        {
            get { return _nombreArea; }
            set { SetProperty(ref _nombreArea, value); }
        }
        /// <summary>
        /// Propiedad LatitudP1, latitud del punto superior izquierdo
        /// </summary>
        public double LatitudP1
        {
            get { return _latitudP1; }
            set { SetProperty(ref _latitudP1, value); }
        }
        /// <summary>
        /// Propiedad LongitudP1, longitud del punto superior izquierdo
        /// </summary>
        public double LongitudP1
        {
            get { return _longitudP1; }
            set { SetProperty(ref _longitudP1, value); }
        }
        /// <summary>
        /// Propiedad LatitudP2, latitud del punto inferior derecho
        /// </summary>
        public double LatitudP2
        {
            get { return _latitudP2; }
            set { SetProperty(ref _latitudP2, value); }
        }

        /// <summary>
        /// Propiedad LongitudP2, longitud del punto inferior derecho
        /// </summary>
        public double LongitudP2
        {
            get { return _longitudP2; }
            set { SetProperty(ref _longitudP2, value); }
        }

    }
}
