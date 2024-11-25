using Kbs.Business.Boat;

namespace Kbs.Business.Reservation;
[Obsolete]
public class ReservationTimeFactory
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IBoatRepository _boatRepository;
    private readonly TimeSpan _interval;
    public ReservationTimeFactory(IReservationRepository reservationRepository, IBoatRepository boatRepository)
        : this(reservationRepository, boatRepository, TimeSpan.FromMinutes(30))
    {

    }

    public ReservationTimeFactory(IReservationRepository reservationRepository, IBoatRepository boatRepository, TimeSpan interval)
    {
        _reservationRepository = reservationRepository;
        _boatRepository = boatRepository;
        _interval = interval;
    }

    public List<TimeSpan> GetPossibleReservationTimes(ReservationTime time, int boatId)
    {
        var boat = _boatRepository.GetById(boatId);
        var nearestReservation = _reservationRepository.GetNearestReservation(boat, time.startTime);
        var maxEnd = time.startTime.AddHours(2).TimeOfDay;
        
        if (nearestReservation != null)
        {
            maxEnd = nearestReservation.StartTime.TimeOfDay < maxEnd
                ? nearestReservation.StartTime.TimeOfDay
                : maxEnd;
        }

        var start = _interval;
        var end = maxEnd - time.startTime.TimeOfDay;
        List<TimeSpan> reservationTimes = new();

        while (start <= end)
        {
            reservationTimes.Add(start);
            start += _interval;
        }

        return reservationTimes;
    }

    public List<ReservationTime> GetPossibleReservationTimes(DateTime date, TimeSpan start, TimeSpan end)
    {
        // get start of week
        date = date.Date;
        List<ReservationTime> reservationTimes = new List<ReservationTime>();

        while (start < end)
        {
            reservationTimes.Add(new ReservationTime(date.Add(start), date.Add(start.Add(_interval))));
            start = start.Add(_interval);
        }

        return reservationTimes;
    }

    public List<DateTime> GetDaysOfWeek(DateTime date)
    {
        // get start of week
        date = date.AddDays(-(int)date.DayOfWeek);
        List<DateTime> daysOfWeek = new List<DateTime>();
        
        while (date.DayOfWeek != DayOfWeek.Saturday) {
            daysOfWeek.Add(date);
            date = date.AddDays(1);
        }

        return daysOfWeek;
    }
    public List<ReservationTime> GetPossibleReservationTimes(TimeSpan start, TimeSpan end, DateTime date, int boatId)
    {
        date = date.Date;
        var reservationTimes = GetPossibleReservationTimes(date, start, end);

        var boat = _boatRepository.GetById(boatId);
        List<ReservationEntity> reservations = _reservationRepository.GetByBoatIdAndDay(boat, date);

        if (reservations.Count == 0)
        {
            // no reservations for that boat on that day, return all possible times
            return GetPossibleReservationTimes(date, ReservationValidator.Morning, ReservationValidator.Evening);
        }

        foreach (ReservationEntity reservation in reservations)
        {
            foreach (ReservationTime possibleReservationTime in GetPossibleReservationTimes(date, reservation.StartTime.TimeOfDay, reservation.EndTime.TimeOfDay))
            {
                var reservationTime = reservationTimes.Single(e =>
                    e.startTime == possibleReservationTime.startTime
                    && e.endTime == possibleReservationTime.endTime);

                //reservationTime.CanBeReserved = false;
            }

        }

        return reservationTimes.ToList();
    }
}