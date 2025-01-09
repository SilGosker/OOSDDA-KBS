using Kbs.Business.Course;

namespace Kbs.Wpf.Game.Create;

public class CreateGameCourseViewModel(CourseEntity course)
{
    public int Id { get; } = course.CourseId;
    public string Name { get; } = course.Name;

    public override string ToString()
    {
        return Name;
    }
}