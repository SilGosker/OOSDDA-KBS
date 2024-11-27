using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.ViewBoatTypes
{
    public class ViewBoatTypesViewModel : ViewModel
    {      
        public ObservableCollection<ViewBoatTypeBoatTypeViewModel> Items { get; } = new();
    }
}
