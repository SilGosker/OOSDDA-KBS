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
        public int Tijdsduur {  get; set; }
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
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
        public void EndTime()
        {
            DateTime end = StartTime.AddMinutes(Tijdsduur);
            Console.WriteLine(end);
        }



        //Enum nog niet duidelijk voor onderstaande
        //private Reservationniveau niveau;

    }
}
