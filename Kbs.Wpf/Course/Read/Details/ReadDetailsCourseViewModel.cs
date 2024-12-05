using System.Windows.Media;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Course.Read.Details;

public class ReadDetailsCourseViewModel : ViewModel
{
    private string _name;
    private string _description;
    private ImageSource _imageSource;
    private byte[] _image;
    private string _difficulty;
    private int _id;
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
    public string Difficulty
    {
        get => _difficulty;
        set => SetField(ref _difficulty, value);
    }
}