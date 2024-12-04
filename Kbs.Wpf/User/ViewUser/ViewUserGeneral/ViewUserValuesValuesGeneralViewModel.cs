using Kbs.Business.Helpers;
using Kbs.Business.User;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.User.ViewUser.ViewUserGeneral
{
    public class ViewUserValuesValuesGeneralViewModel : ViewModel
    {
        private string _name;
        private int _userId;
        private string _role;

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public int UserId
        {
            get => _userId;
            set => SetField(ref _userId, value);
        }

        public string Role
        {
            get => _role;
            set => SetField(ref _role, value);
        }

        public ViewUserValuesValuesGeneralViewModel(UserEntity user)
        {
            ThrowHelper.ThrowIfNull(user);
            Name = user.Name;
            UserId = user.UserId;
            Role = user.Role.ToDutchString();
        }
    }
}

