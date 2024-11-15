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
        public int aantalReservations = 0;
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EndTime => StartTime + Duration;
        
        public ReservationStatus Status { get; set; }
        public bool IsForCompetition { get; set; }
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
