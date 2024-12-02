using System.ComponentModel.DataAnnotations.Schema;

namespace Kbs.Business.Damage;

public class DamageEntity
{
    [Column("DamageID")]
    public int DamageId { get; set; }
    public DamageStatus Status { get; set; }
    [Column("BoatID")]
    public int BoatId {  get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public byte[] Image { get; set; }
}