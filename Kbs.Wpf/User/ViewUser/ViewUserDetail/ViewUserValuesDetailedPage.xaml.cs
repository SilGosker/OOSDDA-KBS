using Kbs.Business.Reservation;
using Kbs.Business.User;
using Kbs.Data.Reservation;
using Kbs.Data.User;
using Kbs.Wpf.BoatType.Read.Details;
using Kbs.Wpf.Reservation.Read.Details;
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
        private readonly ReservationRepository _reservationRepository;
       

        public ViewUserValuesDetailedPage(INavigationManager nav, int id)
        {
            _userRepository = new UserRepository();
            _reservationRepository = new ReservationRepository();
            InitializeComponent();
            UserEntity userrep = _userRepository.GetById(id);
            if (userrep != null)
            {
                ViewModel.Email = userrep.Email;
                ViewModel.UserId = userrep.UserId;
                ViewModel.Role = userrep.Role.ToDutchString();
                ViewModel.Name = userrep.Name;
            }
            
            var reservations = _reservationRepository.GetByUserId(id);
            foreach (var reservation in reservations)
            {
                ViewModel.Reservations.Add(new ViewUserValuesDetailedReservationViewModel(reservation));
            }
            
        }
        private void ReservationSelected(object sender, MouseButtonEventArgs e)
        {
            var row = (DataGridRow)sender;
            if (row == null)
            {
                return;
            }

            var dataContext = (ReadDetailsBoatTypeBoatViewModel)row.DataContext;
            _navigationManager.Navigate(() => new ReadDetailsReservationPage(dataContext.BoatId, _navigationManager));
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void Update(object sender, EventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
