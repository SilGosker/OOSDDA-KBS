using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Parcours.Read.Index;

public class ReadParcoursIndexViewModel : ViewModel
{
    public ObservableCollection<ReadIndexParcoursParcoursViewModel> Items { get; } = new();
}