using System.Windows.Media;
using System.Windows.Media.Imaging;
using Kbs.Wpf.Components;
using Kbs.Business.Course;
using System.Collections.ObjectModel;
using Kbs.Wpf.Course.Components;

namespace Kbs.Wpf.Course.Create;

public class CreateCourseViewModel : ViewModel
{
    private string _name;
    private string _nameErrorMessage;
    private string _imagePath;
    private string _imageErrorMessage;
    private CourseDifficulty _difficulty;
    private string _description;
    private string _difficultyErrorMessage;
    public ObservableCollection<CourseDifficultyViewModel> PossibleDifficulties { get; } = new();
    public string Name 
    { 
        get => _name; 
        set => SetField(ref _name, value); 
    }
    public string NameErrorMessage 
    { 
        get => _nameErrorMessage;
        set => SetField(ref _nameErrorMessage, value);
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

    public string ImageErrorMessage 
    { 
        get => _imageErrorMessage;
        set => SetField(ref _imageErrorMessage, value);
    }
    public CourseDifficulty Difficulty 
    { 
        get => _difficulty;
        set => SetField(ref _difficulty, value);
    }
    public string Description 
    { 
        get => _description;
        set => SetField(ref _description, value);
    }

    public string DifficultyErrorMessage
    {
        get => _difficultyErrorMessage;
        set => SetField(ref _difficultyErrorMessage, value);
    }
}
