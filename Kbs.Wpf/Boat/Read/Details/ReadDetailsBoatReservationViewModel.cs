using Kbs.Business.Reservation;
using Kbs.Business.User;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Read.Details;

public class ReadDetailsBoatReservationViewModel : ViewModel
{
    private string _name;
    private string _startDate;
    private string _endDate;
    private string _status;
    private int _reservationId;
    public ReadDetailsBoatReservationViewModel(ReservationEntity reservation, UserEntity user)
    {
        Name = user.Name;
        StartDate = reservation.StartTime.ToString("dd-MM-yyyy HH:mm");
        EndDate = reservation.EndTime.ToString("dd-MM-yyyy HH:mm");
        ReservationId = reservation.ReservationId;
        Status = reservation.Status.ToDutchString();
    }

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
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