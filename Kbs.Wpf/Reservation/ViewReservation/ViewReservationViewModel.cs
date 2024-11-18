using Kbs.Data;
using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.Reservation.ViewReservation
{
    public class ViewReservationViewModel : ViewModel
    {
        private int _niveau;
        private int _zitplaatsen;
        private bool _stuur;
        private DateTime _tijdsstip;
        private int _tijdsduur;
        private int _reservationID;

        public int Niveau
        {
            get => _niveau;
            set => SetField(ref _niveau, value);
        }
        public int Zitplaatsen
        {
            get => _zitplaatsen;
            set => SetField(ref _zitplaatsen, value);
        }
        public bool Stuur
        {
            get => _stuur;
            set => SetField(ref _stuur, value);
        }
        public DateTime Tijdsstip
        {
            get => _tijdsstip;
            set => SetField(ref _tijdsstip, value);
        }
        public int Tijdsduur
        {
            get => _tijdsduur;
            set => SetField(ref _tijdsduur, value);
        }
        public int ReservationID
        {
            get => _reservationID;
            set => SetField(ref _reservationID, value);
        }
    }
}
