using PRORAM.ResourcesFiles;
using System;
using System.Windows.Controls;

namespace PRORAM.Views
{
    /// <summary>
    /// Interaction logic for RightPanelView.xaml
    /// </summary>
    public partial class RightPanelView : UserControl
    {
        public RightPanelView()
        {
            InitializeComponent();
            this.ChangeContent();
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
            Label Label14 = (Label)this.FindName("Label14");
            Label Label15 = (Label)this.FindName("Label15");
            Label Label16 = (Label)this.FindName("Label16");
            Label Label17 = (Label)this.FindName("Label17");
            Label Label18 = (Label)this.FindName("Label18");
            Label Label19 = (Label)this.FindName("Label19");
            Label Label20 = (Label)this.FindName("Label20");
            Label Label21 = (Label)this.FindName("Label21");
            Label Label22 = (Label)this.FindName("Label22");
            Label Label23 = (Label)this.FindName("Label23");


            Label1.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label1;
            Label2.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label2;
            Label3.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label3;
          //  Label4.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label4;
            Label5.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label5;
            Label6.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label6;
            Label7.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label7;
            Label8.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label8;
            Label9.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label9;
            Label10.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label10;
            Label11.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label11;
            Label12.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label12;
            Label13.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label13;
            Label14.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label14;                       
            Label17.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label17;
            Label18.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label18;
            Label19.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label19;
            Label20.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label20;
            Label21.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label21;
       //     Label22.Content = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.VIEW.RightPanel.Label22;
            

        }
    }
}
