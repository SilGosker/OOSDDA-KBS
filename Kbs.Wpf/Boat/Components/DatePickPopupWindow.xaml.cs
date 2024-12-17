using Kbs.Business.Boat;
using System.Windows;

namespace Kbs.Wpf.Boat.Components
{
    public partial class DatePickPopupWindow : Window
    {
        public DatePickPopupViewModel ViewModel => (DatePickPopupViewModel)DataContext;
        public DatePickPopupWindow(string title)
        {
            InitializeComponent();
            ViewModel.EndDate = DateTime.Now;
            ViewModel.Title = title;
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
            var validationResult = BoatValidator.ValidateEndDate(ViewModel.EndDate);
            if (validationResult.Any())
            {
                foreach (var error in validationResult)
                {
                    MessageBox.Show(error.Value, "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
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
