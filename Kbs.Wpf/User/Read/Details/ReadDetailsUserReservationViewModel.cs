using Kbs.Business.Helpers;
using Kbs.Business.Reservation;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.User.Read.Details
{
    public class ReadDetailsUserReservationViewModel : ViewModel
    {
        private int _reservationId;
        private string _boatName;
        private DateTime _startTime;
        private string _status;

        public int ReservationId
        {
            get => _reservationId;
            set => SetField(ref _reservationId, value);
        }

        public string BoatName
        {
            get => _boatName;
            set => SetField(ref _boatName, value);
        }

        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                SetField(ref _startTime, value);
            }
        }


        public string Status
        {
            get => _status;
            set => SetField(ref _status, value);
        }

        public ReadDetailsUserReservationViewModel(ReservationEntity reservation, string boatName)
        {
            ThrowHelper.ThrowIfNull(reservation);
            ReservationId = reservation.ReservationId;
            BoatName = boatName;
            StartTime = reservation.StartTime;
            Status = reservation.Status.ToDutchString();
        }
    }
}
