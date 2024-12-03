using Kbs.Business.Helpers;
using Kbs.Business.User;
using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.User.ViewUser.ViewUserDetail
{
    public class ViewUserVa_uesDetailedViewModel : ViewModel
    {
        private string _name;
        private string _role;
        private string _email;
        private int _userId;
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
        public string Email
        {
            get => _email;
            set => SetField(ref _email, value);
        }
        public int UserId
        {
            get => _userId;
            set => SetField(ref _userId, value);
        }
        public ViewUserVa_uesDetailedViewModel(UserEntity user)
        {
            ThrowHelper.ThrowIfNull(user);
            Name = user.Name;
            UserId = user.UserId;
            Role = user.Role.ToDutchString();
            Name = user.Name;
            Role = user.Role.ToDutchString() ?? Role;
        }
    }
}
