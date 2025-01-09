using Kbs.Business.Boat;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Medal.Create
{
    public class CreateMedalBoatViewModel : ViewModel
    {
        private int _boatId;
        private string _name;

        public CreateMedalBoatViewModel(BoatEntity boat)
        {
            BoatId = boat.BoatId;
            Name = boat.Name;
        }
        public int BoatId
        {
            get => _boatId;
            set => SetField(ref _boatId, value);
        }

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
