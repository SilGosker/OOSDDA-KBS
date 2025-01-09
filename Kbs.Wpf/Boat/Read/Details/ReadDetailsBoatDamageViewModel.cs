using System.Windows.Media;
using Kbs.Business.Damage;
using Kbs.Business.Extentions;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Read.Details;

public class ReadDetailsBoatDamageViewModel : ViewModel
{
    private ImageSource _image;
    private string _date;
    private int _damageId;
    public ReadDetailsBoatDamageViewModel(DamageEntity damage)
    {
        _image = damage.Image.ToImageSource();
        _date = damage.Date.ToDutchString();
        DamageId = damage.DamageId;
    }

    public ImageSource Image
    {
        get => _image;
        set => SetField(ref _image, value);
    }
    public string Date
    {
        get => _date;
        set => SetField(ref _date, value);
    }

    public int DamageId
    {
        get => _damageId;
        set => SetField(ref _damageId, value);
    }
}