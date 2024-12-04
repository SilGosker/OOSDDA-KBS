using Kbs.Business.Helpers;
using Kbs.Business.Reservation;
using Kbs.Wpf.BoatType.Read.Details;
using Kbs.Wpf.Components;
using Kbs.Wpf.Reservation.Read.Details;
using Kbs.Wpf.User.ViewUser.ViewUserGeneral;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kbs.Wpf.User.ViewUser.ViewUserDetail
{
    public class ViewUserValuesDetailedReservationViewModel : ViewModel
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
                OnPropertyChanged(nameof(StartTimeString));
            }
        }
        public string StartTimeString => StartTime.ToString("dd-MM");

        public string Status
        {
            get => _status;
            set => SetField(ref _status, value);
        }
        public ViewUserValuesDetailedReservationViewModel(ReservationEntity reservation)
        {
            ThrowHelper.ThrowIfNull(reservation);
            ReservationId = reservation.ReservationId;
            BoatId = reservation.BoatId;
            StartTime = reservation.StartTime;
            Status = reservation.Status.ToDutchString();
        }


    }
}
