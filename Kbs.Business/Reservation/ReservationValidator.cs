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
            return false;
        }
        public bool IsWithinDaylightHours(ReservationEntity reservation) 
        {
            var StartTime = TimeOnly.FromDateTime(reservation.StartTime);
            var EndTime = TimeOnly.FromDateTime(reservation.EndTime);
            TimeOnly Morning = new TimeOnly(8, 59, 59);
            TimeOnly Evening = new TimeOnly(16, 59, 59);
            return StartTime > Morning && EndTime < Evening;
        }
        public bool IsDurationValid(ReservationEntity reservation) 
        {
            return reservation.Length.TotalMinutes <= 120;
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

    }
}
