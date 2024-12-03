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
            Id = user.UserId;
        }

        private string _name;
        private int _id;

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
