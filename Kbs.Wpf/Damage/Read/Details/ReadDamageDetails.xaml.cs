using System.IO;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Kbs.Data.Boat;
using Kbs.Data.Damage;

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
        damage.Image = File.ReadAllBytes("D:/funny.jpg");

        var boat = _boatRepository.GetById(damage.BoatId);
        ViewModel.Description = damage.Description;
        ViewModel.Status = damage.Status;
        ViewModel.BoatId = damage.BoatId;
        ViewModel.BoatName = boat.Name;
        ViewModel.Date = damage.DateReported;

        var image = new BitmapImage();
        using (var mem = new MemoryStream(damage.Image))
        {
            mem.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = null;
            image.StreamSource = mem;
            image.EndInit();
            image.Freeze();
            ViewModel.Image = image;
        }
    }
}