using Kbs.Business.BoatType;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.Components;

public class BoatTypeSeatsViewModel : ViewModel
{
    public BoatTypeSeatsViewModel(BoatTypeSeats seats)
    {
        BoatTypeSeats = seats;
    }

    public BoatTypeSeats BoatTypeSeats { get; }

    // Override ToString to display the BoatTypeSeats in Dutch
    public override string ToString()
    {
       return BoatTypeSeats.ToDutchString();
    }
}