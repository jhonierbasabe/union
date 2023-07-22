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
    /// Interaction logic for PopupView.xaml
    /// </summary> 
    public partial class PopupConfirmationView : UserControl
    {
        public PopupConfirmationView()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window parent = this.Parent as Window;
            parent.DragMove();

        }
    }
}
