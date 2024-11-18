using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Session;
using Kbs.Wpf.Attributes;
using Kbs.Wpf.Boat.Index;
using Kbs.Wpf.User.Login;

namespace Kbs.Wpf
{
    public partial class MainWindow : Window, INavigationManager
    {
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

            Navigate(() => new BoatIndexPage());
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
                    return;
                }

                MessageBox.Show(this, "U heeft geen toegang tot deze functie", "Toegang geweigerd");
                return;
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
    }
}