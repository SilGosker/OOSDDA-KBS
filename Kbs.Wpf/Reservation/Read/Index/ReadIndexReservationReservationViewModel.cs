using System.Windows.Media;
using Kbs.Business.Helpers;
using Kbs.Business.Reservation;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.Read.Index;

public class ReadIndexReservationReservationViewModel : ViewModel
{
    
    public ReadIndexReservationReservationViewModel(ReservationEntity reservation)
    {
        ThrowHelper.ThrowIfNull(reservation);
        
        StartTime = reservation.StartTime;
        Length = reservation.Length;
        ReservationId = reservation.ReservationId;
        Status = reservation.Status;
    }
    
    private DateTime _startTime;
    private TimeSpan _duration;
    private int _reservationId;
    private ReservationStatus _status;
    
    public ReservationStatus Status
    {
        get => _status;
        set
        {
            SetField(ref _status, value);
            OnPropertyChanged(nameof(StatusColor));
        }
    }
    public Brush StatusColor
    {
        get
        {
            if (Status == ReservationStatus.Active)
            {
                return Brushes.Green;
            }
            return Brushes.Red;
        }
    }
    
    public DateTime StartTime
    {
        get => _startTime;
        set => SetField(ref _startTime, value);
    }
    
    public string ReservationIdString => $"Reservering #{ReservationId}";
    public string StartTimeString => StartTime.ToString("dd-MM-yyyy HH:mm");
    public string DurationString => $"{Length.TotalMinutes:F0} min";

    public TimeSpan Length
    {
        get => _duration;
        set => SetField(ref _duration, value);
    }
    public int ReservationId
    {
        get => _reservationId;
        set => SetField(ref _reservationId, value);
    }
}