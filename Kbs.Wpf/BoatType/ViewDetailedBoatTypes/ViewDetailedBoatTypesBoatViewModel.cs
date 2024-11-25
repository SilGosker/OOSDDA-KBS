using Kbs.Business.BoatType;
using Kbs.Wpf.BoatType.ViewBoatTypes;
using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.BoatType.ViewDetailedBoatTypes
{
    public class ViewDetailedBoatTypesBoatViewModel : ViewModel
    {
        private string _name;
        private int _boatTypeId;
        private int _speed;
        private string _experience;
        private string _seats;
        private bool _hasWheel;
        private int _status;

        public ObservableCollection<ViewBoatTypeBoatViewModel> Items { get; } = new ObservableCollection<ViewBoatTypeBoatViewModel>();

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }
        public int BoatTypeId
        {
            get => _boatTypeId;
            set => SetField(ref _boatTypeId, value);    
        }
        public int Speed
        {
            get => _speed;
            set => SetField(ref _speed, value);
        }
        public string Experience
        {
            get => _experience;
            set => SetField(ref _experience, value);
        }
        public string Seats
        {
            get => _seats; 
            set => SetField(ref _seats, value);
        }
        public bool HasWheel
        {
            get => _hasWheel;
            set => SetField(ref _hasWheel, value); 
        }
        public string HasWheelFormatted => HasWheel ? "Ja" : "Nee";
    }
}
