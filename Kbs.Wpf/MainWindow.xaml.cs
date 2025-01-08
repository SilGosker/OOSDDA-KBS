using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Course;
using Kbs.Business.Game;
using Kbs.Business.Medal;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Wpf.Boat.Read.Index;
using Kbs.Wpf.BoatType.Read.Index;
using Kbs.Wpf.Components;
using Kbs.Wpf.Course.Read.Index;
using Kbs.Wpf.User.Update;
using Kbs.Wpf.Reservation.Create.SelectBoatType;
using Kbs.Wpf.Reservation.Read.Index;
using Kbs.Wpf.Session.Login;
using Kbs.Wpf.Medal.Read;
using Kbs.Wpf.User.Read.Index;
using Kbs.Wpf.Game.Read.Index;

namespace Kbs.Wpf;

public partial class MainWindow : Window, INavigationManager
{
    private MainViewModel ViewModel => (MainViewModel)DataContext;
    private PageHistoryService _pageHistoryService = new();

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

            AddNavigationItem<BoatTypeEntity>(() => new ViewBoatTypesPage(this), "\u2693 Overzicht Boottypen", true);
            AddNavigationItem<BoatEntity>(() => new ReadIndexBoatPage(this), "\ud83d\udea4 Overzicht Boten", false);
        }

        if (user.IsMember() || user.IsGameCommissioner())
        {
            AddNavigationItem<ReservationEntity>(() => new ReadIndexReservationPage(this), "\ud83d\uddd3\ufe0f Mijn reserveringen", true);
            AddNavigationItem<ReservationTime>(() => new SelectBoatTypePage(this), "\u2795 Nieuwe reservering", false);
        }

        if (user.IsMember())
        {
            AddNavigationItem<MedalEntity>(() => new ReadMedalPage(), "\ud83c\udfc5 Mijn Medailles", false);
        }

        if (user.IsGameCommissioner())
        {
            AddNavigationItem<GameEntity>(() => new ReadIndexGamePage(this), "\ud83c\udfc1 Overzicht Wedstrijden", true);
            AddNavigationItem<UserEntity>(() => new ReadIndexUserPage(this), "\ud83d\udc65 Overzicht Leden", false);
            AddNavigationItem<CourseEntity>(() => new ReadIndexCoursePage(this), "\ud83d\udccd Overzicht Parcours", false);
        }

        AddNavigationItem<Business.Session.Session>(() => new UpdateUserPage(this), "\u2699\ufe0f Instellingen", true);
    }

    private void AddNavigationItem<T>(Func<Page> creator, string title, bool startsNew)
    {
        ViewModel.NavigationItems.Add(new NavigationItemViewModel(this, creator)
        {
            Name = title,
            StartsNewSection = startsNew,
            Type = typeof(T)
        });
    }

    private void HighlightNavigationItem(Type type)
    {
        foreach (NavigationItemViewModel navigationItem in ViewModel.NavigationItems)
        {
            navigationItem.IsHighlighted = navigationItem.Type == type;
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
        HighlightForAttribute highlightForAttribute;
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
                highlightForAttribute = page.GetType().GetCustomAttribute<HighlightForAttribute>();

                HighlightNavigationItem(highlightForAttribute?.Type);

                _pageHistoryService.TryPush(page);
                NavigationFrame.Navigate(page);
                return;
            }

            MessageBox.Show(this, "U heeft geen toegang tot deze functie", "Toegang geweigerd");
            return;
        }

        page = creator();
        highlightForAttribute = page.GetType().GetCustomAttribute<HighlightForAttribute>();

        HighlightNavigationItem(highlightForAttribute?.Type);
        
        _pageHistoryService.TryPush(page);

        NavigationFrame.Navigate(page);
    }

    private void LogOut(object sender, RoutedEventArgs e)
    {
        SessionManager.Instance.Logout();
        var loginWindow = new LoginWindow();
        loginWindow.Show();
        Close();
    }

    private void GoToPreviousPage(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Escape)
        {
            return;
        }

        var page = _pageHistoryService.Previous();

        if (page != null)
        {
            NavigationFrame.Navigate(page);
        }
    }
}