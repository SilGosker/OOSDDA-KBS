using Kbs.Business.Boat;
using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.Reservation.MakeReservation.SelectTime
    {
    public class SelectTimeViewModel : ViewModel
        {
        public ObservableCollection<string> BoatNames
            {
            get => _boatnames;
            set => SetField(ref _boatnames, value);
            }


        private ObservableCollection<string> _boatnames;

        public ObservableCollection<BoatEntity> Boats
            {
            get => _boats;
            set => SetField(ref _boats, value);
            }


        private ObservableCollection<BoatEntity> _boats;

        public ObservableCollection<DateTime> ThisWeek
            {
            get => _thisweek;
            set => SetField(ref _thisweek, value);
            }


        private ObservableCollection<DateTime> _thisweek;
        }
    }
