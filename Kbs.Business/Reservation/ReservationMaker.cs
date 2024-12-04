using Kbs.Business.Boat;
using Kbs.Business.BoatType;

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

        //if there are no reservations on the day, create the full block of reservable time
        if (reservations.Count == 0)
        {
            result.Add(new ReservationTime(selectedDayMorning, selectedDayEvening));
        }
        //if there are reservations on the day, remove reservations out of the reservable time
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

    public List<ReservationTime> MakeReservableTimes(DateTime selectedDay, List<BoatEntity> selectedBoats)
    {
        List<ReservationTime> result = new List<ReservationTime>();
        List<ReservationEntity> allReservations = new List<ReservationEntity>();

        foreach (var boat in selectedBoats)
        {
            allReservations.AddRange(_repository.GetByBoatIdAndDay(boat, selectedDay));
        }

        allReservations = allReservations.OrderBy(r => r.StartTime).ToList();

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

        //if there are no reservations on the day, create the full block of reservable time
        if (allReservations.Count == 0)
        {
            result.Add(new ReservationTime(selectedDayMorning, selectedDayEvening));
        }
        //if there are reservations on the day, remove reservations out of the reservable time
        else
        {
            DateTime startTimeAvailable = new DateTime();
            startTimeAvailable = selectedDayMorning;
            DateTime endTimeAvailable = new DateTime();
            foreach (ReservationEntity t in allReservations)
            {
                endTimeAvailable = t.StartTime;
                result.Add(new ReservationTime(startTimeAvailable, endTimeAvailable));
                startTimeAvailable = t.EndTime;
            }
            result.Add(new ReservationTime(startTimeAvailable, selectedDayEvening));
        }
        return result;
    }
}

