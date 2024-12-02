using System.Collections.ObjectModel;
using Kbs.Business.Boat;
using Kbs.Wpf.Boat.Components;
using Kbs.Wpf.Boat.Create;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Read.Details;

public class ReadDetailsBoatViewModel : ViewModel
{
    private int _boatId;
    private string _name;
    private BoatStatus _status;
    private BoatStatusesViewModel _selectedBoatStatus;
    private string _boatTypeName;
    private DateTime? _deleteRequestDate;
    private string _requestButtonText;
    private int _boatTypeId;
    private bool _deleteButtonEnabled;
    public ObservableCollection<ReadDetailsBoatReservationViewModel> Reservations { get; } = new();
    public ObservableCollection<BoatStatusesViewModel> PossibleBoatStatuses { get; } = new();
    public ObservableCollection<ReadDetailsBoatDamageViewModel> Damages { get; } = new();
    public int BoatTypeId
    {
        get { return _boatTypeId; }
        set => SetField(ref _boatTypeId, value);
    }
    public int BoatId
    {
        get => _boatId;
        set => SetField(ref _boatId, value);
    }
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
    public BoatStatusesViewModel SelectedBoatStatus
    {
        get => _selectedBoatStatus;
        set => SetField(ref _selectedBoatStatus, value);
    }
    public string BoatTypeName
    {
        get => _boatTypeName;
        set => SetField(ref _boatTypeName, value);
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
        _deleteRequestDate.Value.ToString("dd-MM-yyyy HH:mm")
        :"";
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