using Kbs.Business.Boat;
using Kbs.Data.Boat;
using Kbs.Data.Reservation;
using Kbs.Wpf.Boat.Components;
using System.Windows;

namespace Kbs.Wpf.Boat.Components
{
    public partial class ChangeStatusMaintainingWindow : Window
    {
        public ChangeStatusMaintainingViewModel ViewModel => (ChangeStatusMaintainingViewModel)DataContext;
        public ChangeStatusMaintainingWindow()
        {
            InitializeComponent();
            ViewModel.EndDate = DateTime.Now;
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            ViewModel.IsCancelled = false;
            if (ViewModel.EndDate <= DateTime.Now)
            {
                MessageBox.Show("De datum moet in de toekomst liggen.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else
            {
                BoatEntity boat = new BoatEntity()
                {
                    EndDate = ViewModel.EndDate,
                };
            }
            Hide();
        }

        private void Cancel(object sender, RoutedEventArgs e) 
        {
            ViewModel.IsCancelled = true;
            Hide();
        }
        
    }
}
