using Kbs.Business.Reservation;
using Kbs.Wpf.Components;
using System.Windows.Input;
using System.Windows.Media;

namespace Kbs.Wpf.Reservation.ViewReservationGeneralPage
{

    public class ViewReservationViewModel : ViewModel
    {
        private int _niveau;
        private int _zitplaatsen;
        private bool _stuur;
        private DateTime _startTime;
        private TimeSpan _tijdsduur;
        private int _reservationID;
        private ICommand _viewMore;
        public ReservationStatus _status;
        public ViewReservationViewModel(Action<ViewReservationViewModel> action)
        {
            ViewMore = new RelayCommand<ViewReservationViewModel>(action);
        }
        public ReservationStatus Status
        {
            get => _status;
            set
            {
                SetField(ref _status, value);
                OnPropertyChanged(nameof(StatusColor));
            }
        }
        public Brush StatusColor
        {
            get
            {
                if (Status == ReservationStatus.Active)
                {
                    return Brushes.Green;
                }
                return Brushes.Red;
            }
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
        public int Seats
        {
            get => _zitplaatsen;
            set => SetField(ref _zitplaatsen, value);
        }
        public bool HasSteeringWheel
        {
            get => _stuur;
            set => SetField(ref _stuur, value);
        }
        public DateTime StartTime
        {
            get => _startTime;
            set => SetField(ref _startTime, value);
        }
        public string StartTimeFormatted => StartTime.ToString("dd-MM-yyyy HH:mm");
        public string TijdsduurFormatted => $"{Length.TotalMinutes:F0} min";

        public TimeSpan Length
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
