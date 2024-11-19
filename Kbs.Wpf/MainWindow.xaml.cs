using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Azure;
using System.Windows.Navigation;
using Kbs.Business.Session;
using Kbs.Wpf.Attributes;
using Kbs.Wpf.Reservation.ViewReservation;
using Kbs.Wpf.User.Login;
using Kbs.Wpf.Reservation.ViewReservationSpecificPage;

namespace Kbs.Wpf
{
    public partial class MainWindow : Window, INavigationManager
    {
        private MainViewModel ViewModel => (MainViewModel)DataContext;
        public MainWindow()
        {
            InitializeComponent();

            SessionManager.Instance.SessionTimeExpired += async (_, _) =>
            {
                var success = Task.Run(() =>
                {
                    var dialogResult = MessageBox.Show("Uw sessie is verlopen. Druk op OK om af te sluiten.",
                        "Sessie verlopen",
                        MessageBoxButton.OKCancel);

                    return dialogResult == MessageBoxResult.OK;
                });

                var timeout = Task.Delay(TimeSpan.FromSeconds(20));

                var result = await Task.WhenAny(success, timeout);

                if (result == timeout || (result == success && success.Result))
                {
                    SessionManager.Instance.Logout();
                    var loginWindow = new LoginWindow();
                    loginWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Uw sessie is verlengd.", "Sessie verlengd", MessageBoxButton.OK);
                }
            };

            var user = SessionManager.Instance.Current.User;

            // todo: add navigation items
            if (user.IsMember() || user.IsGameCommissioner())
            {
                ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ViewReservationPage(this)) { Name = "Mijn reserveringen" });
                ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new Page()) {Name = "Plaatsen reservering"});
                ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new Page()) {Name = "Instellingen"});
                
            }

            if (user.IsMaterialCommissioner())
            {
                ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new Page()) { Name = "Overzicht boottypen" });
                ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new Page()) { Name = "Overzicht boten" });
            }
        }

        public void Navigate<TPage>(Func<TPage> creator) where TPage : Page
        {
            Page page;
            var attributes = typeof(TPage).GetCustomAttributes(typeof(HasRoleAttribute))
                .Cast<HasRoleAttribute>().ToArray();

            if (attributes.Length > 0)
            {
                var user = SessionManager.Instance?.Current?.User;
                if (user == null)
                {
                    MessageBox.Show(this, "U heeft geen toegang tot deze functie", "Toegang geweigerd");
                    return;
                }

                if (attributes.Any(e => user.Is(e.Role)))
                { 
                    page = creator();
                    NavigationFrame.Navigate(page);
                }

                MessageBox.Show(this, "U heeft geen toegang tot deze functie", "Toegang geweigerd");
            }

            page = creator();
            NavigationFrame.Navigate(page);
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            SessionManager.Instance.Logout();
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void MyReservations(object sender, RoutedEventArgs e)
        {
            var myReservationsPage = new ViewReservationPage(this);
            NavigationFrame.Content = myReservationsPage; // Stel de nieuwe inhoud in
            //My reservations Window
        }
        private void PlaceReservation(object sender, RoutedEventArgs e)
        {
            //place reservations window
        }
        private void Settings(object sender, RoutedEventArgs e)
        {
            //settings window
        }
    }
}