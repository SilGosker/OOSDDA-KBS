using Kbs.Business.BoatType;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.Components;

public class BoatTypeSeatsViewModel(BoatTypeSeats seats) : ViewModel
{
    public BoatTypeSeats BoatTypeSeats { get; } = seats;

    // Override ToString to display the BoatTypeSeats in Dutch
    public override string ToString()
    {
       return BoatTypeSeats.ToDutchString();
    }
}