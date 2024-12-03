using Kbs.Business.BoatType;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Create;

public class CreateBoatBoatTypeViewModel : ViewModel
{
    private readonly string _name;
    
    public CreateBoatBoatTypeViewModel(BoatTypeEntity boatType)
    {
        _name = boatType.Name;
        BoatTypeId = boatType.BoatTypeId;
    }
    
    public int BoatTypeId { get; private set; }
    
    public override string ToString()
    {
        return _name;
    }
}