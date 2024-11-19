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
            if (reservation.totalReservations > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsWithinDaylightHours(ReservationEntity reservation) 
        {
            var StartTime = TimeOnly.FromDateTime(reservation.StartTime);
            var EndTime = TimeOnly.FromDateTime(reservation.EndTime);
            TimeOnly Morning = new TimeOnly(9, 0, 0);
            TimeOnly Evening = new TimeOnly(17, 0, 0);
            if (StartTime < Morning || EndTime > Evening)
            {
                return false;
            } else
            {
                return true;
            }
        }
        public bool IsDurationValid(ReservationEntity reservation) 
        {
            if (reservation.Length.TotalMinutes > 120)
            {
                return false;
            }
            else
            {
                return true;
            }
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
