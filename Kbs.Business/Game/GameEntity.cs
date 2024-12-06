using System.ComponentModel.DataAnnotations.Schema;

namespace Kbs.Business.Game;

public class GameEntity
{
    [Column("GameID")]
    public int GameId { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    [Column("CourseID")]
    public int? CourseId { get; set; }
}