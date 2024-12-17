using System.Windows.Media;
using Kbs.Business.Boat;
using Kbs.Business.Extentions;
using Kbs.Business.Reservation;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.Read.Details;

public class ReadDetailsReservationViewModel : ViewModel
{
    private string _experience;
    private string _seats;
    private bool _hasSteeringWheel;
    private DateTime _startTime;
    private TimeSpan _length;
    private int _reservationId;
    private string _status = ((ReservationStatus)0).ToDutchString();
    private BoatEntity _boatEntity;
    private int _speed;

    public string Experience
    {
        get => _experience;
        set => SetField(ref _experience, value);
    }
    public string Seats
    {
        get => _seats;
        set => SetField(ref _seats, value);
    }
    public string Status
    {
        get => _status;
        set {
            SetField(ref _status, value);
            OnPropertyChanged(nameof(StatusColor));
        }
    }
    public Brush StatusColor
    {
        get
        {
            if (Status == ReservationStatus.Active.ToDutchString())
            {
                return Brushes.Green;
            }
            return Brushes.Red;
        }
    }
    public bool HasSteeringWheel
    {
        get => _hasSteeringWheel;
        set => SetField(ref _hasSteeringWheel, value);
    }
    public DateTime StartTime
    {
        get => _startTime;
        set
        {
            SetField(ref _startTime, value);
            OnPropertyChanged(nameof(StartTimeString));
        }
    }
    
    public TimeSpan Length
    {
        get => _length;
        set
        {
            SetField(ref _length, value);
            OnPropertyChanged(nameof(LengthString));
        }
    }
    
    public int ReservationId
    {
        get => _reservationId;
        set
        {
            SetField(ref _reservationId, value);
            OnPropertyChanged(nameof(ReservationIdString));
        }
    }
    
    public BoatEntity BoatEntity
    {
        get => _boatEntity;
        set => SetField(ref _boatEntity, value);
    }
    
    public int Speed
    {
        get => _speed;
        set => SetField(ref _speed, value);
    }
    
    public string ReservationIdString => $"Reservering #{ReservationId}";
    public string LengthString => Length.ToString(@"hh\:mm");
    public string StartTimeString => StartTime.ToDutchString(true);
    public string HasSteeringWheelString => HasSteeringWheel ? "Ja" : "Nee";
}

