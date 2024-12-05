using Kbs.Business.User;

namespace Kbs.Wpf.User.ReadUser.ViewUserIndex
{
    public class ReadIndexUserRoleViewModel
    {
        private readonly UserRole _role;
        public ReadIndexUserRoleViewModel()
        {
            HasValue = false;
        }
        public ReadIndexUserRoleViewModel(UserRole role)
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
