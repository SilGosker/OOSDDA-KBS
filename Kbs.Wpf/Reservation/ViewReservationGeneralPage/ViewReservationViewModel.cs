using Kbs.Business.Reservation;
using Kbs.Data.Reservation;
using Kbs.Wpf.Components;
using System.Windows.Input;

namespace Kbs.Wpf.Reservation.ViewReservationGeneralPage
{

    public class ViewReservationViewModel : ViewModel
    {
        private int _niveau;
        private int _zitplaatsen;
        private bool _stuur;
        private DateTime _tijdsstip;
        private TimeSpan _tijdsduur;
        private int _reservationID;
        private ICommand _viewMore;
        
        public ViewReservationViewModel(Action<ViewReservationViewModel> action)
        {
            ViewMore = new RelayCommand<ViewReservationViewModel>(action);
        }

        public ICommand ViewMore
        {
            get => _viewMore;
            set => SetField(ref _viewMore, value);
        }

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
        public string TijdsstipFormatted => Tijdsstip.ToString("dd-MM-yyyy");
        public TimeSpan Tijdsduur
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
