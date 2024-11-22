using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kbs.Business.BoatType;
using System.Collections.ObjectModel;
using Kbs.Wpf.Components;
using Kbs.Wpf.Boat.Index;
using Kbs.Business.Helpers;


namespace Kbs.Wpf.BoatType.DetailedPage
{
    public class ViewBoatTypesViewModel : ViewModel
    {
        public string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }
        public int _boatTypeID;
        public int BoatTypeID
        {
            get => _boatTypeID;
            set => SetField(ref _boatTypeID, value);

        }
         
        //public ObservableCollection<BoatIndexBoatTypeViewModel> BoatTypes { get; } = new();
        public ObservableCollection<BoatIndexBoatTypeViewModel> Items { get; } = new();
    }
}
