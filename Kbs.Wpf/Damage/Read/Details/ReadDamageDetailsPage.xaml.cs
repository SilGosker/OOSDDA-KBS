using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.BoatType;
using Kbs.Business.Damage;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Data.Damage;
using Kbs.Wpf.Boat.Read.Details;
using Kbs.Wpf.Boat.Read.Index;
using Kbs.Wpf.BoatType.Read.Details;
using Kbs.Wpf.BoatType.Read.Index;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Damage.Read.Details;

public partial class ReadDamageDetailsPage : Page
{
    private readonly DamageRepository _damageRepository = new();
    private DamageEntity _damageEntity;
    private readonly BoatRepository _boatRepository = new();
    private readonly INavigationManager _navigationManager;
    private ReadDamageDetailsViewModel ViewModel => (ReadDamageDetailsViewModel)DataContext;

    public ReadDamageDetailsPage(int damageId, INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();

        var damage = _damageRepository.GetById(damageId);
        var boat = _boatRepository.GetById(damage.BoatId);

        ViewModel.DamageId = damageId;
        ViewModel.Description = damage.Description;
        ViewModel.Status = damage.Status;
        ViewModel.BoatId = damage.BoatId;
        ViewModel.Date = damage.Date;
        ViewModel.Image = damage.Image.ToImageSource();
        ViewModel.BoatName = boat.Name;
    }

    private void NavigateToBoatPage(object sender, RoutedEventArgs e)
    {
        _navigationManager.Navigate(() => new ReadDetailsBoatPage(_navigationManager, ViewModel.BoatId));
    }
    public void Refresh()
    {
        _navigationManager.Navigate(() => new ViewBoatTypesPage(_navigationManager));
    }

        
    
    private void RemoveDamage(object sender, RoutedEventArgs e)
    {
        DamageEntity entity = _damageRepository.GetById(ViewModel.BoatId);
        MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            _damageRepository.Delete(entity);
            _navigationManager.Navigate(() => new ReadIndexBoatPage(_navigationManager));
        }
    }
}