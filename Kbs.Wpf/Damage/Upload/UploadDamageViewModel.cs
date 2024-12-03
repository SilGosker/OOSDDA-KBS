using Kbs.Wpf.Components;

namespace Kbs.Wpf.Damage.Upload;

public class UploadDamageViewModel : ViewModel
{
    private string _description;
    private bool _boatStatusShouldChange;
    private string _imagePath;
    private string _fileErrorMessage;
    private string _descriptionErrorMessage;
    public string Description
    {
        get => _description;
        set => SetField(ref _description, value);
    }

    public bool BoatStatusShouldChange
    {
        get => _boatStatusShouldChange;
        set => SetField(ref _boatStatusShouldChange, value);
    }

    public string ImagePath
    {
        get => _imagePath;
        set => SetField(ref _imagePath, value);
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
}