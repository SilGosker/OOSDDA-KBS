using System.Windows.Input;
using Kbs.Business.Reservation;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.MakeReservation.SelectTime;

public class SelectTimePossibleTimeViewModel : ViewModel
{
    public SelectTimePossibleTimeViewModel()
    {
        FormattedTime = "Gereserveerd";
    }

    public SelectTimePossibleTimeViewModel(ReservationTime reservationTime, Action<SelectTimePossibleTimeViewModel> action) : this()
    {
        ReservationTime = reservationTime;
        CanBeReserved = reservationTime.CanBeReserved;
        Command = new RelayCommand<SelectTimePossibleTimeViewModel>(action);
        if (CanBeReserved)
        {
            FormattedTime = reservationTime.StartTime.ToString("HH:mm");
        }

    }

    private string _formattedTime;
    private ICommand _command;

    public string FormattedTime
    {
        get => _formattedTime;
        set => SetField(ref _formattedTime, value);
    }
    public ReservationTime ReservationTime { get; }
    public bool CanBeReserved { get; }

    public ICommand Command
    {
        get => _command;
        set => SetField(ref _command, value);
    }
}