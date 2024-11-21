using Kbs.Business.Reservation;
using Kbs.Business.User;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Details;

public class BoatDetailReservationViewModel : ViewModel
{
    private string _userName;
    private string _startDate;
    private string _endDate;
    private string _status;
    private int _reservationId;
    public BoatDetailReservationViewModel(ReservationEntity reservation, UserEntity user)
    {
        StartDate = reservation.StartTime.ToString("dd-MM-yyyy HH:mm");
        EndDate = reservation.EndTime.ToString("dd-MM-yyyy HH:mm");
        ReservationId = reservation.ReservationID;
        Status = reservation.Status.ToDutchString();
    }

    public string UserName
    {
        get => _userName;
        set => SetField(ref _userName, value);
    }
    public string StartDate
    {
        get => _startDate;
        set => SetField(ref _startDate, value);
    }
    public string EndDate
    {
        get => _endDate;
        set => SetField(ref _endDate, value);
    }
    public string Status
    {
        get => _status;
        set => SetField(ref _status, value);
    }
    public int ReservationId
    {
        get => _reservationId;
        set => SetField(ref _reservationId, value);
    }
}