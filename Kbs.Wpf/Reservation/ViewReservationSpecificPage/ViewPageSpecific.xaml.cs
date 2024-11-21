using Kbs.Business.BoatType;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Reservation;

namespace Kbs.Wpf.Reservation.ViewReservationSpecificPage
{
    public partial class ViewPageSpecific : Page
    {
        private readonly BoatTypeRepository _boatTypeRepository = new();
        private readonly ReservationRepository _reservationRepository = new();
        private ViewPageSpecificViewModel ViewModel => (ViewPageSpecificViewModel)DataContext;

        public ViewPageSpecific(int reservationId)
        {
            InitializeComponent();
            var reservation = _reservationRepository.GetById(reservationId);
            var boatType = _boatTypeRepository.GetByReservationId(reservationId);


            ViewModel.ReservationID = reservation.ReservationID;
            ViewModel.Length = reservation.Length;
            ViewModel.StartTime = reservation.StartTime;
            ViewModel.HasSteeringWheel = boatType.HasSteeringWheel;
            ViewModel.Niveau = (int)boatType.RequiredExperience;
            ViewModel.Seats = boatType.Seats.ToDutchString();
            ViewModel.Status = reservation.Status.ToDutchString();

        }

        public void Annuleren(object sender, RoutedEventArgs e)
        {
            var entity = _reservationRepository.GetById(ViewModel.ReservationID);

            MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result) {
                _reservationRepository.Delete(entity);
            }

        }
    }
}
