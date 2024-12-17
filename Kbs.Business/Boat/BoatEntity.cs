using System.ComponentModel.DataAnnotations.Schema;

namespace Kbs.Business.Boat;

public class BoatEntity
{
    [Column("BoatID")]
    public int BoatId { get; set; }
    public string Name { get; set; }
    [Column("BoatTypeID")]
    public int BoatTypeId { get; set; }
    public BoatStatus Status { get; set; }
    public DateTime? DeleteRequestDate { get; set; }
    [Column("ReservationID")]
    public int ReservationId { get; set; }
    public DateTime EndDate { get; set; }
}