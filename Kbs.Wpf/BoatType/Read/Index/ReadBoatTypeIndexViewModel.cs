using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.Read.Index
{
    public class ReadBoatTypeIndexViewModel : ViewModel
    {      
        public ObservableCollection<ReadBoatTypeIndexBoatTypeViewModel> Items { get; } = new();
    }
}
