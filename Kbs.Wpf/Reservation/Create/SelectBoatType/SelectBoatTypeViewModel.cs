using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.Create.SelectBoatType;

public class SelectBoatTypeViewModel : ViewModel
{
    private string _gameCreateMessage;
    public ObservableCollection<SelectBoatTypeBoatTypeViewModel> Items { get; } = new();
    public string GameCreateMessage
    {
        get => _gameCreateMessage;
        set => SetField(ref _gameCreateMessage, value);
    }
}