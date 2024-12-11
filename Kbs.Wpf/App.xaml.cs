using System.Windows;
using Kbs.Business.Session;
using Kbs.Data.Reservation;
using Kbs.Data.User;

namespace Kbs.Wpf;

public partial class App : Application
{
    public App()
    {
        DispatcherUnhandledException += (_, _) =>
        {
            MessageBox.Show("Er is een fout opgetreden waardoor de applicatie niet langer werkt. Start de applicatie opnieuw op en probeer het opnieuw.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        };
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