using Kbs.Business.Helpers;
using Kbs.Wpf.Components;
using Kbs.Wpf.User.ViewUser.ViewUserIndex;
using System.Collections.ObjectModel;

namespace Kbs.Wpf.User.ViewUser.ViewUserGeneral
{
    public class ReadUserIndexViewModel : ViewModel
    {
        public ObservableCollection<ReadUserValuesIndexViewModel> Items { get; } = new();
        public ObservableCollection<ReadUserIndexRoleViewModel> Roles { get; } = new();

        private string _name;
        private ReadUserIndexRoleViewModel _selectedRole;

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public ReadUserIndexRoleViewModel SelectedRole
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

