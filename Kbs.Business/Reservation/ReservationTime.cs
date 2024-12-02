namespace Kbs.Business.Reservation;

public class ReservationTime
{
    public ReservationTime(DateTime startTime, DateTime endTime)
    {
        this.EndTime = endTime;
        this.StartTime = startTime;
        TimeSpan difference = endTime - startTime;
        Length = difference.TotalHours;
    }


    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public double Length { get; set; }

    public bool Active = true;
    public async Task SetStatusToInactiveAsync(DateTime EndTime)
    {
        var time = EndTime - DateTime.Now;
        if (time > TimeSpan.Zero)
        {
            await Task.Delay(time);
        }
        Active = false;
        ReservationEntity res = new ReservationEntity();
        res.Status = ReservationStatus.Expired;
    }
}