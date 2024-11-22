using Kbs.Wpf.Components;
using System.Collections.ObjectModel;

namespace Kbs.Wpf.Reservation.MakeReservation.SelectTime;

public class SelectTimeDayOfWeek : ViewModel
{
    public ObservableCollection<SelectTimePossibleTimeViewModel> Times { get; } = new();
    public SelectTimeDayOfWeek(DateTime date)
    {
        Date = date;
    }

    private DateTime _date;
    public DateTime Date
    {
        get => _date;
        set
        {
            SetField(ref _date, value);
            OnPropertyChanged(nameof(DayOfWeek));
            OnPropertyChanged(nameof(FormattedDate));
        }
    }
    public string DayOfWeek => Date.DayOfWeek switch
    {
        System.DayOfWeek.Monday => "Maandag",
        System.DayOfWeek.Tuesday => "Dinsdag",
        System.DayOfWeek.Wednesday => "Woensdag",
        System.DayOfWeek.Thursday => "Donderdag",
        System.DayOfWeek.Friday => "Vrijdag",
        System.DayOfWeek.Saturday => "Zaterdag",
        System.DayOfWeek.Sunday => "Zondag",
        _ => "Onbekend"
    };

    public string FormattedDate => Date.ToString("dd-MM-yyyy");
}