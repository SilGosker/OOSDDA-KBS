using Kbs.Business.Helpers;
using Kbs.Business.User;
using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.User.ViewUser.ViewUserGeneral
{
    public class ViewUserDropdownViewModel : ViewModel
    {
        public ViewUserDropdownViewModel(UserEntity user) 
        {
            ThrowHelper.ThrowIfNull(user);
            Name = user.Name;
            Role = user.Role.ToDutchString();
        }

        private string _name;
        private string _role;
        //private string _role = ((UserRole)0).ToDutchString();


        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public string Role
        {
            get => _role;
            set => SetField(ref _role, value);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
