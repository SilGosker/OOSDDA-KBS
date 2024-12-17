using Kbs.Business.BoatType;
using Kbs.Wpf.BoatType.Components;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.Read.Index
{
    public class ReadBoatTypeIndexBoatTypeViewModel : ViewModel
    {
        private string _name;
        private int _id;
        private BoatTypeExperienceViewModel _experience;
        private BoatTypeSeatsViewModel _seats;
        public ReadBoatTypeIndexBoatTypeViewModel(BoatTypeEntity boatType)
        {
            this.Name = boatType.Name;
            this.Id = boatType.BoatTypeId;
            this.Experience = new BoatTypeExperienceViewModel(boatType.RequiredExperience);
            this.Seats = new BoatTypeSeatsViewModel(boatType.Seats);
        }

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }
        public BoatTypeExperienceViewModel Experience
        {
            get => _experience;
            set => SetField(ref _experience, value);
        }
        public BoatTypeSeatsViewModel Seats
        {
            get => _seats;
            set => SetField(ref _seats, value);
        }

    }
}
