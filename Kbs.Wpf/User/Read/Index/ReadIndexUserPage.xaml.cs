using Kbs.Business.User;
using Kbs.Wpf.User.ReadUser.ReadUserDetail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using Kbs.Data.User;

namespace Kbs.Wpf.User.ReadUser.ReadUserGeneral
{
    public partial class ReadIndexUserPage : Page
    {
        private readonly UserRepository _userRepository;
        private readonly INavigationManager _navigationManager;
        private ReadIndexUserViewModel ViewModel => (ReadIndexUserViewModel)DataContext;

        public ReadIndexUserPage(INavigationManager navigationManager)
        {
            _userRepository = new UserRepository();
            _navigationManager = navigationManager;
            InitializeComponent();

            var roles = _userRepository.GetAllRoles();

               ViewModel.Roles.Add(new());
            foreach (var role in roles)
            {
                ViewModel.Roles.Add(new(role));
            }

            var userEntities = _userRepository.Get();
            foreach (var user in userEntities)
            {
                ViewModel.Items.Add(new ReadIndexUserValuesViewModel(user));
            }
        }

        public void ClickUser(object sender, RoutedEventArgs e)
        {
            var item = (ReadIndexUserValuesViewModel)((ListViewItem)sender).DataContext;
            _navigationManager.Navigate(() => new ReadDetailsUserPage(_navigationManager, item.UserId));
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
            var viewModel = ViewModel.SelectedRole;
            IEnumerable<UserEntity> filteredUsers;
            var selectedRole = viewModel?.Role;

            if (viewModel == null || !viewModel.HasValue || selectedRole is null)
            {
                filteredUsers = _userRepository.GetUsersByName(ViewModel.Name);
            }
            else
            {
                filteredUsers = _userRepository.GetUsersByNameAndRole(ViewModel.Name, (UserRole)selectedRole);
            }

            ViewModel.Items.Clear();
            foreach (var user in filteredUsers)
            {
                ViewModel.Items.Add(new ReadIndexUserValuesViewModel(user));
            }
        }
    }
}

