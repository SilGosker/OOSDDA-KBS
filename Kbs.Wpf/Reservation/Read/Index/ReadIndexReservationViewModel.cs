using Kbs.Business.Reservation;
﻿using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.Read.Index;
public class ReadIndexReservationViewModel : ViewModel
{
    public ObservableCollection<ReadIndexReservationReservationViewModel> Items { get; } = new();
   
}
