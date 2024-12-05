using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Wpf.Boat.Create;
using Kbs.Wpf.Boat.Read.Index;
using Kbs.Wpf.BoatType.Create;
using Kbs.Wpf.BoatType.Read.Index;
using Kbs.Wpf.User.Update;
using Kbs.Wpf.Damage.Read.Details;
using Kbs.Wpf.Reservation.Create.SelectBoatType;
using Kbs.Wpf.Reservation.Read.Index;
using Kbs.Wpf.Session.Login;
using Kbs.Wpf.User.Read.Index;

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

        if (user.IsMaterialCommissioner())
        {
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ViewBoatTypesPage(this)) { Name = "Overzicht boottypen" });
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new CreateBoatTypePage(this)) { Name = "Boottype aanmaken" });
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ReadIndexBoatPage(this)) { Name = "Overzicht boten" });
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new CreateBoatPage(this)) { Name = "Boot aanmaken" });
        }
        
        if (user.IsMember() || user.IsGameCommissioner())
        {
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ReadIndexReservationPage(this)) { Name = "Mijn reserveringen" });
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new SelectBoatTypePage(this)) {Name = "Plaatsen reservering"});
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new UpdateUserPage(this)) {Name = "Instellingen"});
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ReadIndexUserPage(this)) { Name = "Inzien leden" });

        }
        ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new UpdateUserPage(this)) { Name = "Instellingen" });
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

            if (attributes.Any(e => user.Is(e.UserRole)))
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