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
    /// Interaction logic for ChannelFrecView.xaml
    /// </summary>
    public partial class ChannelFrecView : UserControl
    {
        public ChannelFrecView()
        {            
            InitializeComponent();
            this.ChangeContent();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window parent = this.Parent as Window;
            parent.DragMove();

        }

        private void ChangeContent()
        {
            Label Label1 = (Label)this.FindName("Label1");
            Label Label2 = (Label)this.FindName("Label2");
            Label Label3 = (Label)this.FindName("Label3");
            Label Label4 = (Label)this.FindName("Label4");
            Label Title = (Label)this.FindName("Title");

            Label1.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.ChannelFrec.Label1;
            Label2.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.ChannelFrec.Label2;
            Label3.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.ChannelFrec.Label3;
            Label4.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.ChannelFrec.Label4;
            Title.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.ChannelFrec.Title;
            
        }
    }
}
