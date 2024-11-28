namespace Kbs.Business.Damage;

public class Damage
{
    public int BoatId {  get; set; }
    public string DamageDescription { get; set; }
    public DateTime? DateReported { get; set; }
    public byte[] Image { get; set; }
}