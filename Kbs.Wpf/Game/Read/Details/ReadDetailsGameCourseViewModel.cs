using Kbs.Wpf.Components;

namespace Kbs.Wpf.Game.Read.Details;

public class ReadDetailsGameCourseViewModel : ViewModel
{
    private string _name;
    private int _id;

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