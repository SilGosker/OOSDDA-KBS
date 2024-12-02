using System.IO;
using System.Windows.Controls;
using Kbs.Data.Boat;
using Kbs.Data.Damage;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Damage.Read.Details;

public partial class ReadDamageDetails : Page
{
    private readonly DamageRepository _damageRepository = new();
    private readonly BoatRepository _boatRepository = new();
    private readonly INavigationManager _navigationManager;
    private ReadDamageDetailsViewModel ViewModel => (ReadDamageDetailsViewModel)DataContext;

    public ReadDamageDetails(int damageId, INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();

        var damage = _damageRepository.GetById(damageId);
        var boat = _boatRepository.GetById(damage.BoatId);

        ViewModel.Description = damage.Description;
        ViewModel.Status = damage.Status;
        ViewModel.BoatId = damage.BoatId;
        ViewModel.BoatName = boat.Name;
        ViewModel.Date = damage.Date;
        ViewModel.Image = damage.Image.ToImageSource();
    }
}