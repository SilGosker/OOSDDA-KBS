using Kbs.Data.Boat;
using Kbs.Data.Reservation;
using Kbs.Wpf.Boat.Read.Define;
using System.Windows;

namespace Kbs.Wpf.Boat.Read.Define
{
    public partial class ChangeStatusMaintainingWindow : Window
    {
        public ChangeStatusMaintainingViewModel ViewModel => (ChangeStatusMaintainingViewModel)DataContext;
        public ChangeStatusMaintainingWindow()
        {
            InitializeComponent();
            ViewModel.Date = DateTime.Now;
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            ViewModel.IsCancelled = false;
            Hide();
        }

        private void Cancel(object sender, RoutedEventArgs e) 
        {
            ViewModel.IsCancelled = true;
            Hide();
        }
        
    }
}
