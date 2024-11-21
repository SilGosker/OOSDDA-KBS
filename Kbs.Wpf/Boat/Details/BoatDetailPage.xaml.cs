using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.Boat;
using Kbs.Business.Reservation;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.Reservation;
using Kbs.Data.User;
using Kbs.Wpf.Attributes;
using Kbs.Wpf.Boat.Index;
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
    private const int _daysForDeletion = 30;
    public BoatDetailPage(INavigationManager navigationManager, int boatId)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        var boat = _boatRepository.GetById(boatId);
        ViewModel.BoatId = boat.BoatID;
        ViewModel.Name = boat.Name;
        ViewModel.Status = boat.Status.ToDutchString();
        ViewModel.BoatTypeName = "Onbekend";
        ViewModel.DeleteRequestDate = boat.DeleteRequestDate;
        PermanentDeletionButton.Visibility = ((DateTime.Now - ViewModel.DeleteRequestDate).Value.TotalDays > _daysForDeletion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;

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

    private void RequestDeletion(object sender, RoutedEventArgs e)
    {
        DateTime? newDeleteRequestDateValue;
        string popupMessage = "";
        if (ViewModel.DeleteRequestDate == null)
        {
            newDeleteRequestDateValue = DateTime.Now;
            popupMessage = $"Verwijdering is aangevraagd.\nHet duurt {_daysForDeletion} voordat je deze boot permanent mag verwijderen";
        }
        else
        {
            newDeleteRequestDateValue = null;
            popupMessage = "Verwijdering aanvraag is ongedaan gemaakt.";
        }
        ViewModel.DeleteRequestDate = newDeleteRequestDateValue;
        MessageBox.Show(popupMessage);
        _boatRepository.UpdateDeleteRequestDate(ViewModel.BoatId, ViewModel.DeleteRequestDate);
        _navigationManager.Navigate(() => new BoatDetailPage(_navigationManager, ViewModel.BoatId));
    }

    private void PermanentDeletion(object sender, RoutedEventArgs e)
    {
        _boatRepository.DeleteById(ViewModel.BoatId);
        MessageBox.Show("Succesfully deleted");
        _navigationManager.Navigate(() => new BoatIndexPage(_navigationManager));
    }
}