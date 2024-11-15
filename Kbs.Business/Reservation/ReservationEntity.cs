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
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EndTime => StartTime + Duration;
        
        public ReservationStatus Status { get; set; }
        public bool IsForCompetition { get; set; }
        //Onderstaande 4 staan nog niet in TO
        public bool IsDeleted()
        {
            return Is(ReservationStatus.Deleted);
        }
        public bool IsExpired()
        {
            return Is(ReservationStatus.Expired);
        }
        public bool IsActive()
        {
            return Is(ReservationStatus.Active);
        }
        public bool Is(ReservationStatus status)
        {
            return (Status & status) != 0;
        }
    }
}
