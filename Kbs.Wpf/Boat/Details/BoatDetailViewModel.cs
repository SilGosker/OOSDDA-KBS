using System.Collections.ObjectModel;
using Kbs.Business.Boat;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Details;

public class BoatDetailViewModel : ViewModel
{
    private int _boatId;
    private string _name;
    private string _status;
    private string _boatTypeName;
    private DateTime? _deleteRequestDate;
    private string _requestButtonText;
    private int _boatTypeId;
    private bool _enableDeletion;
    public ObservableCollection<BoatDetailReservationViewModel> Reservations { get; } = new();
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
    public string Status
    {
        get => _status;
        set => SetField(ref _status, value);
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

    public bool EnableDeletion
    {
        get => _enableDeletion;
        set => SetField(ref _enableDeletion, value);
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