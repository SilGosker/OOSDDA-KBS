using Kbs.Business.User;

namespace Kbs.Wpf.User.ViewUser.ViewUserIndex
{
    public class ReadUserIndexRoleViewModel
    {
        private readonly UserRole _role;
        public ReadUserIndexRoleViewModel()
        {
            HasValue = false;
        }
        public ReadUserIndexRoleViewModel(UserRole role)
        {
            _role = role;
        }
        public UserRole Role => _role;
        public bool HasValue { get; } = true; 
        public override string ToString()
        {
            if (HasValue)
            {
                return _role.ToDutchString();
            }
            return "Alle rollen";
        }
    }
}
