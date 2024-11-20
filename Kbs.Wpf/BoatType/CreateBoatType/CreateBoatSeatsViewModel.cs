using Kbs.Business.BoatType;

namespace Kbs.Wpf.BoatType.CreateBoatType;

public class CreateBoatSeatsViewModel
{
    public CreateBoatSeatsViewModel(BoatTypeSeats seats)
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