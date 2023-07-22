using PRORAM.ResourcesFiles;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PRORAM.Views
{
    /// <summary>
    /// Lógica de interacción para PowerRadarView.xaml
    /// </summary>
    public partial class PowerRadarView : UserControl
    {
        public PowerRadarView()
        {
            InitializeComponent();
            this.ChangeContent();
        }

        private void ChangeContent()
        {

            Label Label1 = (Label)this.FindName("Label1");
            Label Label2 = (Label)this.FindName("Label2");
            Label Label3 = (Label)this.FindName("Label3");

            Label1.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.PowerRadar.Label1;
            Label2.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.PowerRadar.Label2;
            Label3.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.PowerRadar.Label3;
            

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window parent = this.Parent as Window;
            parent.DragMove();

        }
    }
}
