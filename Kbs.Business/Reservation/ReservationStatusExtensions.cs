namespace Kbs.Business.Reservation;

public static class ReservationStatusExtensions
{
    public static string ToDutchString(this ReservationStatus status)
    {
        return status switch
        {
            ReservationStatus.Active => "Actief",
            ReservationStatus.Deleted => "Verwijderd",
            ReservationStatus.Expired => "Afgerond",
            _ => "Onbekend"
        };
    }
}