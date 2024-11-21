using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.Boat;
using Kbs.Business.Reservation;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.Reservation;
using Kbs.Data.User;
using Kbs.Wpf.Attributes;
using Kbs.Wpf.Reservation.ViewReservationSpecificPage;

namespace Kbs.Wpf.Boat.Details;

[HasRole(Role.MaterialCommissioner)]
public partial class BoatDetailPage : Page
{
    private readonly IBoatRepository _boatRepository = new BoatRepository();
    private readonly IUserRepository _userRepository = new UserRepository();
    private readonly IReservationRepository _registrationRepository = new ReservationRepository();
    public BoatDetailViewModel ViewModel => (BoatDetailViewModel)DataContext;
    private readonly INavigationManager _navigationManager;
    public BoatDetailPage(INavigationManager navigationManager, int boatId)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        var boat = _boatRepository.GetById(boatId);
        ViewModel.BoatId = boat.BoatID;
        ViewModel.Name = boat.Name;
        ViewModel.Status = boat.Status.ToDutchString();
        ViewModel.BoatTypeName = "Onbekend";

        foreach (var reservation in _registrationRepository.GetByBoatId(boat.BoatID))
        {
            var user = _userRepository.GetById(reservation.UserId);
            ViewModel.Reservations.Add(new BoatDetailReservationViewModel(reservation, user));
        }
    }

    private void ReservationSelected(object sender, MouseButtonEventArgs e)
    {
        var row = (DataGridRow)sender;
        if (row == null)
        {
            return;
        }

        var dataContext = (BoatDetailReservationViewModel)row.DataContext;

        _navigationManager.Navigate(() => new ViewPageSpecific(dataContext.ReservationId));
    }
}