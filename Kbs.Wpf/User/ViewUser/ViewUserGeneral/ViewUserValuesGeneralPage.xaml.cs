using Kbs.Business.User;
using Kbs.Data.User;
using Kbs.Wpf.User.ViewUser.ViewUserDetail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Kbs.Wpf.User.ViewUser.ViewUserGeneral
{
    public partial class ViewUserValuesGeneralPage : Page
    {
        private readonly UserRepository _userRepository;
        private readonly INavigationManager _navigationManager;
        private ViewUserValuesValuesGeneralViewModel ViewUserValuesValuesGeneralViewModel => (ViewUserValuesValuesGeneralViewModel)DataContext;
        private ViewUserValuesGeneralViewModel ViewModel => (ViewUserValuesGeneralViewModel)DataContext;


        public ViewUserValuesGeneralPage(INavigationManager navigationManager)
        {
            _userRepository = new UserRepository();
            _navigationManager = navigationManager;
            InitializeComponent();

            var roles = _userRepository.GetAllRoles();
            ViewModel.Roles.Add("Alle rollen");
            foreach (var role in roles)
            {
                ViewModel.Roles.Add(role);
                //ViewModel.Roles.Add(((UserRole)role).ToDutchString());      Stel dit actief zetten dan methoden aanpassen van <UserRole> naar <string>
            }

            var userentity = _userRepository.Get();
            foreach (UserEntity user in userentity)
            {
                ViewModel.Items.Add(new ViewUserValuesValuesGeneralViewModel(user));
            }
        }

        public void ClickUser(object sender, RoutedEventArgs e)
        {
            var item = (ViewUserValuesValuesGeneralViewModel)((ListViewItem)sender).DataContext;
            _navigationManager.Navigate(() => new ViewUserValuesDetailedPage(_navigationManager, item.UserId));
        }
        private void NameChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UpdateItems();
            }
        }
      
        private void RoleChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateItems();
        }


        private void UpdateItems()
        {
            string selectedRole = ViewModel.Role;
            IEnumerable<UserEntity> filteredUsers;

            if (selectedRole == "Alle rollen")
            {
                filteredUsers = _userRepository.GetUsersByName(ViewModel.Name);
            }
            else
            {
                filteredUsers = _userRepository.GetUsersByNameAndRole(ViewModel.Name, selectedRole);
            }
            ViewModel.Items.Clear(); 

            foreach (var user in filteredUsers)
            {
                ViewModel.Items.Add(new ViewUserValuesValuesGeneralViewModel(user));
            }
        }

    }
}
