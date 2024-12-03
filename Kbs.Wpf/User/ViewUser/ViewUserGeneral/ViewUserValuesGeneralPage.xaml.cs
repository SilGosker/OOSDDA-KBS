using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Data.User;
using Kbs.Wpf.Boat.Read.Index;
using Kbs.Wpf.Components;
using Kbs.Wpf.Reservation.Read.Details;
using Kbs.Wpf.Reservation.Read.Index;
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
    /// <summary>
    /// Interaction logic for ViewUserValuesGeneralPage.xaml
    /// </summary>
    public partial class ViewUserValuesGeneralPage : Page
    {
        private readonly UserRepository _userRepository;
        private readonly INavigationManager _navigationManager;
        private ViewUserValuesValuesGeneralViewModel ViewUserValuesValuesGeneralViewModel => (ViewUserValuesValuesGeneralViewModel)DataContext;
        private ViewUserValuesGeneralViewModel ViewUserValuesGeneralViewModel => (ViewUserValuesGeneralViewModel)DataContext;


        public ViewUserValuesGeneralPage(INavigationManager navigationManager)
        {
            _userRepository = new UserRepository();
            _navigationManager = navigationManager;
            InitializeComponent();
            var userrep = _userRepository.Get();
            foreach (UserEntity user in userrep)
            {
                {
                    ViewUserValuesGeneralViewModel.Items.Add(new ViewUserValuesValuesGeneralViewModel(user));
                }
            }
        }
        public void ClickUser(object sender, RoutedEventArgs e)
        {

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
        }
    }
}
