using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Details;

public class BoatDetailReservationViewModel : ViewModel
{
    public BoatDetailReservationViewModel()
    {

    }

    public string UserName { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Status { get; set; }
    public int ReservationId { get; set; }
}