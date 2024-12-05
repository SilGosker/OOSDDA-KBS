using Kbs.Business.Reservation;
using Kbs.Business.User;
using Kbs.Data.Reservation;
using Kbs.Data.User;
using Kbs.Wpf.BoatType.Read.Details;
using Kbs.Wpf.Reservation.Read.Details;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kbs.Wpf.User.ReadUser.ReadUserDetail
{
    public partial class ReadUserDetailedPage : Page
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly INavigationManager _navigationManager;
        private readonly ReservationRepository _reservationRepository = new ReservationRepository();

        private ReadUserDetailedViewModel ViewModel => (ReadUserDetailedViewModel)DataContext;

        public ReadUserDetailedPage(INavigationManager nav, int id)
        {
            InitializeComponent();

            var user = _userRepository.GetById(id);
            ViewModel.Name = user.Name;
            ViewModel.UserId = user.UserId;
            ViewModel.Email = user.Email;
            ViewModel.Role = user.Role.ToDutchString();

            var reservations = _reservationRepository.GetByUserId(id);
            foreach (var reservation in reservations)
            {
                ViewModel.Reservations.Add(new ReadUserDetailedReservationViewModel(reservation));
            }
        }

        private void ReservationSelected(object sender, MouseButtonEventArgs e)
        {
            var row = (DataGridRow)sender;
            if (row == null) return;

            var dataContext = (ReadDetailsBoatTypeBoatViewModel)row.DataContext;
            _navigationManager.Navigate(() => new ReadDetailsReservationPage(dataContext.BoatId, _navigationManager));
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedReservation = (ReadUserDetailedReservationViewModel)((DataGrid)sender).SelectedItem;
            if (selectedReservation != null)
            {
            }
        }

        private void DataGrid_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                if (e.Delta > 0)
                    dataGrid.ScrollIntoView(dataGrid.Items[0]);
                else
                    dataGrid.ScrollIntoView(dataGrid.Items[dataGrid.Items.Count - 1]);
            }
        }
    }
}
