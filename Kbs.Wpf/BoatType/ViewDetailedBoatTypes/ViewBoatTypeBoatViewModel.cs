using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.ViewDetailedBoatTypes
{
    public class ViewBoatTypeBoatViewModel : ViewModel
    {
        private string _name;
        private string _status;
        private int _boatId;

        public ViewBoatTypeBoatViewModel(BoatEntity boat)
        {
            Name = boat.Name;
            BoatId = boat.BoatId;
            Status = boat.Status.ToDutchString(); 
        }
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);  
        }

        public int BoatId
        {
            get => _boatId;
            set => SetField(ref _boatId, value);
        }

        public string Status
        {
            get => _status;
            set => SetField(ref _status, value);
        }   
    }
}
