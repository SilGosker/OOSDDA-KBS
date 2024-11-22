using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.CreateReservation.SelectTime;

public class SelectTimeViewModel : ViewModel
{
    public ObservableCollection<SelectTimeBoatViewModel> Boats { get; } = new();
    public ObservableCollection<SelectTimeDayOfWeek> CurrentWeek { get; } = new();
    public ObservableCollection<SelectTimePossibleTimeViewModel> PossibleTimes { get; } = new();

    private SelectTimeBoatViewModel _selectedBoat;
    private DateTime _selectedDate = DateTime.Now;
    public SelectTimeBoatViewModel SelectedBoat
    {
        get => _selectedBoat;
        set => SetField(ref _selectedBoat, value);
    }

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set => SetField(ref _selectedDate, value);
    }
}