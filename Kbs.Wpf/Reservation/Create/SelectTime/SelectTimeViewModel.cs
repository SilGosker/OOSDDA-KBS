using System.Collections.ObjectModel;
using System.Windows;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.Create.SelectTime;

public class SelectTimeViewModel : ViewModel
{
    private Visibility _gameCommissionerComboBoxVisibility;
    private Visibility _memberComboBoxVisibility;
    private string _noBoatsSelectedError;
    private string _gameCreateMessage;
    public ObservableCollection<SelectTimeBoatViewModel> Boats { get; } = new();
    public ObservableCollection<string> DaysOfWeek { get; } = new();
    public ObservableCollection<DateTime> ThisWeek { get; } = new();

    public Visibility GameCommissionerComboBoxVisibility
    {
        get => _gameCommissionerComboBoxVisibility;
        set => SetField(ref _gameCommissionerComboBoxVisibility, value);
    }

    public Visibility MemberComboBoxVisibility
    {
        get => _memberComboBoxVisibility;
        set => SetField(ref _memberComboBoxVisibility, value);
    }

    public string NoBoatsSelectedError
    {
        get => _noBoatsSelectedError;
        set => SetField(ref _noBoatsSelectedError, value);
    }

    public string GameCreateMessage
    {
        get => _gameCreateMessage;
        set => SetField(ref _gameCreateMessage, value);
    }
}