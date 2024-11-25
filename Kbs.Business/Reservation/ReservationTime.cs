namespace Kbs.Business.Reservation;

public class ReservationTime
    {
    public ReservationTime(DateTime startTime, DateTime endTime)
        {
        this.endTime = endTime;
        this.startTime = startTime;
        if (endTime.Minute - startTime.Minute == 30)
            {
            length += 0.5;
            }
        else if (endTime.Minute - startTime.Minute == -30)
            {
            length -= 0.5;
            }

        length += endTime.Hour - startTime.Hour;
        }


    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }
    public double length { get; set; }
    }