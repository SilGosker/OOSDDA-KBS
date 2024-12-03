using Kbs.Business.Boat;

namespace Kbs.Business.Reservation;

public class ReservationMaker
{
    private readonly IReservationRepository _repository;

    public ReservationMaker(IReservationRepository repo)
    {
        this._repository = repo;
    }

    /// <summary>
    /// Makes a list of reservable times.
    /// </summary>
    /// <param name="selectedDay"> A datetime where the day is the desired one. </param>
    /// <param name="selectedBoat"> The boat desired to be reserved. </param>
    /// <returns> A list of ReservationTime </returns>
    public List<ReservationTime> MakeReservableTimes(DateTime selectedDay, BoatEntity selectedBoat)
    {
        List<ReservationTime> result = new List<ReservationTime>();
        List<ReservationEntity> reservations = _repository.GetByBoatIdAndDay(selectedBoat, selectedDay);

        selectedDay = selectedDay.AddDays(-1);
        selectedDay = selectedDay.AddYears(-1);
        
        DateTime selectedDayDayOnly = new DateTime();
        selectedDayDayOnly = selectedDayDayOnly.AddDays(selectedDay.DayOfYear);
        selectedDayDayOnly = selectedDayDayOnly.AddYears(selectedDay.Year);

        DateTime selectedDayMorning = new DateTime();
        selectedDayMorning = selectedDayDayOnly.AddHours(ReservationValidator.Morning.Hours);
        selectedDayMorning = selectedDayMorning.AddMinutes(ReservationValidator.Morning.Minutes);

        DateTime selectedDayEvening = new DateTime();
        selectedDayEvening = selectedDayDayOnly.AddHours(ReservationValidator.Evening.Hours);
        selectedDayEvening = selectedDayEvening.AddMinutes(ReservationValidator.Evening.Minutes);

        //if there are no reservations on the day, creates the full block of reservable time
        if (reservations.Count == 0)
        {
            result.Add(new ReservationTime(selectedDayMorning, selectedDayEvening));
        }
        //if there are reservations on the day removes reservations out of the reservable time
        else
        {
            DateTime startTimeAvailable = new DateTime();
            startTimeAvailable = selectedDayMorning;
            DateTime endTimeAvailable = new DateTime();
            foreach (ReservationEntity t in reservations)
            {
                endTimeAvailable = t.StartTime;
                result.Add(new ReservationTime(startTimeAvailable, endTimeAvailable));
                startTimeAvailable = t.EndTime;
            }
            result.Add(new ReservationTime(startTimeAvailable, selectedDayEvening));
        }
        return result;
    }

    public static string ConvertDayOfWeekToDutch (DateTime dayofweek)
    {
        switch (dayofweek.DayOfWeek.ToString()) { case "Monday": return "Maandag"; case "Tuesday": return "Dinsdag"; case "Wednesday": return "Woensdag"; case "Thursday": return "Donderdag"; case "Friday": return "Vrijdag"; case "Saturday": return "Zaterdag"; case "Sunday": return "Zondag"; default: return "geen dag gevonden"; }
    }


}

