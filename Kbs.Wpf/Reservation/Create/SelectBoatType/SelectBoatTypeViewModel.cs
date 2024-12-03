using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.Create.SelectBoatType;

public class SelectBoatTypeViewModel : ViewModel
{
    public ObservableCollection<SelectBoatTypeBoatTypeViewModel> Items { get; } = new();
    public ObservableCollection<int> Amount { get; } = new();
    private string _comboboxVisability;
    public string ComboboxVisability
    {
        get => _comboboxVisability;
        set => SetField(ref _comboboxVisability, value);
    }
    
}