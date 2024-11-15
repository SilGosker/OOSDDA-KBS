using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Reservation
{
    internal class ReservationStatus
    {
        public enum reservationStatus
        {
           Verwijderd = 1,
           Verlopen = 2,
           Actief = 3
        };
    }
}
