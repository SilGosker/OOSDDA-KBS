using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.Read.Details
{
    public class ReadDetailsBoatTypeViewModel : ViewModel
    {
        private string _name;
        private int _boatTypeId;
        private int _speed;
        private string _experience;
        private string _seats;
        private bool _hasWheel;

        public ObservableCollection<ReadDetailsBoatTypeBoatViewModel> Items { get; } = new ObservableCollection<ReadDetailsBoatTypeBoatViewModel>();

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
        public string BoatTypeNameString => $"Boottype: {Name}";
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
        public string HasWheelString => HasWheel ? "Ja" : "Nee";
        
    }
}
