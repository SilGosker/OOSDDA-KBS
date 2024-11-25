﻿using Kbs.Business.Boat;

namespace Kbs.Business.Reservation;

public class ReservationMaker
{
    public IReservationRepository repo;

    public ReservationMaker(IReservationRepository repo)
    {
        this.repo = repo;
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
        List<ReservationEntity> reservations = repo.GetByBoatIdAndDay(selectedBoat, selectedDay);

        DateTime selectedDayDayOnly = new DateTime();
        selectedDayDayOnly = selectedDayDayOnly.AddDays(selectedDay.DayOfYear - 2);
        selectedDayDayOnly = selectedDayDayOnly.AddYears(selectedDay.Year - 1);

        DateTime selectedDayMorning = new DateTime();
        selectedDayMorning = selectedDayDayOnly.AddHours(ReservationValidator.Morning.Hours);
        selectedDayMorning = selectedDayMorning.AddMinutes(ReservationValidator.Morning.Minutes);

        DateTime selectedDayEvening = new DateTime();
        selectedDayEvening = selectedDayDayOnly.AddHours(ReservationValidator.Evening.Hours);
        selectedDayEvening = selectedDayEvening.AddMinutes(ReservationValidator.Evening.Minutes);

        if (reservations.Count == 0)
        {
            result.Add(new ReservationTime(selectedDayMorning, selectedDayEvening));
        }
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
}
