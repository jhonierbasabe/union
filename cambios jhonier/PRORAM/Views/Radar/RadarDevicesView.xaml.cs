using PRORAM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for RadarDevicesView.xaml
    /// </summary>
    public partial class RadarDevicesView : UserControl
    {
        public RadarDevicesView()
        {
            InitializeComponent();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window parent = this.Parent as Window;
            parent.DragMove();

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
  

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = (RadarDevicesViewModel)this.DataContext;
            //vm.RadarDevicesMode2_ = new ObservableCollection<Models.RadarDevicesModel>();
        }
    }
}
