using System.IO;
using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Boat;
using Kbs.Business.Damage;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.Damage;
using Kbs.Data.Reservation;
using Kbs.Wpf.Boat.Components;
using Kbs.Wpf.Boat.Read.Details;
using Kbs.Wpf.Components;
using Kbs.Wpf.Damage.Read.Details;
using Microsoft.Win32;

namespace Kbs.Wpf.Damage.Upload;

[HasRole(UserRole.MaterialCommissioner)]
[HighlightFor(typeof(DamageEntity))]
public partial class UploadDamagePage : Page
{
    private readonly DamageValidator _damageValidator = new();
    private readonly DamageRepository _damageRepository = new();
    private readonly BoatRepository _boatRepository = new();
    private readonly ReservationRepository _reservationRepository = new();
    private readonly INavigationManager _navigationManager;
    private UploadDamageViewModel ViewModel => (UploadDamageViewModel)DataContext;
    private readonly DatePickPopupWindow _changeStatusDialog = new("Verwachte einddatum onderhoud");
    private readonly ReadDetailsBoatViewModel _readDetailsBoatViewModel = new();
    
    public UploadDamagePage(int boatId, INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        ViewModel.BoatIsFine = true;
        ViewModel.BoatId = boatId;

    }

    private void SelectFileButtonClick(object sender, RoutedEventArgs e)
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

    private void Submit(object sender, RoutedEventArgs e)
    {
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

        ViewModel.FileErrorMessage = validationResult.TryGetValue(nameof(damage.Image), out string imageError) ? imageError : string.Empty;
        ViewModel.DescriptionErrorMessage = validationResult.TryGetValue(nameof(damage.Description), out string descriptionError) ? descriptionError : string.Empty;

        if (validationResult.Count != 0)
        {
            return;
        }

        if (!ViewModel.BoatIsFine)
        {
            _changeStatusDialog.ShowDialog();

            var boat = _boatRepository.GetById(ViewModel.BoatId);
            boat.Status = BoatStatus.Maintaining;
            
            if (_changeStatusDialog.ViewModel.IsCancelled)
            {
                // Reset the dialog
                _changeStatusDialog.ViewModel.IsCancelled = false;
                return;
            }

            MessageBoxResult result = MessageBox.Show("Alle reserveringen tot de gekozen datum worden geannuleerd. sWeet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            if (result == MessageBoxResult.Yes)
            {
                 _boatRepository.Update(boat);
                if (_readDetailsBoatViewModel.Status == BoatStatus.Maintaining)
                {
                    _reservationRepository.UpdateWhenMaintained(ViewModel.BoatId, _changeStatusDialog.ViewModel.EndDate);
                }
                else if (_readDetailsBoatViewModel.Status == BoatStatus.Broken)
                {
                    _reservationRepository.UpdateWhenBroken(ViewModel.BoatId);
                }
            }
        }

        _damageRepository.Create(damage);
        
        _navigationManager.Navigate(() => new ReadDamageDetailsPage(damage.DamageId, _navigationManager));
    }
}