using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Kbs.Business.Boat;
using Kbs.Business.Damage;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.Damage;
using Kbs.Data.Reservation;
using Kbs.Wpf.Boat.Read.Define;
using Kbs.Wpf.Boat.Read.Details;
using Kbs.Wpf.Damage.Read.Details;
using Microsoft.Win32;

namespace Kbs.Wpf.Damage.Upload;

[HasRole(UserRole.MaterialCommissioner)]
public partial class UploadDamagePage : Page
{
    private readonly DamageValidator _damageValidator = new();
    private readonly DamageRepository _damageRepository = new();
    private readonly BoatRepository _boatRepository = new();
    private readonly ReservationRepository _reservationRepository = new();
    private readonly INavigationManager _navigationManager;
    private UploadDamageViewModel ViewModel => (UploadDamageViewModel)DataContext;
    private readonly ChangeStatusMaintainingWindow _changeStatusDialog = new();


    public UploadDamagePage(int boatId, INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        ViewModel.BoatIsFine = true;
        ViewModel.BoatId = boatId;
    }

    private void SelectFileButtonClick(object sender, System.Windows.RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png",
            Title = "Selecteer een foto"
        };

        if (dialog.ShowDialog() == true)
        {

            ViewModel.ImagePath = dialog.FileName;
        }
    }

    private void Submit(object sender, System.Windows.RoutedEventArgs e)
    {
        if (!ViewModel.BoatIsFine)
        {
            _changeStatusDialog.ShowDialog();
        }

        if (_changeStatusDialog.ViewModel.IsCancelled)
        {
            return;
        }

        _reservationRepository.DeleteWhenMaintained(ViewModel.BoatId, _changeStatusDialog.ViewModel.EndDate);
        if (!ViewModel.BoatIsFine)
        {
            MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.No == result)
            {
                return;
            }
            if (MessageBoxResult.Yes == result)
            {
                _reservationRepository.DeleteWhenBroken(ViewModel.BoatId);
            }
        }
        var damage = new DamageEntity()
        {
            BoatId = ViewModel.BoatId,
            Date = DateTime.Now,
            Status = DamageStatus.UnSolved,
            Description = ViewModel.Description,
        };

        try
        {
            damage.Image = File.ReadAllBytes(ViewModel.ImagePath);
        }
        catch
        {
            // ignored, validation will handle it
        }

        var validationResult = _damageValidator.ValidateForUpload(damage);

        if (validationResult.TryGetValue(nameof(damage.Image), out string imageError))
        {
            ViewModel.FileErrorMessage = imageError;
        }
        else
        {
            ViewModel.FileErrorMessage = string.Empty;
        }

        if (validationResult.TryGetValue(nameof(damage.Description), out string descriptionError))
        {
            ViewModel.DescriptionErrorMessage = descriptionError;
        }
        else
        {
            ViewModel.DescriptionErrorMessage = string.Empty;
        }

        if (validationResult.Count != 0)
        {
            return;
        }

        _damageRepository.Create(damage);

        if (!ViewModel.BoatIsFine)
        {
            var boat = _boatRepository.GetById(ViewModel.BoatId);
            boat.Status = BoatStatus.Maintaining;
            _boatRepository.Update(boat);
        }

        _navigationManager.Navigate(() => new ReadDamageDetailsPage(damage.DamageId, _navigationManager));
    }
}