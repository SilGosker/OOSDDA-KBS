using Kbs.Business.Boat;
using System.Windows;

namespace Kbs.Wpf.Boat.Components
{
    public partial class DatePickPopupWindow : Window
    {
        public DatePickPopupViewModel ViewModel => (DatePickPopupViewModel)DataContext;
        public DatePickPopupWindow()
        {
            InitializeComponent();
            ViewModel.EndDate = DateTime.Now;
            Closing += (s, e) => //om bij te houden of het via het kruisje wordt gesloten
            {
                e.Cancel = true; 
                ViewModel.IsCancelled = true; 
                Hide(); //venster verborgen ipv vernietigd -> anders foutmelding bij meerdere keren
            };
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            ViewModel.IsCancelled = false;
            if (ViewModel.EndDate <= DateTime.Now)
            {
                MessageBox.Show("Er is een fout opgetreden waardoor de applicatie niet langer werkt. Start de applicatie opnieuw op en probeer het opnieuw.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
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
