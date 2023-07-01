using Prism.Commands;
using Prism.Mvvm;
using PRORAM.DataValidation;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Data;
namespace PRORAM.Models
{
    /// <summary>
    /// Clase TargetAreaModel, contendra la informacion del area objetivo
    /// </summary>
    public class TargetAreaModel : ValidatableBindableBase
    {
        #region Private
        private string _NombreArea;
        public double? _LatitudP1;
        public double? _LongitudP1;
        public double? _LatitudP2;
        public double? _LongitudP2;
        private string _logsEvent;
        #endregion

        /// <summary>
        /// Propiedad LogsEvent string, mensaje para el panel de logs de la consola
        /// </summary>
        public string LogsEvent
        {
            get { return _logsEvent; }
            set { SetProperty(ref _logsEvent, value); }
        }
        /// <summary>
        /// Propiedad LongitudP2
        /// </summary>
        [Required]
        [Display(Name = "Longitud")]
        [Range(-90, 90, ErrorMessage = "Ingrese un valor valido de {0}, entre {1}  y {2} ")]
        public double? LongitudP2
        {
            get { return _LongitudP2; }
            set {SetProperty(ref _LongitudP2, value); }
        }

        /// <summary>
        /// Propiedad LatitudP2
        /// </summary>
        [Required]
        [Display(Name = "Latitud")]
        [Range(-180, 180, ErrorMessage = "Ingrese un valor valido de {0}, entre {1}  y {2} ")]
        public double? LatitudP2
        {
            get { return _LatitudP2; }
            set { SetProperty(ref _LatitudP2, value); }
        }


        /// <summary>
        /// Propiedad LongitudP1
        /// </summary>
        [Required]
        [Display(Name = "Longitud")]
        [Range(-90, 90, ErrorMessage = "Ingrese un valor valido de {0}, entre {1}  y {2} ")]
        public double? LongitudP1
        {
            get { return _LongitudP1; }
            set {SetProperty(ref _LongitudP1, value); }
        }
        /// <summary>
        /// Propiedad LatitudP1
        /// </summary>
        [Required]
        [Display(Name ="Latitud")]
        [Range(-180, 180, ErrorMessage = "Ingrese un valor valido de {0}, entre {1}  y {2} ")]
        public double? LatitudP1
        {
            get { return _LatitudP1; }
            set {SetProperty(ref _LatitudP1,  value); }
        }

        /// <summary>
        /// Propiedad NombreArea    
        /// </summary>
        [Required(ErrorMessage = "Ingrese un Nombre del área")]
        [Display(Name = "Nombre del área")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Ingrese el {0} entre {2} y {1} caracteres de longitud")]
        public string NombreArea
        {
            get { return _NombreArea; }
            set { SetProperty(ref _NombreArea, value); } 
        }
    }

}

