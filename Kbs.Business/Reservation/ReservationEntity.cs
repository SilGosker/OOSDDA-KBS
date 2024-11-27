using System.ComponentModel.DataAnnotations.Schema;

namespace Kbs.Business.Reservation;

public class ReservationEntity
{
    [Column("ReservationID")]
    public int ReservationId { get; set; }
    [Column("UserID")]
    public int UserId { get; set; }
    [Column("BoatID")]
    public int BoatId { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Length { get; set; }
    public DateTime EndTime => StartTime + Length;
    public ReservationStatus Status { get; set; }
    public bool IsForCompetition { get; set; }
}