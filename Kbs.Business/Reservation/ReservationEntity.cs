using Kbs.Business.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Reservation
{
    public class ReservationEntity
    {
        public int ReservationID { get; set; }
        //Onderstaand staat niet in TO
        public int totalReservations = 0;
        public int UserId { get; set; }
        public int BoatId { get; set; }
        public int BoatTypeId { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Length { get; set; }
        public DateTime EndTime => StartTime + Length;
        public ReservationStatus _status;        
        //Om van int naar string te converten om te kunnen tonen
        
        public ReservationStatus Status
        {
            get; set;     
        }
        public int Seats { get; set; }
        public bool IsForCompetition { get; set; }
        
    }
}

