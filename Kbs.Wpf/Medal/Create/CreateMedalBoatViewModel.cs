using Kbs.Business.Boat;
using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.Medal.Create
{
    public class CreateMedalBoatViewModel: ViewModel
    {
        private int _boatId;
        private string _name;
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

        public CreateMedalBoatViewModel(BoatEntity boat)
        {
            BoatId = boat.BoatId;
            Name = boat.Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
