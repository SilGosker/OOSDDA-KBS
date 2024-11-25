using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Business.User;
using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.BoatType.ViewDetailedBoatTypes
{
    public class ViewDetailedBoatTypesPageViewModel : ViewModel
    {
        private string _name;
        private int _status;
        private int _boatTypeID;

        public ViewDetailedBoatTypesPageViewModel(BoatTypeEntity boattype, List<BoatEntity> boatStatus)
        {
            Name = boattype.Name;
            BoatTypeID = boattype.BoatTypeId;
            var matchingBoat = boatStatus.FirstOrDefault(boat => boat.BoatTypeId == boattype.BoatTypeId);
            if (matchingBoat != null)
            {
                Status = (int)matchingBoat.Status; 
            }
        }
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);  
        }

        public int BoatTypeID
        {
            get => _boatTypeID;
            set => SetField(ref _boatTypeID, value);
        }
        public int Status
        {
            get => _status;
            set => SetField(ref _status, value);
        }   
    }
}
