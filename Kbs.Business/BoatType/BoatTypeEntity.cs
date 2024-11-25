using System.ComponentModel.DataAnnotations.Schema;

namespace Kbs.Business.BoatType;

public class BoatTypeEntity
{
    [Column("BoatTypeID")]
    public int BoatTypeId { get; set; }
    public string Name { get; set; }
    public BoatTypeRequiredExperience RequiredExperience { get; set; }
    public int Speed { get; set; }
    public BoatTypeSeats Seats { get; set; }
    public bool HasSteeringWheel { get; set; }
    public int Experience { get; set; }
    public bool hasWheel {get; set; }
    public int Status { get; set; }
}