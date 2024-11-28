using Kbs.Business.Boat;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.Create.SelectTime
{
    public class SelectTimeBoatViewModel : ViewModel
    {

        public SelectTimeBoatViewModel(BoatEntity boat)
        {
            Boat = boat;
            Name = boat.Name;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        private BoatEntity _boat;



        public BoatEntity Boat
        {
            get => _boat;
            set => SetField(ref _boat, value);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
