using System.Collections.ObjectModel;
using Kbs.Business.BoatType;
using Kbs.Wpf.BoatType.Components;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.Read.Details
{
    public class ReadDetailsBoatTypeViewModel : ViewModel
    {
        private string _name;
        private int _boatTypeId;
        private int _speed;
        private BoatTypeRequiredExperience _experience;
        private BoatTypeSeats _seats;
        private BoatTypeExperienceViewModel _selectedExperience;
        private BoatTypeSeatsViewModel _selectedSeats;
        private bool _hasWheel;

        public ObservableCollection<ReadDetailsBoatTypeBoatViewModel> Items { get; } = new ObservableCollection<ReadDetailsBoatTypeBoatViewModel>();
        public ObservableCollection<BoatTypeExperienceViewModel> PossibleExperiences { get; } = new();
        public ObservableCollection<BoatTypeSeatsViewModel> PossibleSeats { get; } = new();

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
        public BoatTypeRequiredExperience Experience
        {
            get => _experience;
            set => SetField(ref _experience, value);
        }
        public BoatTypeSeats Seats
        {
            get => _seats; 
            set => SetField(ref _seats, value);
        }

        public BoatTypeExperienceViewModel SelectedBoatTypeExperience
        {
            get => _selectedExperience;
            set => SetField(ref _selectedExperience, value);
        }

        public BoatTypeSeatsViewModel SelectedBoatTypeSeats
        {
            get => _selectedSeats;
            set => SetField(ref _selectedSeats, value);
        }

        public bool HasWheel
        {
            get => _hasWheel;
            set => SetField(ref _hasWheel, value); 
        }
        public string HasWheelFormatted => HasWheel ? "Ja" : "Nee";
    }
}
