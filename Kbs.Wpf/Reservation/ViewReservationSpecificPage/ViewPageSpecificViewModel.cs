using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.ViewReservationSpecificPage
{
    public class ViewPageSpecificViewModel : ViewModel
    {
        private int _niveau;
        private int _seats;
        private bool _hasSteeringWheel;
        private DateTime _tijdsstip;
        private TimeSpan _length;
        private int _reservationID;
        private int _status;

        public int Niveau
        {
            get => _niveau;
            set => SetField(ref _niveau, value);
        }
        public int Seats
        {
            get => _seats;
            set => SetField(ref _seats, value);
        }
        public int Status
        {
            get => _status;
            set => SetField(ref _status, value);
        }
        public bool HasSteeringWheel
        {
            get => _hasSteeringWheel;
            set => SetField(ref _hasSteeringWheel, value);
        }
        public DateTime StartTime
        {
            get => _tijdsstip;
            set => SetField(ref _tijdsstip, value);
        }
        public string StartTimeFormatted => StartTime.ToString("dd-MM-yyyy");
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
        public object Items { get; internal set; }
    }
}
