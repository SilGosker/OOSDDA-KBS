using Kbs.Business.BoatType;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Data.Boat;
using Kbs.Wpf.Boat.Index;
using Kbs.Wpf.BoatType.ViewDetailedBoatTypes;
using Kbs.Wpf.Reservation.ViewReservationGeneralPage;

namespace Kbs.Wpf.Reservation.ViewReservationSpecificPage;

public partial class ViewPageSpecific : Page
{
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private readonly BoatRepository _boatRepository = new();
    private readonly ReservationRepository _reservationRepository = new();
    private readonly INavigationManager _navigationManager;
    private ViewPageSpecificViewModel ViewModel => (ViewPageSpecificViewModel)DataContext;

    public ViewPageSpecific(int reservationId, INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        var reservation = _reservationRepository.GetById(reservationId);
        var boatType = _boatTypeRepository.GetByReservationId(reservationId);

        ViewModel.ReservationId = reservation.ReservationId;
        ViewModel.Length = reservation.Length;
        ViewModel.StartTime = reservation.StartTime;
        ViewModel.HasSteeringWheel = boatType.HasSteeringWheel;
        ViewModel.Experience = boatType.RequiredExperience.ToDutchString();
        ViewModel.Seats = boatType.Seats.ToDutchString();
        ViewModel.Status = reservation.Status.ToDutchString();
        ViewModel.BoatEntity = _boatRepository.GetById(reservation.BoatId);
        ViewModel.Speed = boatType.Speed;

    }
    public void Delete(object sender, RoutedEventArgs e)
    {
        var entity = _reservationRepository.GetById(ViewModel.ReservationId);
        if (ViewModel.Status == ReservationStatus.Active.ToDutchString())
        {
            MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
            {
                _reservationRepository.Delete(entity);

                if (SessionManager.Instance.Current.User.IsMaterialCommissioner())
                {
                    _navigationManager.Navigate(() => new BoatIndexPage(_navigationManager));
                }
                else
                {
                    _navigationManager.Navigate(() => new ViewReservationPage(_navigationManager));
                }
            }
        } else
        {
            MessageBox.Show("Reservering kan niet verwijderd worden, omdat deze al is afgelopen.");
        }
    }
}