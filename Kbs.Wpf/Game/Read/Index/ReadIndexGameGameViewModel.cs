using System.Windows.Media;
using Kbs.Business.Extentions;
using Kbs.Business.Game;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Game.Read.Index;

public class ReadIndexGameGameViewModel : ViewModel
{
    private string _name;
    private int _id;
    private DateTime _date;
    private string _course;
    
    public ReadIndexGameGameViewModel(GameEntity game, ReadIndexGameCourseViewModel course)
    {
        Name = game.Name;
        Id = game.GameId;
        Date = game.Date;
        Course = course?.Name ?? "Onbekend";
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
    
    public DateTime Date
    {
        get => _date;
        set
        {
            SetField(ref _date, value);
            OnPropertyChanged(nameof(DateFormatted));
        }
    }
    
    public Brush DateColor => Date > DateTime.Now ? Brushes.Green : Brushes.Red;

    public string Course
    {
        get => _course;
        set => SetField(ref _course, value);
    }

    public string DateFormatted => Date.ToDutchString();
    

}