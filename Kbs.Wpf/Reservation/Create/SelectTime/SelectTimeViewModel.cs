using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.Create.SelectTime
{
    public class SelectTimeViewModel : ViewModel
    {
        public ObservableCollection<SelectTimeBoatViewModel> Boats { get; } = new();


        public ObservableCollection<DateTime> ThisWeek { get; } = new();
        public ObservableCollection<string> DaysOfWeek { get; } = new();
    }
}
