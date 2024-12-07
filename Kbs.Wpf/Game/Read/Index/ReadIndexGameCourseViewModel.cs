using Kbs.Business.Course;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Game.Read.Index;

public class ReadIndexGameCourseViewModel : ViewModel
{
    private string _name;
    private int _id;
    
    public ReadIndexGameCourseViewModel(CourseEntity course)
    {
        Name = course.Name;
        Id = course.CourseId;
    }
    
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }
    
    public int Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }
    
    public override string ToString()
    {
        return Name;
    }
}