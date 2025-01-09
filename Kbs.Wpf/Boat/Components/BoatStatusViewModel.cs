using Kbs.Business.Boat;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Components;

public class BoatStatusViewModel(BoatStatus types) : ViewModel
{
    public BoatStatus BoatStatus { get; } = types;

    // Override ToString to display the BoatStatuses in Dutch
    public override string ToString()
    {
        return BoatStatus.ToDutchString();
    }
}
