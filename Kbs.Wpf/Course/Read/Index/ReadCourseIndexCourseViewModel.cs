using Kbs.Business.Course;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Course.Read.Index;

public class ReadCourseIndexCourseViewModel : ViewModel
{
    private int _courseId;
    private string _name;
    private string _difficulty;

    public ReadCourseIndexCourseViewModel(CourseEntity course)
    {
        CourseId = course.CourseId;
        Name = course.Name;
        Difficulty = course.Difficulty.ToDutchString();
    }

    public int CourseId
    {
        get => _courseId;
        set => SetField(ref _courseId, value);
    }

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public string Difficulty
    {
        get => _difficulty;
        set => SetField(ref _difficulty, value);
    }
}