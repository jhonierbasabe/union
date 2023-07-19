using PRORAM.Models;
using PRORAM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;
using MaterialDesignThemes.Wpf;

namespace PRORAM.Views
{
    /// <summary>
    /// Interaction logic for GeoLayerView.xaml
    /// </summary>
    public partial class GeoLayerView : UserControl
    {
        public GeoLayerView()
        {

            InitializeComponent();

        }
        private void SetCanvasSize()
        {

            Canvas myCanvas = new Canvas();
            Border borderContent = (Border)this.FindName("BorderContent");
            Canvas canvasContent = (Canvas)this.FindName("CanvasContent");
            ColorZone ToolBar = (ColorZone)this.FindName("ToolBar");
            ContentControl MyMap = (ContentControl)this.FindName("MyMap");


            var width = this.ActualWidth;
            var height = this.ActualHeight;

            canvasContent.Width = width - 20;
            canvasContent.Height = height - 20;

            MyMap.Width = width - 30;
            MyMap.Height = height - 30;



            ToolBar.Height = (double)(height * 0.1);
            ToolBar.Width = width - 50;



            foreach (FrameworkElement fe in canvasContent.Children)
            {

                // example 0

                //double top = (double)fe.GetValue(Canvas.TopProperty);
                //double left = (double)fe.GetValue(Canvas.LeftProperty);
                var top = canvasContent.ActualHeight;
                var left = canvasContent.ActualWidth;


                if (fe.Name != "ToolBar")
                {
                    fe.SetValue(Canvas.ZIndexProperty, 1);
                    fe.SetValue(Canvas.TopProperty, (double)5);
                    fe.SetValue(Canvas.LeftProperty, (double)5);
                    fe.SetValue(Canvas.RightProperty, (double)(left - 5));
                    fe.SetValue(Canvas.BottomProperty, (double)(top - 5));
                }
                else
                {
                    fe.SetValue(Canvas.ZIndexProperty, 2);
                    fe.SetValue(Canvas.TopProperty, (double)(20));
                    fe.SetValue(Canvas.LeftProperty, (double)(20));
                    fe.SetValue(Canvas.RightProperty, (double)(left - 20));
                    fe.SetValue(Canvas.BottomProperty, (double)(top * 0.15));
                }
                //// example 1
                //double top1 = Canvas.GetTop(fe);
                //double left1 = Canvas.GetLeft(fe);

            }


        }

        private void BorderContent_Loaded(object sender, RoutedEventArgs e)
        {
            SetCanvasSize();
        }

        private void BorderContent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetCanvasSize();
        }
    }
}

