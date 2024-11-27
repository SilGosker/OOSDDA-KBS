namespace Kbs.Business.Boat;

public static class BoatStatusExtensions
{
    public static string ToDutchString(this BoatStatus status)
    {
        return status switch
        {
            BoatStatus.Operational => "Operationeel",
            BoatStatus.Maintaining => "In onderhoud",
            BoatStatus.Broken => "Kapot",
            _ => "Onbekend"
        };
    }
}