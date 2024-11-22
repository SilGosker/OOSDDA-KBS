using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.CreateReservation.SelectBoatType;

public class SelectBoatTypeViewModel : ViewModel
{
    public ObservableCollection<SelectBoatTypeBoatTypeViewModel> Items { get; } = new();
}