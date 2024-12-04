using Kbs.Business.User;

namespace Kbs.Wpf.User.ViewUser.ViewUserIndex
{
    public class ViewuserValuesIndexRoleViewModel
    {
        private readonly UserRole _role;
        public ViewuserValuesIndexRoleViewModel()
        {
            HasValue = false;
        }
        public ViewuserValuesIndexRoleViewModel(UserRole role)
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
