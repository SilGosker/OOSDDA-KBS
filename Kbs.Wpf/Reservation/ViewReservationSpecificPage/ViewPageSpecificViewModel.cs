using Kbs.Business.Reservation;
using Kbs.Wpf.Components;
using System.Windows.Media;

namespace Kbs.Wpf.Reservation.ViewReservationSpecificPage
{
    public class ViewPageSpecificViewModel : ViewModel
    {
        private int _niveau;
        private string _seats;
        private bool _hasSteeringWheel;
        private DateTime _startTime;
        private TimeSpan _length;
        private int _reservationID;
        private ReservationStatus _status = ReservationStatus.Active;

        public int Niveau
        {
            get => _niveau;
            set => SetField(ref _niveau, value);
        }
        public string Seats
        {
            get => _seats;
            set => SetField(ref _seats, value);
        }
        public ReservationStatus Status
        {
            get => _status;
            set {
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
        public bool HasSteeringWheel
        {
            get => _hasSteeringWheel;
            set => SetField(ref _hasSteeringWheel, value);
        }
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                SetField(ref _startTime, value);
                OnPropertyChanged(nameof(StartTimeFormatted));
            }
        }
        public string StartTimeFormatted => StartTime.ToString("dd-MM-yyyy HH:mm");
        public TimeSpan Length
        {
            get => _length;
            set => SetField(ref _length, value);
        }
        public int ReservationID
        {
            get => _reservationID;
            set => SetField(ref _reservationID, value);
        }
    }
}
