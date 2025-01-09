using Kbs.Business.BoatType;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Create;

public class CreateBoatBoatTypeViewModel(BoatTypeEntity boatType) : ViewModel
{
    private readonly string _name = boatType.Name;

    public int BoatTypeId { get; private set; } = boatType.BoatTypeId;

    public override string ToString()
    {
        return _name;
    }
}