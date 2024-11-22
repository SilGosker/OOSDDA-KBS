using System.Collections.ObjectModel;
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
    private string _waitMessage;
    public ObservableCollection<BoatDetailReservationViewModel> Reservations { get; } = new();
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
        set => SetField(ref _deleteRequestDate, value);
    }
    public string RequestButtonText
    {
        get => _requestButtonText;
        set => SetField(ref _requestButtonText, value);
    }
    public string WaitMessage
    {
        get => _waitMessage;
        set => SetField(ref _waitMessage, value);
    }
    public int GetTimeRemaining(int targetDuration)
    {
        return (int)(targetDuration - (DateTime.Now - _deleteRequestDate).Value.TotalSeconds); // Change TotalDays to TotalSeconds only for testing purposes
    }
}