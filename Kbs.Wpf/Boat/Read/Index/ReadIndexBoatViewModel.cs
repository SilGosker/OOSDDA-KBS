using System.Collections.ObjectModel;
using Kbs.Business.BoatType;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Read.Index;

public class ReadIndexBoatViewModel : ViewModel
{
    private string _name;
    private int _boatTypeId;

    public ReadIndexBoatViewModel()
    {
        // Add default boat type that shows all boats when selected
        BoatTypes.Add(new ReadIndexBoatBoatTypeViewModel(new BoatTypeEntity()
        {
            Name = "Alle boottypes",
            BoatTypeId = -1
        }));
    }

    public ObservableCollection<ReadIndexBoatBoatViewModel> Items { get; } = new();
    public ObservableCollection<ReadIndexBoatBoatTypeViewModel> BoatTypes { get; } = new();
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