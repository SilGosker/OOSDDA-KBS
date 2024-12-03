using Kbs.Business.Boat;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Components;

public class BoatStatusViewModel : ViewModel
{
    public BoatStatusViewModel(BoatStatus types)
    {
        BoatStatus = types;
    }

    public BoatStatus BoatStatus { get; }

    // Override ToString to display the BoatStatuses in Dutch
    public override string ToString()
    {
        return BoatStatus.ToDutchString();
    }
}
