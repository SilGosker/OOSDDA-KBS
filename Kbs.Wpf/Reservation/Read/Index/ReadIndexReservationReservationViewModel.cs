using System.Windows.Media;
using Kbs.Business.Reservation;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.Read.Index;

public class ReadIndexReservationReservationViewModel : ViewModel
{
    private DateTime _startTime;
    private TimeSpan _length;
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
    public string StartTimeFormatted => StartTime.ToString("dd-MM-yyyy HH:mm");
    public string TijdsduurFormatted => $"{Length.TotalMinutes:F0} min";

    public TimeSpan Length
    {
        get => _length;
        set => SetField(ref _length, value);
    }
    public int ReservationID
    {
        get => _reservationId;
        set => SetField(ref _reservationId, value);
    }
    
}