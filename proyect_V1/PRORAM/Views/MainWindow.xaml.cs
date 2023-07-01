using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using PRORAM.Properties;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using PRORAM.Models;
using PRORAM.Models.Shared;
using System;
using Prism.Events;

namespace PRORAM.Views
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand MenuItemSelected { get; } = new RoutedCommand("MenuItemSelected", typeof(MainWindow));
        private IEventAggregator _ea;
        public MainWindow(IEventAggregator ea)
        {
            _ea = ea;
            InitializeComponent();
            SetTheme();
        }

        public void SetTheme()
        {

            Settings.Default.Theme = MaterialDesignThemes.Wpf.BaseTheme.Dark;

            Settings.Default.Save();
            ((App)Application.Current).SetTheme(Settings.Default.Theme);
        }

        //private void LoadStage(object sender, RoutedEventArgs e)
        //{

        //    string load = "";
        //    OpenFileDialog openFileDialog = new OpenFileDialog();

        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        try
        //        {
        //            load = File.ReadAllText(openFileDialog.FileName);
        //            _ea.GetEvent<MsmSentEvent>().Publish(new RadarActions()
        //            {
        //                Action = "Reset"
        //            });
        //            DSconnection.DSConnection.Reset();

        //            LoadStageModel stage = JsonConvert.DeserializeObject<LoadStageModel>(load);

        //            DSconnection.DSConnection.AddSurveillanceArea(stage.SurveillanceArea.LatitudP1, stage.SurveillanceArea.LongitudP1, stage.SurveillanceArea.LatitudP2, stage.SurveillanceArea.LongitudP2, stage.SurveillanceArea.NombreArea);

        //            _ea.GetEvent<MessageSentEvent>().Publish(new TargetAreaModel
        //            {
        //                LatitudP1 = stage.SurveillanceArea.LatitudP1,
        //                LatitudP2 = stage.SurveillanceArea.LatitudP2,
        //                LongitudP1 = stage.SurveillanceArea.LongitudP1,
        //                LongitudP2 = stage.SurveillanceArea.LongitudP2,
        //                NombreArea = stage.SurveillanceArea.NombreArea,
        //                LogsEvent = "Se cargó un área de vigilancia."
        //            });

        //            foreach (var device in stage.Devices)
        //            {
        //                device.Radiation = false;
        //                device.StateConnection = false;
        //                device.IsSaveComplete = false;
        //                device.IsSaveCompleteT = false;
        //                device.SaveProgress = 0;
        //                device.SaveProgressT = 0;
        //                device.IsSaving = false;
        //                device.IsSavingT = false;
        //                DSconnection.DSConnection.AddRadarDeviceRow(device);
        //                _ea.GetEvent<MsmSentEvent>().Publish(new RadarActions()
        //                {
        //                    GuidRadar = device.GuidRadar,
        //                    RadarDevice = device,
        //                    Action = "AddRadar",
        //                    Logs = "Se cargó un nuevo dispositivo radar con ip " + device.IpAddress
        //                });

        //            }
        //        }
        //        catch { }
        //    }
        //}


        private void SaveStage(object sender, RoutedEventArgs e)
        {
            var devices = DSconnection.DSConnection.CountDevices();
            string save = "";

            if (devices > 0)
            {
                LoadStageModel stage = DSconnection.DSConnection.GetSurveillanceAreaModel();
                save = JsonConvert.SerializeObject(stage, Formatting.Indented);
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text file(*.txt)| *.txt ";
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, save);
                }
            }

        }
    }
}
