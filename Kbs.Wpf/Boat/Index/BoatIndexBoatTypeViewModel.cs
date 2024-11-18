using Kbs.Business.BoatType;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Index;

public class BoatIndexBoatTypeViewModel : ViewModel
{
    public BoatIndexBoatTypeViewModel(BoatTypeEntity boatType)
    {
        Name = boatType.Name;
        Id = boatType.BoatTypeID;
    }
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