using PRORAM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Prism.Interactivity;
using System.Windows.Shapes;

namespace PRORAM.Views
{
    /// <summary>
    /// Interaction logic for TargetAreaView.xaml
    /// </summary>
    public partial class TargetAreaView : UserControl
    {


        public TargetAreaView()
        {
            InitializeComponent();
            this.ChangeContext();
        }

        private void ChangeContext()
        {
            Label Label1 = (Label)this.FindName("Label1");
            Label Label2 = (Label)this.FindName("Label2");
            Label Label3 = (Label)this.FindName("Label3");
            Label Label4 = (Label)this.FindName("Label4");
            Label Label6 = (Label)this.FindName("Label6");
            Label Label7 = (Label)this.FindName("Label7");
            Label Label8 = (Label)this.FindName("Label8");


            Label1.Content = ResourcesFiles.TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.TargetArea.Label1;
            Label2.Content = ResourcesFiles.TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.TargetArea.Label2;
            Label3.Content = ResourcesFiles.TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.TargetArea.Label3;
            Label4.Content = ResourcesFiles.TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.TargetArea.Label4;

            Label6.Content = ResourcesFiles.TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.TargetArea.Label6;
            Label7.Content = ResourcesFiles.TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.TargetArea.Label7;
            Label8.Content = ResourcesFiles.TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.TargetArea.Label8;

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window parent = this.Parent as Window;
            parent.DragMove();
         
        }
    }
}
