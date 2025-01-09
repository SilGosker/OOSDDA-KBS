using System.ComponentModel.DataAnnotations.Schema;

namespace Kbs.Business.Medal
{
    public class MedalEntity
    {
        [Column("MedalID")]
        public int MedalId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("BoatID")]
        public int? BoatId { get; set; }
        [Column("GameID")]
        public int GameId { get; set; }
        [Column("Medal")]
        public MedalMaterial Material { get; set; }
    }
}
