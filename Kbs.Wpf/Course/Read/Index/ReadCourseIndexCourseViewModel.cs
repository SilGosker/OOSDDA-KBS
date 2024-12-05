using Kbs.Business.Course;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Course.Read.Index;

public class ReadCourseIndexCourseViewModel : ViewModel
{
    private int _parcourId;
    private string _name;
    private string _difficulty;

    public ReadCourseIndexCourseViewModel(CourseEntity course)
    {
        ParcourId = course.ParcoursId;
        Name = course.Name;
        Difficulty = course.Difficulty.ToDutchString();
    }

    public int ParcourId
    {
        get => _parcourId;
        set => SetField(ref _parcourId, value);
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