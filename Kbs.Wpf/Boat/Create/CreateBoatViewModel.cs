using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Create;

public class CreateBoatViewModel : ViewModel
{
    private string _name;
    private int _boatTypeId;
    private string _nameErrorMessage;
    private string _boatTypeErrorMessage;
    
    public ObservableCollection<CreateBoatBoatTypeViewModel> PossibleBoatTypes { get; } = new();
    
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
    
    public string NameErrorMessage
    {
        get => _nameErrorMessage;
        set => SetField(ref _nameErrorMessage, value);
    }

    public string BoatTypeErrorMessage
    {
        get => _boatTypeErrorMessage;
        set => SetField(ref _boatTypeErrorMessage, value);
    }
    
}