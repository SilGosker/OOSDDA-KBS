using Kbs.Business.User;
using Kbs.Data.User;
using Kbs.Wpf.User.ViewUser.ViewUserGeneral;
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

namespace Kbs.Wpf.User.ViewUser.ViewUserDetail
{
    public partial class ViewUserValuesDetailedPage : Page
    {
        private readonly UserRepository _userRepository;
        private readonly INavigationManager _navigationManager;
        private ViewUserVa_uesDetailedViewModel ViewModel => (ViewUserVa_uesDetailedViewModel)DataContext;


        public ViewUserValuesDetailedPage(INavigationManager nav, int id)
        {
            _userRepository = new UserRepository();
            InitializeComponent();
            UserEntity userrep = _userRepository.GetById(id);
            if (userrep != null)
            {
                ViewModel.Email = userrep.Email;
                ViewModel.UserId = userrep.UserId;
                ViewModel.Role = userrep.Role.ToDutchString();
                ViewModel.Name = userrep.Name;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void Update(object sender, EventArgs e)
        {

        }
    }
}
