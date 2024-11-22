using Kbs.Wpf.Reservation.ViewReservationGeneralPage;
using System.Collections.ObjectModel;

namespace Kbs.Wpf.Reservation.ViewReservationSpecificPage;

public class ViewReservationPageSpecificViewModel
{
    public ObservableCollection<ViewReservationViewModel> Items { get; } = new();
}