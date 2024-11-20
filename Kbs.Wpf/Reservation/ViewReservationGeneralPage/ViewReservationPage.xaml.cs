using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Data.Reservation;
using Kbs.Wpf.Reservation.ViewReservationGeneralPage;
using Kbs.Wpf.Reservation.ViewReservationSpecificPage;
using System.Windows.Controls;

namespace Kbs.Wpf.Reservation.ViewReservation
{
    public partial class ViewReservationPage : Page
    {
        private ViewReservationPageViewModel ViewModel => (ViewReservationPageViewModel)DataContext;
        private readonly ReservationRepository _reservationRepository = new ReservationRepository();
        private readonly INavigationManager _navigationManager;

        public ViewReservationPage(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            InitializeComponent();
            ReservationRepository repository = new ReservationRepository();
            var reservations = _reservationRepository.GetByUserId(SessionManager.Instance.Current.User.UserId);
            var sortedReservations = repository.SortByStatus(reservations);
            foreach (var reservation in sortedReservations)
            {
                {
                    ViewModel.Items.Add(new ViewReservationViewModel(ZieMeer)
                    {
                        ReservationID = reservation.ReservationID,
                        Length = reservation.Length,
                        StartTime = reservation.StartTime,
                        Status = reservation.Status,
                    });
                }
            }
        }

        private void ZieMeer(ViewReservationViewModel item)
        {
            _navigationManager.Navigate(() => new ViewPageSpecific(item.ReservationID));
        }

        private void ReservationClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var listViewItem = (ListViewItem)sender;
            var item = (ViewReservationViewModel)listViewItem.DataContext;
            ZieMeer(item);
        }
    }
}


