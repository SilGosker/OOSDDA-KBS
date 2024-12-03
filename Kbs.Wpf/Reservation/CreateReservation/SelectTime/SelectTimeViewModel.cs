using Kbs.Business.Boat;
using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.Reservation.CreateReservation.SelectTime
{
    public class SelectTimeViewModel : ViewModel
    {
        public ObservableCollection<SelectTimeBoatViewModel> Boats { get; } = new();


        public ObservableCollection<DateTime> ThisWeek { get; } = new();
        public ObservableCollection<string> DaysOfWeek { get; } = new();
    }
}
