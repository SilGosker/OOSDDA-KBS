using System.Collections.ObjectModel;

namespace Kbs.Wpf.Reservation.Read.Index;
public class ReadIndexReservationViewModel
{
    public ObservableCollection<ReadIndexReservationReservationViewModel> Items { get; } = new();
}
