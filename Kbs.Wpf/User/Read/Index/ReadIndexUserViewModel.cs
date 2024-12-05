using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.User.Read.Index
{
    public class ReadIndexUserViewModel : ViewModel
    {
        public ObservableCollection<ReadIndexUserUserViewModel> Items { get; } = new();
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

