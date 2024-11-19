using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Details;

public class BoatDetailViewModel : ViewModel
{
    public ObservableCollection<BoatDetailReservationViewModel> Reservations { get; } = new();
    public int BoatId { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string BoatTypeName { get; set; }
}