using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.Boat;
using Kbs.Business.Reservation;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using Kbs.Data.User;
using Kbs.Wpf.Attributes;
using Kbs.Wpf.Boat.Index;
using Kbs.Wpf.Components;
using Kbs.Wpf.Reservation.ViewReservationSpecificPage;

namespace Kbs.Wpf.Boat.Details;

[HasRole(Role.MaterialCommissioner)]
public partial class BoatDetailPage : Page
{
    private readonly BoatRepository _boatRepository = new();
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private readonly UserRepository _userRepository = new();
    private readonly ReservationRepository _registrationRepository = new();
    private readonly INavigationManager _navigationManager;
    private readonly BoatValidator _boatValidator;
    private BoatDetailViewModel ViewModel => (BoatDetailViewModel)DataContext;
    public BoatDetailPage(INavigationManager navigationManager, int boatId)
    {
        _navigationManager = navigationManager;
        _boatValidator = new(this._boatTypeRepository);
        InitializeComponent();
        var boat = _boatRepository.GetById(boatId);
        ViewModel.BoatTypeId = boat.BoatTypeId;
        ViewModel.BoatId = boat.BoatId;
        ViewModel.Name = boat.Name;
        ViewModel.Status = boat.Status.ToDutchString();
        ViewModel.BoatTypeName = _boatTypeRepository.GetById(boat.BoatTypeId).Name;
        ViewModel.DeleteRequestDate = boat.DeleteRequestDate;
        ViewModel.RequestButtonText = ViewModel.DeleteRequestDate == null
            ? "Ter verwijdering opstellen"
            : "Annuleer verwijdering aanvraag";

        var result = _boatValidator.IsValidForPermanentDeletion(boat);
        ViewModel.EnableDeletion = result.Count == 0;

        foreach (var reservation in _registrationRepository.GetByBoatId(boat.BoatId))
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

        _navigationManager.Navigate(() => new ViewPageSpecific(dataContext.ReservationId, _navigationManager));
    }

    private void RequestDeletion(object sender, RoutedEventArgs e)
    {
        DateTime? newDeleteRequestDateValue;
        string popupMessage = "";
        if (ViewModel.DeleteRequestDate == null)
        {
            newDeleteRequestDateValue = DateTime.Now;
            popupMessage = $"Verwijdering is aangevraagd.";
        }
        else
        {
            newDeleteRequestDateValue = null;
            popupMessage = "Verwijdering aanvraag is ongedaan gemaakt.";
        }
        ViewModel.DeleteRequestDate = newDeleteRequestDateValue;
        MessageBox.Show(popupMessage);
        ViewModel.Status = BoatStatus.Maintaining.ToDutchString();
        BoatEntity newBoatValues = new BoatEntity() { BoatId = ViewModel.BoatId, BoatTypeId = ViewModel.BoatTypeId, Name = ViewModel.Name, DeleteRequestDate = ViewModel.DeleteRequestDate, Status = BoatStatus.Maintaining };
        _boatRepository.Update(newBoatValues);

        //Refresh the whole page so every UI element gets updated where needed
        _navigationManager.Navigate(() => new BoatDetailPage(_navigationManager, ViewModel.BoatId));
    }

    private void PermanentDeletion(object sender, RoutedEventArgs e)
    {
        string popupMessage = "";
        if (ViewModel.DeleteRequestDate != null)
        {
            BoatEntity boat = new BoatEntity()
            {
                BoatId = ViewModel.BoatId,
                BoatTypeId = ViewModel.BoatTypeId,
                DeleteRequestDate = ViewModel.DeleteRequestDate,
                Name = ViewModel.Name
            };
            var result = _boatValidator.IsValidForPermanentDeletion(boat);

            if (result.Count == 0)
            {
                _boatRepository.DeleteById(ViewModel.BoatId);
                popupMessage = "Succesvol verwijderd";
                _navigationManager.Navigate(() => new BoatIndexPage(_navigationManager));
            }
            else
            {
                result.TryGetValue(nameof(boat.DeleteRequestDate), out string message);
                popupMessage = message;
                ViewModel.DeleteRequestDate = ViewModel.DeleteRequestDate;
            }
        }
        else
        {
            popupMessage = "De wacht periode is nog niet gestart.";
        }

        MessageBox.Show(popupMessage);
    }
}