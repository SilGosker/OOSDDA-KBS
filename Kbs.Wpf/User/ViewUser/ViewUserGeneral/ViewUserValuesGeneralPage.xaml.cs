using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.Reservation;
using Kbs.Data.User;
using Kbs.Wpf.Boat.Read.Details;
using Kbs.Wpf.Boat.Read.Index;
using Kbs.Wpf.BoatType.Read.Details;
using Kbs.Wpf.Components;
using Kbs.Wpf.Reservation.Read.Details;
using Kbs.Wpf.Reservation.Read.Index;
using Kbs.Wpf.User.ViewUser.ViewUserDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                //ViewModel.Roles.Add(role); Stel dit actief zetten dan methoden aanpassen van <UserRole> naar <string>
                ViewModel.Roles.Add(((UserRole)role).ToDutchString());
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
        private void TypeChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateItems();
        }


        private void UpdateItems()
        {
            var filteredUsers = _userRepository.GetUsersByNameAndRole(
                ViewModel.Name,
                ViewModel.Role
            );
            ViewModel.Items.Clear();
            foreach (var user in filteredUsers)
            {
                ViewModel.Items.Add(new ViewUserValuesValuesGeneralViewModel(user));
            }
        }
    }
}
