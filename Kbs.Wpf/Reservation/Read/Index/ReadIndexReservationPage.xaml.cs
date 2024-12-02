using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Data.Reservation;
using Kbs.Wpf.Reservation.Read.Details;

namespace Kbs.Wpf.Reservation.Read.Index;

public partial class ReadIndexReservationPage : Page
{
    private ReadIndexReservationViewModel ReadIndexReservationViewModel => (ReadIndexReservationViewModel)DataContext;
    private readonly ReservationRepository _reservationRepository = new();
    private readonly INavigationManager _navigationManager;
    private readonly ReservationValidator _reservationValidator;
    private readonly ReservationTime _reservationTime;

    public ReadIndexReservationPage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        var reservations = _reservationRepository.GetByUserId(SessionManager.Instance.Current.User.UserId);
        var sortedReservations = _reservationRepository.OrderByStatusAndTime(reservations);

        foreach (var reservation2 in sortedReservations)
        {
            var reservationId = reservation2.ReservationId;
            var date = _reservationRepository.GetDate(reservationId);
            //var aardappel = _reservationValidator.ReservationTimeHasPassed(date);
            _reservationTime.SetStatusToInactiveAsync(date);
            if (_reservationTime.Active = false)
            {
                _reservationRepository.ChangeStatus(reservationId);
            }
        }
        

        foreach (var reservation in sortedReservations)
        {
            {
                
                ReadIndexReservationViewModel.Items.Add(new ReadIndexReservationReservationViewModel()
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
        var item = (ReadIndexReservationReservationViewModel)listViewItem.DataContext;
        _navigationManager.Navigate(() => new ReadDetailsReservationPage(item.ReservationID, _navigationManager));
    }
}