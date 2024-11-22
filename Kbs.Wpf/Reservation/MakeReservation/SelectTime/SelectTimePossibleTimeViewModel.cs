using Kbs.Business.Reservation;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.MakeReservation.SelectTime;

public class SelectTimePossibleTimeViewModel : ViewModel
{
    public SelectTimePossibleTimeViewModel(ReservationTime reservationTime)
    {
        ReservationTime = reservationTime;
        CanBeReserved = reservationTime.CanBeReserved;
        FormattedTime = CanBeReserved ? reservationTime.StartTime.ToString("HH:mm") : "Gereserveerd";
    }

    private string _formattedTime;

    public string FormattedTime
    {
        get => _formattedTime;
        set => SetField(ref _formattedTime, value);
    }
    public ReservationTime ReservationTime { get; }
    public bool CanBeReserved { get; }
}