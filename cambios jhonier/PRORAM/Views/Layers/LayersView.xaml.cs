using PRORAM.ResourcesFiles;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PRORAM.Views
{
    /// <summary>
    /// Interaction logic for LayersView.xaml
    /// </summary>
    public partial class LayersView : UserControl
    { 
        public LayersView()
        {
            InitializeComponent();
            this.SizeChanged += ResizeWindow;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window parent = this.Parent as Window;
            parent.DragMove();

        }

        private void ResizeWindow(object sender, SizeChangedEventArgs e)
        {
            var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            Window parent = this.Parent as Window;
            parent.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            parent.Top = workingArea.Height / 2 - this.ActualHeight / 2;
        }
    }
}
