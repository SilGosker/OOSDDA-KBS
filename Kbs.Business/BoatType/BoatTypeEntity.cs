namespace Kbs.Business.BoatType;

public class BoatTypeEntity
{
    public int BoatTypeID { get; set; }
    public string Name { get; set; }
    public BoatTypeRequiredExperience RequiredExperience { get; set; }
    public int Speed { get; set; }
    public BoatTypeSeats Seats { get; set; }
    public bool HasSteeringWheel { get; set; }
}