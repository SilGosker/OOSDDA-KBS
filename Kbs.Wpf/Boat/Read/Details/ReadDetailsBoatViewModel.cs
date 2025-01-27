using System.Collections.ObjectModel;
using System.Windows.Media;
using Kbs.Business.Boat;
using Kbs.Business.Extentions;
using Kbs.Wpf.Boat.Components;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Read.Details;

public class ReadDetailsBoatViewModel : ViewModel
{
    private int _boatId;
    private string _name;
    private BoatStatus _status;
    private BoatStatusViewModel _selectedBoatStatus;
    private string _boatTypeName;
    private DateTime? _deleteRequestDate;
    private string _requestButtonText;
    private int _boatTypeId;
    private bool _deleteButtonEnabled;
    private string _nameError;
    private string _statusError;
    public ObservableCollection<ReadDetailsBoatReservationViewModel> Reservations { get; } = new();
    public ObservableCollection<BoatStatusViewModel> PossibleBoatStatuses { get; } = new();
    public ObservableCollection<ReadDetailsBoatDamageViewModel> Damages { get; } = new();
    public int BoatTypeId
    {
        get { return _boatTypeId; }
        set => SetField(ref _boatTypeId, value);
    }
    public int BoatId
    {
        get => _boatId;
        set
        {
            SetField(ref _boatId, value);
            OnPropertyChanged(nameof(BoatIdFormatted));
        }
    }
    public string BoatIdFormatted => $"Boot #{BoatId}";
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }
    public BoatStatus Status
    {
        get => _status;
        set => SetField(ref _status, value);
    }
    public BoatStatusViewModel SelectedBoatStatus
    {
        get => _selectedBoatStatus;
        set => SetField(ref _selectedBoatStatus, value);
    }
    
    public Brush StatusColor
    {
        get
        {
            if (Status == BoatStatus.Operational)
            {
                return Brushes.Green;
            }
            return Brushes.Red;
        }
    }
    public string BoatTypeName
    {
        get => _boatTypeName;
        set => SetField(ref _boatTypeName, value);
    }
    public string NameError
    {
        get => _nameError;
        set => SetField(ref _nameError, value);
    }
    public string StatusError
    {
        get => _statusError;
        set => SetField(ref _statusError, value);
    }

    public DateTime? DeleteRequestDate
    {
        get => _deleteRequestDate;
        set {
            SetField(ref _deleteRequestDate, value);
            OnPropertyChanged(nameof(DeleteRequestDateMessage));
            OnPropertyChanged(nameof(WaitDuration));
            OnPropertyChanged(nameof(WaitDurationMessage));
        }
    }
    public string RequestButtonText
    {
        get => _requestButtonText;
        set => SetField(ref _requestButtonText, value);
    }

    public bool DeleteButtonEnabled
    {
        get => _deleteButtonEnabled;
        set => SetField(ref _deleteButtonEnabled, value);
    }

    public string DeleteRequestDateMessage => _deleteRequestDate != null ?
        _deleteRequestDate.Value.ToDutchString(true) : "";
        
    public TimeSpan? WaitDuration
    {
        get
        {
            TimeSpan? duration = null;
            if (DeleteRequestDate != null)
            {
                duration = BoatValidator.RequestDeletionTime - (DateTime.Now - DeleteRequestDate);
                duration = duration > TimeSpan.Zero ? duration : TimeSpan.Zero;
            }
            return duration;
        }
    }

    public string WaitDurationMessage => WaitDuration != null ?
        $"{WaitDuration.Value.Days} dagen,\n{WaitDuration.Value.Hours} uren,\n{WaitDuration.Value.Minutes} minuten"
        : "";
}