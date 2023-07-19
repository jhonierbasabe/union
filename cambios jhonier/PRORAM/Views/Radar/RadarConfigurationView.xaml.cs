using PRORAM.ResourcesFiles;
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

namespace PRORAM.Views
{
    /// <summary>
    /// Interaction logic for RadarConfigurationView.xaml
    /// </summary>
    public partial class RadarConfigurationView : UserControl
    {
        public RadarConfigurationView()
        {
            InitializeComponent();
            this.SizeChanged += ResizeWindow;
            this.ChangeContent();
        }
        private void ResizeWindow(object sender, SizeChangedEventArgs e)
        {
            var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            Window parent = this.Parent as Window;
            parent.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            parent.Top = workingArea.Height / 2 - this.ActualHeight / 2;
        }
        private void ChangeContent()
        {
            Label Label1 = (Label)this.FindName("Label1");
            Label Label2 = (Label)this.FindName("Label2");
            Label Label3 = (Label)this.FindName("Label3");
            Label Label4 = (Label)this.FindName("Label4");
            Label Label5 = (Label)this.FindName("Label5");
            Label Label6 = (Label)this.FindName("Label6");
            Label Label7 = (Label)this.FindName("Label7");
            Label Label8 = (Label)this.FindName("Label8");
            Label Label9 = (Label)this.FindName("Label9");
            Label Label10 = (Label)this.FindName("Label10");
            Label Label11 = (Label)this.FindName("Label11");
            Label Label12 = (Label)this.FindName("Label12");
            Label Label13 = (Label)this.FindName("Label13");



            Label1.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RadarConfiguration.Label1;
            Label2.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RadarConfiguration.Label2;
            Label3.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RadarConfiguration.Label3;
            Label4.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RadarConfiguration.Label4;
            Label5.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RadarConfiguration.Label5;
                                    
            Label9.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RadarConfiguration.Label9;
            Label10.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RadarConfiguration.Label10;
            Label11.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RadarConfiguration.Label11;
            //Label12.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RadarConfiguration.Label12;
            //Label13.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RadarConfiguration.Label13;

        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window parent = this.Parent as Window;
            parent.DragMove();

        }
    }
}
