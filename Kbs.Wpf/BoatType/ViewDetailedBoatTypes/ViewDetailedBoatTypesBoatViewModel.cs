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
        private string _boatName;
        private int _boatTypeId;
        private int _speed;
        private int _experience;
        private int _seats;
        private bool _hasWheel;
        private string _boatTypeName;
        private int _status;
        
        public string BoatName
        {
            get => _boatName;
            set => SetField(ref _boatName, value);
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
        public int Experience
        {
            get => _experience;
            set => SetField(ref _experience, value);
        }
        public int Seats
        {
            get => _seats; 
            set => SetField(ref _seats, value);
        }
        public bool HasWheel
        {
            get => _hasWheel;
            set => SetField(ref _hasWheel, value);
        }
        public string BoatTypeName
        {
            get => _boatTypeName; 
            set => SetField(ref _boatTypeName, value);
        }
        public int Status
        {
            get => _status; 
            set => SetField(ref _status, value);
        }
    }
}
