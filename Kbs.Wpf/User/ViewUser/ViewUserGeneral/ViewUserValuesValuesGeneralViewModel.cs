using Kbs.Business.Helpers;
using Kbs.Business.Reservation;
using Kbs.Business.User;
using Kbs.Wpf.Components;
using Kbs.Wpf.Reservation.Read.Index;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.User.ViewUser.ViewUserGeneral
{
    public class ViewUserValuesValuesGeneralViewModel : ViewModel
    {

        private string _name;
        private int _userId;
        private string _role = ((UserRole)0).ToDutchString();

        public string Name
        {
            get { return _name; }
            set { SetField(ref _name, value); }
        }
        public int UserId
        {
            get { return _userId; }
            set { SetField(ref _userId, value); }
        }
        public string Role
        {
            get { return _role; }
            set { SetField(ref _role, value); }
        }

        public ViewUserValuesValuesGeneralViewModel(UserEntity user)
        {
            ThrowHelper.ThrowIfNull(user);
            Name = user.Name;
            UserId = user.UserId;
            //Role = user.Role.ToDutchString();
            Name = user.Name;
            Role = user.Role.ToDutchString() ?? Role;
        }
        public ViewUserValuesValuesGeneralViewModel()
        {

        }

    }
}
