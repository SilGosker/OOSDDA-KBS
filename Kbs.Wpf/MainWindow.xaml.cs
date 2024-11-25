using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Session;
using Kbs.Wpf.Attributes;
using Kbs.Wpf.Boat.Create;
using Kbs.Wpf.User.Update;
using Kbs.Wpf.Boat.Index;
using Kbs.Wpf.BoatType.CreateBoatType;
using Kbs.Wpf.BoatType.ViewBoatTypes;
using Kbs.Wpf.Reservation.ViewReservationGeneralPage;
using Kbs.Wpf.Session.Login;

namespace Kbs.Wpf;

public partial class MainWindow : Window, INavigationManager
{
    private MainViewModel ViewModel => (MainViewModel)DataContext;
    protected override void OnClosed(EventArgs e)
    {
        SessionManager.Instance.SessionTimeExpired -= SessionExpired;
        base.OnClosed(e);
    }

    public MainWindow()
    {
        InitializeComponent();

        SessionManager.Instance.SessionTimeExpired += SessionExpired;

        var user = SessionManager.Instance.Current.User;

        // todo: add navigation items
        if (user.IsMember() || user.IsGameCommissioner())
        {
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ViewReservationPage(this)) { Name = "Mijn reserveringen" });
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new Page()) {Name = "Plaatsen reservering"});
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new AccountView(this)) {Name = "Instellingen"});
                
        }

        if (user.IsMaterialCommissioner())
        {
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new CreateBoatTypePage(this)) { Name = "Boottype aanmaken" });
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new BoatIndexPage(this)) { Name = "Overzicht boten" });
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new CreateBoatPage(this)) { Name = "Boot aanmaken" });
                ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ViewBoatTypesPage(this)) { Name = "Detail Pagina Boottype" });
        }
    }

    private async void SessionExpired(object sender, SessionTimeExpiredEventArgs args)
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
            SessionManager.Instance.ExtendSession();
            MessageBox.Show("Uw sessie is verlengd.", "Sessie verlengd", MessageBoxButton.OK);
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