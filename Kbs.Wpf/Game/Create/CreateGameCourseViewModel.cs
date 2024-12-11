using Kbs.Business.Course;

namespace Kbs.Wpf.Game.Create;

public class CreateGameCourseViewModel
{
    public CreateGameCourseViewModel(CourseEntity course)
    {
        Name = course.Name;
        Id = course.CourseId;
    }

    public int Id { get; }
    public string Name { get; }

    public override string ToString()
    {
        return Name;
    }
}