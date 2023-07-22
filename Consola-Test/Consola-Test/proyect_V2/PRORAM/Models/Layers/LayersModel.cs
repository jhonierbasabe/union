using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.Models.Layers
{
    /// <summary>
    /// Clase LayersModel modelo que conteniene las propiedades de las capas de visualización
    /// </summary>
    public class LayersModel:BindableBase
    {
        #region Propiedades privadas
        private int _id;
        private String  _name;
        private bool _state;
        private string _sigla;
        #endregion

        #region Propiedades publicas
        /// <summary>
        /// Propiedad Sigla string, sigla de la capa
        /// </summary>
        public string Sigla
        {
            get { return _sigla; }
            set { SetProperty(ref _sigla, value); }
        }
        /// <summary>
        /// Propiedad State bool, estado de visualización de la capa
        /// </summary>
        public bool State
        {
            get { return _state; }
            set {SetProperty(ref _state, value); }
        }
        /// <summary>
        /// Prorpiedad Name, nombre de la capa
        /// </summary>
        public String  Name
        {
            get { return _name; }
            set {SetProperty(ref _name, value); }
        }

        /// <summary>
        /// Propiedad Id int, identificador unico de la capa
        /// </summary>
        public int Id
        {
            get { return _id; }
            set {SetProperty(ref _id,value); }
        }

        #endregion
    }
}
