using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kbs.Wpf.Components;

public static class ByteArrayExtensions
{
    public static ImageSource ToImageSource(this byte[] source)
    {
        if (source == null)
        {
            source = File.ReadAllBytes("./Images/NoImageAvailable.jpg");
        }

        var image = new BitmapImage();
        using (var mem = new MemoryStream(source))
        {
            mem.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = null;
            image.StreamSource = mem;
            image.EndInit();
            image.Freeze();
            return image;
        }
    }
}