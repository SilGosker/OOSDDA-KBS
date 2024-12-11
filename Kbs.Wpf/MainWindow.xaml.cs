using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Wpf.Boat.Create;
using Kbs.Wpf.Boat.Read.Index;
using Kbs.Wpf.BoatType.Create;
using Kbs.Wpf.BoatType.Read.Index;
using Kbs.Wpf.Course.Read.Index;
using Kbs.Wpf.User.Update;
using Kbs.Wpf.Damage.Read.Details;
using Kbs.Wpf.Game.Create;
using Kbs.Wpf.Reservation.Create.SelectBoatType;
using Kbs.Wpf.Reservation.Read.Index;
using Kbs.Wpf.Session.Login;
using Kbs.Wpf.Medal.Create;
using Kbs.Wpf.Medal.Read;
using Kbs.Wpf.User.Read.Index;
using Kbs.Wpf.Course.Create;
using Kbs.Wpf.Game.Read.Index;

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
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ViewBoatTypesPage(this)) { Name = "Boottypen" });
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ReadIndexBoatPage(this)) { Name = "Boten" });
        }

        if (user.IsMember() || user.IsGameCommissioner())
        {
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ReadIndexReservationPage(this)) { Name = "Mijn reserveringen" });
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new SelectBoatTypePage(this)) { Name = "Nieuwe reservering" });
        }

        if (user.IsMember())
        {
            // For sprint 3
            // ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ReadMedalPage()) { Name = "Medailles" });
        }

        if (user.IsGameCommissioner())
        {
            // For sprint 3
            // ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ReadIndexGamePage(this)) { Name = "Overzicht Wedstrijden" });
            // ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new CreateMedalPage(this, 2)) { Name = "Medailles uitreiken" });
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ReadIndexUserPage(this)) { Name = "Overzicht leden" });
            ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new ReadIndexCoursePage(this)) { Name = "Overzicht parcours" });
        }

        ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, () => new UpdateUserPage(this))
            { Name = "Instellingen" });
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