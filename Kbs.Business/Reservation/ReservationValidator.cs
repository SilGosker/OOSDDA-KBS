using Kbs.Business.User;
using System;

namespace Kbs.Business.Reservation;

public class ReservationValidator
{
    public static TimeOnly Evening => new(17, 0, 0);
    public static TimeOnly Morning => new(9, 0, 0);

    public Dictionary<string, string> ValidForCreation(ReservationEntity reservation) { return new Dictionary<string, string>(); }
    public Dictionary<string, string> ValidForUpdates(ReservationEntity reservation) { return new Dictionary<string, string>(); }

    public bool IsReservationLimitReached(int totalreservations)
    {
        if (totalreservations > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsWithinDaylightHours(ReservationEntity reservation)
    {
        var StartTime = TimeOnly.FromDateTime(reservation.StartTime);
        var EndTime = TimeOnly.FromDateTime(reservation.EndTime);
        return StartTime > Morning && EndTime < Evening;
    }
    public bool IsDurationValid(ReservationEntity reservation)
    {
        return reservation.Length.
            Minutes <= 120;
    }
    public bool CompetitionReservationValidator(ReservationEntity reservation)
    {
        if (!reservation.IsForCompetition)
        {
            IsDurationValid(reservation);
            return false;
        }
        else
        {
            return true;
        }
    }
}