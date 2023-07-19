using Prism.Interactivity.InteractionRequest;
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
using System.Windows.Threading;

namespace PRORAM.Views
{
    /// <summary>
    /// Interaction logic for CustomPopupView.xaml
    /// </summary>
    public partial class CustomPopupView : UserControl, IInteractionRequestAware
    {
        public CustomPopupView()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FinishInteraction?.Invoke();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window parent = this.Parent as Window;
            parent.DragMove();

        }
        public Action FinishInteraction { get; set; }
        public INotification Notification { get; set; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var started = DateTime.Now;
            new DispatcherTimer(
                   TimeSpan.FromMilliseconds(50),
                   DispatcherPriority.Normal,
                   new EventHandler((o, l) =>
                   {
                       var totalDuration = started.AddSeconds(3).Ticks - started.Ticks;
                       var currentProgress = DateTime.Now.Ticks - started.Ticks;
                       var currentProgressPercent = 100.0 / totalDuration * currentProgress;
                    
                       if (currentProgressPercent >= 50)
                       {                           
                           ((DispatcherTimer)o).Stop();
                           FinishInteraction?.Invoke();
                       }

                   }), Dispatcher.CurrentDispatcher);
        }
    }
}
