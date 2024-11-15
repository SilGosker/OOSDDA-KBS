using Kbs.Business.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Reservation
{
    internal class ReservationValidator
    {
        public Dictionary<string, string> ValidForCreation(ReservationEntity reservation) { return new Dictionary<string, string>(); }
        public Dictionary<string, string> ValidForUpdates(ReservationEntity reservation){ return new Dictionary<string, string>(); }
        public bool IsReservationLimitReached() { return true; }
        public bool IsWithinDaylightHours() { return true; }
        public bool IsDurationValid() { return true; }
        public bool CompetitionReservationValidator() {  return true; }
        
    }
}
