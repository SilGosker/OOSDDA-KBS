using System.Collections.ObjectModel;
using Kbs.Wpf.Components;
using System.Windows.Controls;

namespace Kbs.Wpf.Game.Create;

public class CreateGameViewModel : ViewModel
{
    private string _nameErrorMessage;
    private string _dateErrorMessage;
    private string _name;
    private DateTime _date;
    private CreateGameCourseViewModel _selectedCourse;
    public string NameErrorMessage
    {
        get => _nameErrorMessage;
        set => SetField(ref _nameErrorMessage, value);
    }

    public string DateErrorMessage
    {
        get => _dateErrorMessage;
        set => SetField(ref _dateErrorMessage, value);
    }
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

    public ObservableCollection<CreateGameCourseViewModel> PossibleCourses { get; } = new();
    public CreateGameCourseViewModel SelectedCourse
    {
        get => _selectedCourse;
        set => SetField(ref _selectedCourse, value);
    }
}