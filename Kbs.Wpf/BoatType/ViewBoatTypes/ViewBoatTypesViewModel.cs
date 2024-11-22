using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.ViewBoatTypes
{
    public class ViewBoatTypesViewModel : ViewModel
    {
        
        // TODO: remove wanneer verwijderen verplaatst wordt.
        public int _boatTypeID;
        public int BoatTypeID
        {
            get => _boatTypeID;
            set => SetField(ref _boatTypeID, value);
        }         
        public ObservableCollection<ViewBoatTypeBoatTypeViewModel> Items { get; } = new();
    }
}
