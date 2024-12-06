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
    public string Name => _name;

    public override string ToString()
    {
        return _name;
    }
}