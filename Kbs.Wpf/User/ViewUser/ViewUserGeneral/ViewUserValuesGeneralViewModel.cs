using Kbs.Wpf.Components;
using System.Collections.ObjectModel;


namespace Kbs.Wpf.User.ViewUser.ViewUserGeneral
{
    public class ViewUserValuesGeneralViewModel : ViewModel
    {
        public ObservableCollection<ViewUserValuesValuesGeneralViewModel> Items { get; } = new();
        public ObservableCollection<string> Roles { get; } = new ObservableCollection<string>();

        private string _name;
        private string _role;


        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public string Role
        {
            get => _role;
            set
            {
                if (SetField(ref _role, value))
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
