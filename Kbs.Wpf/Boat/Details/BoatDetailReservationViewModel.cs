using Kbs.Business.Reservation;
using Kbs.Business.User;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Details;

public class BoatDetailReservationViewModel : ViewModel
{
    public BoatDetailReservationViewModel(ReservationEntity reservation, UserEntity user)
    {
        StartDate = reservation.StartTime.ToString("dd-MM-yyyy HH:mm");
        EndDate = reservation.EndTime.ToString("dd-MM-yyyy HH:mm");
        ReservationId = reservation.ReservationID;
        Status = reservation.Status.ToDutchString();
    }

    public string UserName { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Status { get; set; }
    public int ReservationId { get; set; }
}