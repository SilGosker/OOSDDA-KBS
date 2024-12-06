using Kbs.Business.Course;

namespace Kbs.Wpf.Game.Create;

public class CreateGameCourseViewModel
{
    private readonly string _name;

    public CreateGameCourseViewModel(CourseEntity course)
    {
        _name = course.Name;
        Id = course.CourseId;
    }

    public int Id { get; }

    public override string ToString()
    {
        return _name;
    }
}