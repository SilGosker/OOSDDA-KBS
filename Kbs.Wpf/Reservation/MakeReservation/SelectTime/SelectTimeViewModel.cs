using Kbs.Wpf.Components;
using System.Collections.ObjectModel;

namespace Kbs.Wpf.Reservation.MakeReservation.SelectTime;

public class SelectTimeViewModel : ViewModel
{
    public ObservableCollection<SelectTimeBoatViewModel> Boats { get; } = new();
    public ObservableCollection<SelectTimeDayOfWeek> CurrentWeek { get; } = new();
    public ObservableCollection<SelectTimePossibleTimeViewModel> PossibleTimes { get; } = new();

    private int _selectedBoatId;
    private DateTime _selectedDate = DateTime.Now;
    public int SelectedBoatId
    {
        get => _selectedBoatId;
        set => SetField(ref _selectedBoatId, value);
    }

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set => SetField(ref _selectedDate, value);
    }
}