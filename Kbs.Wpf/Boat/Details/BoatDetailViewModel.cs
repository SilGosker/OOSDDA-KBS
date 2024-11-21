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
}