using Kbs.Business.Helpers;
using Kbs.Wpf.Components;
using Kbs.Wpf.User.ViewUser.ViewUserIndex;
using System.Collections.ObjectModel;

namespace Kbs.Wpf.User.ViewUser.ViewUserGeneral
{
    public class ViewUserValuesIndexViewModel : ViewModel
    {
        public ObservableCollection<ViewUserValuesValuesIndexViewModel> Items { get; } = new();
        public ObservableCollection<ViewuserValuesIndexRoleViewModel> Roles { get; } = new();

        private string _name;
        private ViewuserValuesIndexRoleViewModel _selectedRole;

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public ViewuserValuesIndexRoleViewModel SelectedRole
        {
            get => _selectedRole;
            set
            {
                if (SetField(ref _selectedRole, value))
                {
                    OnRoleChanged();
                }
            }
        }

        private void OnRoleChanged()
        {
        }
    }
}

