namespace Kbs.Business.Reservation;

public class ReservationTime
{
    public ReservationTime()
    {

    }
    public ReservationTime(DateTime startTime, DateTime endTime)
    {
        this.StartTime = startTime;
        this.EndTime = endTime;
        CanBeReserved = true;
    }

    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public bool CanBeReserved { get; set; }

}