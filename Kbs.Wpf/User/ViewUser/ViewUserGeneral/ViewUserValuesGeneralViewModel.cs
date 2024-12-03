using Kbs.Business.Helpers;
using Kbs.Business.User;
using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.User.ViewUser.ViewUserGeneral
{
    public class ViewUserValuesGeneralViewModel : ViewModel
    {
        private string _name;
        private int _userId;
        private UserEntity _role;
        public string Name {
            get { return _name; }
            set { SetField(ref _name, value);}
        }
        public int UserId {
            get { return _userId; }
            set { SetField(ref _userId, value); }
        }
        public UserEntity Role
        {
            get { return _role; }
            set { SetField(ref _role, value); }
        }

        
    }
}
