namespace Kbs.Business.BoatType;

public static class BoatTypeSeatsExtensions
{
    public static string ToDutchString(this BoatTypeSeats seats)
    {
        return seats switch
        {
            BoatTypeSeats.One => "Één zitplaats",
            BoatTypeSeats.Two => "Twee zitplaatsen",
            BoatTypeSeats.Three => "Drie zitplaatsen",
            BoatTypeSeats.Four => "Vier zitplaatsen",
            BoatTypeSeats.Eight => "Acht zitplaatsen",
            _ => "Onbekend"
        };
    }
}