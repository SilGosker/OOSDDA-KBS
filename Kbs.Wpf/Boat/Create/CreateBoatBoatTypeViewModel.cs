using Kbs.Business.BoatType;

namespace Kbs.Wpf.Boat.Create;

public class CreateBoatBoatTypeViewModel
{
    private readonly string _name;
    
    public CreateBoatBoatTypeViewModel(BoatTypeEntity boatType)
    {
        _name = boatType.Name;
        BoatTypeId = boatType.BoatTypeID;
    }
    
    public int BoatTypeId { get; private set; }
    
    public override string ToString()
    {
        return _name;
    }
}