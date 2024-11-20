using System.Collections.ObjectModel;
using Kbs.Business.BoatType;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Index;

public class BoatIndexViewModel : ViewModel
{
    private string _name;
    private int _boatTypeId;

    public BoatIndexViewModel()
    {
        // Add default boat type that shows all boats when selected
        BoatTypes.Add(new BoatIndexBoatTypeViewModel(new BoatTypeEntity()
        {
            Name = "Alle boottypes",
            BoatTypeID = -1
        }));
    }

    public ObservableCollection<BoatIndexBoatViewModel> Items { get; } = new();
    public ObservableCollection<BoatIndexBoatTypeViewModel> BoatTypes { get; } = new();
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }
    public int BoatTypeId
    {
        get => _boatTypeId;
        set => SetField(ref _boatTypeId, value);
    }
        
}