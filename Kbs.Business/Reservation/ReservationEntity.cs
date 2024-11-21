using Kbs.Business.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Reservation
{
    public class ReservationEntity
    {
        public int ReservationId { get; set; }
        //Onderstaand staat niet in TO
        public int totalReservations = 0;
        public int UserId { get; set; }
        public int BoatId { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Length { get; set; }
        public DateTime EndTime => StartTime + Length;
        
        public ReservationStatus Status { get; set; }
        public bool IsForCompetition { get; set; }
        //Onderstaande 4 staan nog niet in TO
        public bool Is(ReservationStatus status)
        {
            return Status == status;
        }
    }
}
