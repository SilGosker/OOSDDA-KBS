using Kbs.Business.Session;
using Kbs.Data.User;
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
        private ViewUserValuesGeneralViewModel ViewModel => (ViewUserValuesGeneralViewModel)DataContext;

        public ViewUserValuesGeneralPage(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            InitializeComponent();
            //var users = _userRepository.GetByIdList(SessionManager.Instance.Current.User.UserId);
            /*
            foreach (var user in users)
            {
                {
                    ViewModel.Name = user.Name;
                    ViewModel.UserId = user.UserId;
                    //ViewModel.Role = (string)user.Role;
                }
            }
            */
        }
        public void ClickUser(object sender, RoutedEventArgs e)
        {

        }
    }
}
