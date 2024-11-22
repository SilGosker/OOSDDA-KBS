using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf;


public class MainViewModel : ViewModel
{
    public ObservableCollection<NavigationItemViewModel> NavigationItems { get; set; } = new();
}