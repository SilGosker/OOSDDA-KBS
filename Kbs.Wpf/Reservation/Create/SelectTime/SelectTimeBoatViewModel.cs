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
        private BoatEntity _boat;
        private string _name;
        private bool _isSelected;
        private List<BoatEntity> _selectedBoats;
        public List<BoatEntity> SelectedBoats
        {
            get => _selectedBoats;
            set
            {
                _selectedBoats = value;
                OnPropertyChanged(nameof(SelectedBoats));
            }
        }
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public BoatEntity Boat
        {
            get => _boat;
            set => SetField(ref _boat, value);
        }
        
        public bool IsSelected
        {
            get => _isSelected;
            set => SetField(ref _isSelected, value);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
