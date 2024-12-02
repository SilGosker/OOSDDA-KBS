using System.Windows.Media.Imaging;
using Kbs.Business.Damage;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Damage.Read.Details;

public class ReadDamageDetailsViewModel : ViewModel
{
    private string _description;
    private BitmapImage _image;
    private DamageStatus _status;
    private int _boatId;
    private string _boatName;
    private DateTime _date;

    public string Description
    {
        get => _description;
        set => SetField(ref _description, value);
    }

    public BitmapImage Image
    {
        get => _image;
        set => SetField(ref _image, value);
    }
    public DamageStatus Status
    {
        get => _status;
        set
        {
            SetField(ref _status, value);
            OnPropertyChanged(nameof(StatusFormatted));
        }
    }

    public int BoatId
    {
        get => _boatId;
        set => SetField(ref _boatId, value);
    }
    public string BoatName
    {
        get => _boatName;
        set => SetField(ref _boatName, value);
    }
    public DateTime Date
    {
        get => _date;
        set
        {
            SetField(ref _date, value);
            OnPropertyChanged(nameof(DateFormatted));
        }
    }

    public string DateFormatted => Date.ToString("yyyy-MM-dd");
    public string StatusFormatted => Status.ToDutchString();
}