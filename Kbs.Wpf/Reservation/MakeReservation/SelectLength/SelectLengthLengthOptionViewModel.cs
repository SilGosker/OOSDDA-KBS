using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.MakeReservation.SelectLength;

public class SelectLengthLengthOptionViewModel : ViewModel
{
    private bool _selected;
    private TimeSpan _length;
    public SelectLengthLengthOptionViewModel(TimeSpan time)
    {
        Selected = false;
        Length = time;
    }
    public bool Selected
    {
        get => _selected;
        set => SetField(ref _selected, value);
    }

    public string FormattedLength => Length.ToString("hh':'mm");

    public TimeSpan Length
    {
        get => _length;
        set
        {
            SetField(ref _length, value);
            OnPropertyChanged(nameof(FormattedLength));
        }
    }
}