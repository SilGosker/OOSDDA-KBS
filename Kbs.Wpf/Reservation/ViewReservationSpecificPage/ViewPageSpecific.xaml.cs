using Kbs.Business.BoatType;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Reservation;
using Kbs.Wpf.Reservation.ViewReservationGeneralPage;

namespace Kbs.Wpf.Reservation.ViewReservationSpecificPage;

public partial class ViewPageSpecific : Page
{
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private readonly ReservationRepository _reservationRepository = new();
    private readonly INavigationManager _navigationManager;
    private ViewPageSpecificViewModel ViewModel => (ViewPageSpecificViewModel)DataContext;

    public ViewPageSpecific(int reservationId, INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        var reservation = _reservationRepository.GetById(reservationId);
        var boatType = _boatTypeRepository.GetByReservationId(reservationId);


        ViewModel.ReservationID = reservation.ReservationId;
        ViewModel.Length = reservation.Length;
        ViewModel.StartTime = reservation.StartTime;
        ViewModel.HasSteeringWheel = boatType.HasSteeringWheel;
        ViewModel.Experience = boatType.RequiredExperience.ToDutchString();
        ViewModel.Seats = boatType.Seats.ToDutchString();
        ViewModel.Status = reservation.Status.ToDutchString();

    }
    public void Delete(object sender, RoutedEventArgs e)
    {
        var entity = _reservationRepository.GetById(ViewModel.ReservationID);
        if (ViewModel.Status != ReservationStatus.Active.ToDutchString())
        {
            MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
            {
                _reservationRepository.Delete(entity);
                _navigationManager.Navigate(() => new ViewReservationPage(_navigationManager));
            }
        } else
        {
            return;
        }
    }
}