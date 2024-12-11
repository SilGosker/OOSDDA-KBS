using Kbs.Wpf.Components;

namespace Kbs.Wpf.Game.Read.Details;

public class ReadDetailsGameBoatViewModel : ViewModel
{
    private string _name;
    private int _reservationId;
    
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public int ReservationId
    {
        get => _reservationId;
        set => SetField(ref _reservationId, value);
    }
}