using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PRORAM.Models.Shared;
using PRORAM.Models.TPC;
using PRORAM.ServicesTcp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase TargetPanelViewModel, controla la interación con la vista TargetPanelView
    /// </summary>
    public class TargetPanelViewModel: BindableBase
    {
        #region private
        private ObservableCollection<Tracks> _trackList;        
        private IEventAggregator _ea;
        private IRegionManager _regionManager;
        private bool _expander;

        private Tracks _strackList;
        #endregion

        /// <summary>
        /// Propiedad STrackList, contiene la información de un track seleccionado
        /// </summary>
        public Tracks STrackList
        {
            get { return _strackList; }
            set {SetProperty(ref _strackList, value); }
        }

        /// <summary>
        /// Propiedad Expander, define si el panel de blancos esta visible
        /// </summary>
        public bool Expander
        {
            get { return _expander; }
            set {SetProperty(ref _expander, value); }
        }
        /// <summary>
        /// Propiedad TrackList, colección de tracks disponibles
        /// </summary>
        public ObservableCollection<Tracks> TrackList
        {
            get { return _trackList; }
            set {SetProperty(ref _trackList, value); }
        }

        public DelegateCommand DeleteTrackCommand { get; set; }

        /// <summary>
        /// Constructor de la clase TargetPanelViewModel
        /// </summary>
        /// <param name="regionManager">parametro de redireccionamiento de la vista</param>
        /// <param name="ea">objecto referencia a los eventos generados por la aplicación</param>
        public TargetPanelViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            Expander = false;
            _ea = ea;
            _regionManager = regionManager;
            TrackList = new ObservableCollection<Tracks>();
            _ea.GetEvent<EventPanel>().Subscribe(ShowDetail);
            DeleteTrackCommand = new DelegateCommand(DeleteTrack);
        }
        /// <summary>
        /// Metodo DeleteTrack, elimina un track seleccionado
        /// </summary>
        private void DeleteTrack()
        {
                        
            _ea.GetEvent<EventTargets>().Publish(new TargetEvents { Track = STrackList, Action = "Delete", Target = "Track"});
            App.Current.Dispatcher.Invoke(delegate
            {
                TrackList.Remove(TrackList.Where(x => x.Id == STrackList.Id && x.RadarOrigin == STrackList.RadarOrigin).FirstOrDefault());
            });
        }
        /// <summary>
        /// Metodo ShowDetail, muestra el panel de blancos        
        /// </summary>
        /// <param name="obj"></param>
        private void ShowDetail(DetailPanel obj)
        {
          
            if(obj.Action == "Add" && obj.Target == "Track")
            {
                Expander = true;                
                TrackList.Add(obj.Track);
            }
            if (obj.Action == "Replace" && obj.Target == "Track")
            {
                Expander = true;
                var index = TrackList.IndexOf(TrackList.Where(x => x.Id == obj.Track.Id && x.RadarOrigin ==obj.Track.RadarOrigin).FirstOrDefault());
                TrackList[index] = obj.Track;
            }
            if(obj.Action == "Clear" && obj.Target =="Track")
            {
        
                App.Current.Dispatcher.Invoke(delegate
                {
                    try
                    {
                        TrackList.Remove(TrackList.Where(x => x.Id == obj.Track.Id && x.RadarOrigin == obj.Track.RadarOrigin).Single());
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                    return true;
                });

            }
            if(obj.Action == "Add" && obj.Target == "Plot")
            {
                Expander = true;
  
            }
        }
    }
}
