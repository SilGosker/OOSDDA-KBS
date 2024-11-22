using Kbs.Business.Boat;

namespace Kbs.Business.Reservation;

public class ReservationMaker
{
    public IReservationRepository repo;

    public ReservationMaker(IReservationRepository repo)
    {
        this.repo = repo;
    }

    public List<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS> MakeButtonList(DateTime requestedDay, BoatEntity boat)
    {
        List<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS> result =
            new List<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS>();
        List<ReservationEntity> huh = repo.GetByBoatIdAndDay(boat, requestedDay);
        if (huh.Count == 0)
        {
            DateTime temp = new DateTime();
            temp = temp.AddDays(requestedDay.DayOfYear - 2);
            temp = temp.AddYears(requestedDay.Year - 1);

            DateTime temp2 = new DateTime();
            temp2 = temp;
            temp = temp.AddHours(ReservationValidator.Morning.Hours);
            temp = temp.AddMinutes(ReservationValidator.Morning.Minutes);

            temp2 = temp2.AddHours(ReservationValidator.Evening.Hours);
            temp2 = temp2.AddMinutes(ReservationValidator.Evening.Minutes);

            result.Add(new IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS(temp, temp2));
        }
        else
        {
            DateTime temp = new DateTime();
            temp = temp.AddDays(requestedDay.DayOfYear - 2);
            temp = temp.AddYears(requestedDay.Year - 1);
            DateTime temp2 = new DateTime();
            temp2 = temp;
            temp = temp.AddHours(ReservationValidator.Morning.Hours);
            temp = temp.AddMinutes(ReservationValidator.Morning.Minutes);
            DateTime temp3 = new DateTime();
            temp3 = temp2;
            temp3 = temp3.AddHours(ReservationValidator.Evening.Hours);
            temp3 = temp3.AddMinutes(ReservationValidator.Evening.Minutes);


                
            foreach (ReservationEntity t in huh)
            {
                temp2 = t.StartTime;

                result.Add(new IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS(temp, temp2));

                temp = t.EndTime;
            }


            result.Add(new IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS(temp, temp3));
        }

        return result;
    }
}

public class IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS
{
    public IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS(DateTime startTime, DateTime endTime)
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