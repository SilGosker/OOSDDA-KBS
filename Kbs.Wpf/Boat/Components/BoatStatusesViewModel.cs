using Kbs.Business.Boat;

namespace Kbs.Wpf.Boat.Components;

public class BoatStatusesViewModel
{
    public BoatStatusesViewModel(BoatStatus types)
    {
        BoatStatuses = types;
    }

    public BoatStatus BoatStatuses { get; }

    // Override ToString to display the BoatStatuses in Dutch
    public override string ToString()
    {
        return BoatStatuses.ToDutchString();
    }
}
