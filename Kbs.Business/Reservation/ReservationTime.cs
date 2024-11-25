namespace Kbs.Business.Reservation;

public class ReservationTime
    {
    public ReservationTime(DateTime startTime, DateTime endTime)
        {
        this.EndTime = endTime;
        this.StartTime = startTime;
        if (endTime.Minute - startTime.Minute == 30)
            {
            Length += 0.5;
            }
        else if (endTime.Minute - startTime.Minute == -30)
            {
            Length -= 0.5;
            }

        Length += endTime.Hour - startTime.Hour;
        }


    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public double Length { get; set; }
    }