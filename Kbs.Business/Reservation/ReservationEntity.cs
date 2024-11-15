using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Reservation
{
    internal class ReservationEntity
    {
        private int reservationId;
        private DateTime startDate;
        private ReservationStatus status;
        private int userId;
        //private Reservationniveau Niveau;
        private bool isForCompetition;
        private int tijdsduur;
        //private DateTime EndDate;
    }
}
