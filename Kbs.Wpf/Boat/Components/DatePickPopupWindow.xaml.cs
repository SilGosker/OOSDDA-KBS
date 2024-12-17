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
                MessageBox.Show("De datum moet in de toekomst liggen.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            BoatEntity boat = new BoatEntity()
            {
                EndDate = ViewModel.EndDate,
            };
            Hide();
        }

        private void Cancel(object sender, RoutedEventArgs e) 
        {
            ViewModel.IsCancelled = true;
            Hide();
        }
    }
}
