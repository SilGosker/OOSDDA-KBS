using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.Session;
using Kbs.Data.Reservation;
using Kbs.Wpf.Reservation.Create.SelectBoatType;
using Kbs.Wpf.Reservation.Read.Details;

namespace Kbs.Wpf.Reservation.Read.Index;

public partial class ReadIndexReservationPage : Page
{
    private ReadIndexReservationViewModel ReadIndexReservationViewModel => (ReadIndexReservationViewModel)DataContext;
    private readonly ReservationRepository _reservationRepository = new();
    private readonly INavigationManager _navigationManager;

    public ReadIndexReservationPage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        var user = SessionManager.Instance.Current.User.UserId;
        var reservationsWithStatus = _reservationRepository.GetByUserId(user);       
        var sortedReservations = _reservationRepository.OrderByStatusAndTime(reservationsWithStatus);
        foreach (var reservation in sortedReservations)
        {
            ReadIndexReservationViewModel.Items.Add(new ReadIndexReservationReservationViewModel(reservation));
        }
    }

    private void ReservationClicked(object sender, MouseButtonEventArgs e)
    {
        var listViewItem = (ListViewItem)sender;
        var item = (ReadIndexReservationReservationViewModel)listViewItem.DataContext;
        _navigationManager.Navigate(() => new ReadDetailsReservationPage(item.ReservationId, _navigationManager));
    }

    private void NavigateToCreateReservationPage(object sender, System.Windows.RoutedEventArgs e)
    {
        _navigationManager.Navigate(() => new SelectBoatTypePage(_navigationManager));
    }
}