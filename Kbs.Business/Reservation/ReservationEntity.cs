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
        public int Tijdsduur {  get; set; }
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        
        public reservationStatus Status { get; set; }
        public bool IsForCompetition { get; set; }
        public bool isVerwijderd()
        {
            return Is(reservationStatus.Verwijderd);
        }
        public bool isVerlopen()
        {
            return Is(reservationStatus.Verlopen);
        }
        public bool isActief()
        {
            return Is(reservationStatus.Actief);
        }
        public bool Is(reservationStatus status)
        {
            return (Status & status) != 0;
        }
        public void EndTimeMethod()
        {
            DateTime end = new DateTime(StartTime.Year, StartTime.Month, StartTime.Day);
            end += Duration;
            EndTime = end;
        }



        //Enum nog niet duidelijk voor onderstaande
        //private Reservationniveau niveau;

    }
}
