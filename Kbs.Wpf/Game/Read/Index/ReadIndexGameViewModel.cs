using System.Collections.ObjectModel;
using Kbs.Business.Course;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Game.Read.Index;

public class ReadIndexGameViewModel : ViewModel
{
    private string _name;
    private int _courseId;
    
    public ReadIndexGameViewModel()
    {
        // Add default course that shows all games when selected
        Courses.Add(new ReadIndexGameCourseViewModel(new CourseEntity()
        {
            Name = "Alle parcours",
            CourseId = -1
        }));
    }
    
    public ObservableCollection<ReadIndexGameGameViewModel> Items { get; } = new();
    public ObservableCollection<ReadIndexGameCourseViewModel> Courses { get; } = new();
    
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }
    
    public int CourseId
    {
        get => _courseId;
        set => SetField(ref _courseId, value);
    }
    
}