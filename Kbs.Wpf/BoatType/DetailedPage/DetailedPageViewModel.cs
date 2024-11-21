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
    public class DetailedPageViewModel : ViewModel
    {
        public string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }
        public ObservableCollection<BoatIndexBoatTypeViewModel> BoatTypes { get; } = new();
        public ObservableCollection<BoatIndexBoatTypeViewModel> Items { get; } = new();

        /*
        public DetailedPageViewModel (BoatTypeEntity boatType)
        {
            ThrowHelper.ThrowIfNull(boatType);
            //Name = boatType;
        }
        public DetailedPageViewModel(char boattype) { }
        */


        /*
        public DetailedPageViewModel(BoatTypeEntity boatType)
        {
            ThrowHelper.ThrowIfNull(boatType);
            Name = boatType.Name;
        }
        */
        
    }
}
