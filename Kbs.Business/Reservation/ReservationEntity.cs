namespace Kbs.Business.Reservation
{
    public class ReservationEntity
    {
        public int ReservationID { get; set; }
        public int UserId { get; set; }
        public int BoatId { get; set; }
        public int BoatTypeId { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Length { get; set; }
        public DateTime EndTime => StartTime + Length;
        public ReservationStatus Status { get; set; }
        public int Seats { get; set; }
        public bool IsForCompetition { get; set; }
    }
}

