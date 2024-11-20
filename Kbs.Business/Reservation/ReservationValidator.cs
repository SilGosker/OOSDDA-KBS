using Kbs.Business.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Reservation
{
    public class ReservationValidator
    {
        public Dictionary<string, string> ValidForCreation(ReservationEntity reservation) { return new Dictionary<string, string>(); }
        public Dictionary<string, string> ValidForUpdates(ReservationEntity reservation){ return new Dictionary<string, string>(); }
        public bool IsReservationLimitReached(UserEntity user, ReservationEntity reservation)
        {
            return reservation.totalReservations > 2;
        }
        public bool IsWithinDaylightHours(ReservationEntity reservation) 
        {
            var StartTime = TimeOnly.FromDateTime(reservation.StartTime);
            var EndTime = TimeOnly.FromDateTime(reservation.EndTime);
            TimeOnly Morning = new TimeOnly(9, 0, 0);
            TimeOnly Evening = new TimeOnly(17, 0, 0);
            return StartTime < Morning || EndTime > Evening;
        }
        public bool IsDurationValid(ReservationEntity reservation) 
        {
            return reservation.Length.TotalMinutes > 120;
        }
        public bool CompetitionReservationValidator(ReservationEntity reservation) 
        {
            if (!reservation.IsForCompetition)
            {
                IsDurationValid(reservation);
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ShowStatus(ReservationStatus status, ReservationEntity reservation)
        {
            if (status == ReservationStatus.Active)
            {
                reservation.BoolShowStatus = true;
                //Console.WriteLine("Active");
                return true;
            }
            else
            {
                reservation.BoolShowStatus = false;
                return false;
            }
        }
    }
}
