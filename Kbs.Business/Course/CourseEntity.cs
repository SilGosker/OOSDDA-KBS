using System.ComponentModel.DataAnnotations.Schema;

namespace Kbs.Business.Course;

public class CourseEntity
{
    [Column("CourseID")]
    public int CourseId { get; set; }

    public byte[] Image { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CourseDifficulty Difficulty { get; set; }
}