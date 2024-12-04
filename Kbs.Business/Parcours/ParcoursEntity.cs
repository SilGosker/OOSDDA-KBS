using System.ComponentModel.DataAnnotations.Schema;

namespace Kbs.Business.Parcours;

public class ParcoursEntity
{
    [Column("ParcoursID")]
    public int ParcoursId { get; set; }

    public byte[] Image { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ParcoursDifficulty Difficulty { get; set; }
}