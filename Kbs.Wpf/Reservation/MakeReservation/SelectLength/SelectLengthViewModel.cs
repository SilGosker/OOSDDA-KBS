using Kbs.Wpf.Components;
using System.Collections.ObjectModel;

namespace Kbs.Wpf.Reservation.MakeReservation.SelectLength;

public class SelectLengthViewModel : ViewModel
{
    private SelectLengthLengthOptionViewModel _selectedLength;
    private int _boatId;
    private DateTime _selectedStartTime;
    private string _lengthErrorMessage;

    public SelectLengthLengthOptionViewModel SelectedLength
    {
        get => _selectedLength;
        set => SetField(ref _selectedLength, value);
    }

    public ObservableCollection<SelectLengthLengthOptionViewModel> LengthOptions { get; } = new();

    public int BoatId
    {
        get => _boatId;
        set => SetField(ref _boatId, value);
    }

    public DateTime SelectedStartTime
    {
        get => _selectedStartTime;
        set => SetField(ref _selectedStartTime, value);
    }

    public string LengthErrorMessage
    {
        get => _lengthErrorMessage;
        set => SetField(ref _lengthErrorMessage, value);
    }
}