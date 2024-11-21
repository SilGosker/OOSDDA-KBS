using System.Collections.ObjectModel;

namespace Kbs.Wpf.Reservation.ViewReservationGeneralPage;
public class ViewReservationPageViewModel
{
    public ObservableCollection<ViewReservationViewModel> Items { get; } = new();
}
