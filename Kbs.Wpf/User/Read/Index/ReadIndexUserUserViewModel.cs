using Kbs.Business.Helpers;
using Kbs.Business.User;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.User.Read.Index
{
    public class ReadIndexUserUserViewModel : ViewModel
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

        public ReadIndexUserUserViewModel(UserEntity user)
        {
            ThrowHelper.ThrowIfNull(user);
            if (string.IsNullOrEmpty(user.Name))
            {
                Name = user.Email;
            }
            else
            {
                Name = user.Name;
            }
            UserId = user.UserId;
            Role = user.Role.ToDutchString();
        }
    }
}

