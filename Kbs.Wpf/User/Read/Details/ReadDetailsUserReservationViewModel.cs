﻿using Kbs.Business.Helpers;
using Kbs.Business.Reservation;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.User.Read.Details
{
    public class ReadDetailsUserReservationViewModel : ViewModel
    {
        private int _reservationId;
        private int _boatId;
        private DateTime _startTime;
        private string _status;

        public int ReservationId
        {
            get => _reservationId;
            set => SetField(ref _reservationId, value);
        }

        public int BoatId
        {
            get => _boatId;
            set => SetField(ref _boatId, value);
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

        public ReadDetailsUserReservationViewModel(ReservationEntity reservation)
        {
            ThrowHelper.ThrowIfNull(reservation);
            ReservationId = reservation.ReservationId;
            BoatId = reservation.BoatId;
            StartTime = reservation.StartTime;
            Status = reservation.Status.ToDutchString();
        }
    }
}
