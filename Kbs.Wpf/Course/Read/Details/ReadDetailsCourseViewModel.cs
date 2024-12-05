using System.Collections.ObjectModel;
using System.Windows.Media;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Course.Read.Details;

public class ReadDetailsCourseViewModel : ViewModel
{
    private string _name;
    private string _description;
    private ImageSource _imageSource;
    private byte[] _image;
    private int _id;
    private string _nameErrorMessage;
    private string _imageErrorMessage;
    private string _difficultyErrorMessage;
    private ReadDetailsCourseDifficultyViewModel _selectedDifficulty;
    public int Id
    {
        get => _id;
        set
        {
            SetField(ref _id, value);
            OnPropertyChanged(nameof(IdFormatted));
        } 
    }

    public string IdFormatted => $"Parcours #{Id}";

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public string Description
    {
        get => _description;
        set => SetField(ref _description, value);
    }
    public byte[] Image
    {
        get => _image;
        set
        {
            SetField(ref _image, value);
            ImageSource = value.ToImageSource();
        } 
    }
    public ImageSource ImageSource
    {
        get => _imageSource;
        set => SetField(ref _imageSource, value);
    }
    public string NameErrorMessage
    {
        get => _nameErrorMessage;
        set => SetField(ref _nameErrorMessage, value);
    }
    public string ImageErrorMessage
    {
        get => _imageErrorMessage;
        set => SetField(ref _imageErrorMessage, value);
    }
    public string DifficultyErrorMessage
    {
        get => _difficultyErrorMessage;
        set => SetField(ref _difficultyErrorMessage, value);
    }

    public ObservableCollection<ReadDetailsCourseDifficultyViewModel> Difficulties { get; } = new();
    public ReadDetailsCourseDifficultyViewModel SelectedDifficulty
    {
        get => _selectedDifficulty;
        set => SetField(ref _selectedDifficulty, value);
    }
}