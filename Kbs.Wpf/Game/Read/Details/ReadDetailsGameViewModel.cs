using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Game.Read.Details;

public class ReadDetailsGameViewModel : ViewModel
{
    private int _gameId;
    private string _name;
    private DateTime _date;
    private int _courseId;
    private string _courseName;
    private string _nameError;
    private string _courseError;
    private string _dateError;
    private ReadDetailsGameCourseViewModel _selectedCourse;
    
    public ObservableCollection<ReadDetailsGameBoatViewModel> Boats { get; } = new();
    public ObservableCollection<ReadDetailsGameCourseViewModel> Courses { get; } = new();
    
    public int GameId
    {
        get => _gameId;
        set => SetField(ref _gameId, value);
    }
    public string GameIdString => $"Game #{GameId}";
    
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }
    
    public DateTime Date
    {
        get => _date;
        set => SetField(ref _date, value);
    }
    
    public int CourseId
    {
        get => _courseId;
        set => SetField(ref _courseId, value);
    }
    
    public string CourseName
    {
        get => _courseName;
        set => SetField(ref _courseName, value);
    }
    
    public string NameError
    {
        get => _nameError;
        set => SetField(ref _nameError, value);
    }

    public string CourseErrorMessage
    {
        get => _courseError;
        set => SetField(ref _courseError, value);
    }

    public string DateErrorMessage
    {
        get => _dateError;
        set => SetField(ref _dateError, value);
    }
    
    public  ReadDetailsGameCourseViewModel SelectedCourse
    {
        get => _selectedCourse;
        set => SetField(ref _selectedCourse, value);
    }
}