using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.Session;
using Kbs.Data.Reservation;
using Kbs.Wpf.Reservation.ViewReservationSpecificPage;

namespace Kbs.Wpf.Reservation.ViewReservationGeneralPage;

public partial class ViewReservationPage : Page
{
    private ViewReservationPageViewModel ViewModel => (ViewReservationPageViewModel)DataContext;
    private readonly ReservationRepository _reservationRepository = new();
    private readonly INavigationManager _navigationManager;

    public ViewReservationPage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        var reservations = _reservationRepository.GetByUserId(SessionManager.Instance.Current.User.UserId);
        var sortedReservations = _reservationRepository.OrderByStatusAndTime(reservations);

        foreach (var reservation in sortedReservations)
        {
            {
                ViewModel.Items.Add(new ViewReservationViewModel()
                {
                    ReservationID = reservation.ReservationId,
                    Length = reservation.Length,
                    StartTime = reservation.StartTime,
                    Status = reservation.Status,
                });
            }
        }
    }

    private void ReservationClicked(object sender, MouseButtonEventArgs e)
    {
        var listViewItem = (ListViewItem)sender;
        var item = (ViewReservationViewModel)listViewItem.DataContext;
        _navigationManager.Navigate(() => new ViewPageSpecific(item.ReservationID, _navigationManager));
    }
}