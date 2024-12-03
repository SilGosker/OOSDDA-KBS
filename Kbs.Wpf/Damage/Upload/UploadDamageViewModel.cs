using System.Windows.Media;
using System.Windows.Media.Imaging;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Damage.Upload;

public class UploadDamageViewModel : ViewModel
{
    private string _description;
    private bool _boatIsFine;
    private string _imagePath;
    private string _fileErrorMessage;
    private string _descriptionErrorMessage;
    private int _boatId;
    public string Description
    {
        get => _description;
        set => SetField(ref _description, value);
    }

    public bool BoatIsFine
    {
        get => _boatIsFine;
        set => SetField(ref _boatIsFine, value);
    }

    public string ImagePath
    {
        get => _imagePath;
        set
        {
            SetField(ref _imagePath, value);
            OnPropertyChanged(nameof(Image));
        }
    }

    public ImageSource Image
    {
        get
        {
            if (string.IsNullOrWhiteSpace(ImagePath))
            {
                return null;
            }

            return new BitmapImage(new Uri(ImagePath));

        }
    }
    public string FileErrorMessage
    {
        get => _fileErrorMessage;
        set => SetField(ref _fileErrorMessage, value);
    }

    public string DescriptionErrorMessage
    {
        get => _descriptionErrorMessage;
        set => SetField(ref _descriptionErrorMessage, value);
    }

    public int BoatId
    {
        get => _boatId;
        set => SetField(ref _boatId, value);
    }
}