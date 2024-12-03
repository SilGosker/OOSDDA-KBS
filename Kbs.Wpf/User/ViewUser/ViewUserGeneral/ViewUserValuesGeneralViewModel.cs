using Kbs.Business.BoatType;
using Kbs.Business.Helpers;
using Kbs.Business.User;
using Kbs.Wpf.Boat.Create;
using Kbs.Wpf.Boat.Read.Index;
using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.User.ViewUser.ViewUserGeneral
{
    public class ViewUserValuesGeneralViewModel : ViewModel
    {
        public ObservableCollection<ViewUserValuesValuesGeneralViewModel> Items { get; } = new();
        public ObservableCollection<string> Roles { get; } = new();

        private string _name;
        private string _selectedRole;

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public string SelectedRole
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
