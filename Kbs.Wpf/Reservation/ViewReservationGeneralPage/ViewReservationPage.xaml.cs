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

            var reservations = _reservationRepository.GetByUserId(SessionManager.Instance.Current.User.UserId);
            foreach (var reservation in reservations)
            {
                ViewModel.Items.Add(new ViewReservationViewModel(ZieMeer)
                {
                    ReservationID = reservation.ReservationID,
                    Tijdsduur = reservation.Length,
                    StartTime = reservation.StartTime
                });
            }
        }

        private void ZieMeer(ViewReservationViewModel item)
        {
            _navigationManager.Navigate(() => new ViewPageSpecific(item.ReservationID));
        }
    }
}


