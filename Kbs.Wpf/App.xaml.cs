using System.Windows;
using Kbs.Business.Session;
using Kbs.Data.Reservation;
using Kbs.Data.User;

namespace Kbs.Wpf;

public partial class App : Application
{
    public App()
    {
        SessionManager.Instance = new SessionManager(new UserRepository(), TimeSpan.FromMinutes(15));
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        InitializeComponent();
        UpdateReservationStatusAsync();

    }
    private async void UpdateReservationStatusAsync()
    {
        ReservationRepository res = new ReservationRepository();
        await res.ChangeStatusAsync(); // Gebruik 'await' voor de asynchrone aanroep
    }
}