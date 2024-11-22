using Kbs.Business.Boat;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.CreateReservation.SelectTime;

public class SelectTimeBoatViewModel : ViewModel
{
    public SelectTimeBoatViewModel(BoatEntity boat)
    {
        BoatId = boat.BoatId;
        Name = boat.Name;
    }

    private string _name;
    private int _boatId;
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public int BoatId
    {
        get => _boatId;
        set => SetField(ref _boatId, value);
    }

    public override string ToString()
    {
        return Name;
    }
}