namespace Kbs.Business.BoatType;

public class BoatTypeEntity
{
    public int BoatTypeID { get; set; }
    public string Name { get; set; }
    public int RequiredExperience { get; set; }
    public int Speed { get; set; }
    public int Seats { get; set; }
    public bool HasSteeringWheel { get; set; }
}