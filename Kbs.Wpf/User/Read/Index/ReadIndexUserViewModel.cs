using Kbs.Business.Helpers;
using Kbs.Wpf.Components;
using Kbs.Wpf.User.ReadUser.ViewUserIndex;
using System.Collections.ObjectModel;

namespace Kbs.Wpf.User.ReadUser.ReadUserGeneral
{
    public class ReadIndexUserViewModel : ViewModel
    {
        public ObservableCollection<ReadIndexUserValuesViewModel> Items { get; } = new();
        public ObservableCollection<ReadIndexUserRoleViewModel> Roles { get; } = new();

        private string _name;
        private ReadIndexUserRoleViewModel _selectedRole;

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public ReadIndexUserRoleViewModel SelectedRole
        {
            get => _selectedRole;
            set => SetField(ref _selectedRole, value);
        }
    }
}

