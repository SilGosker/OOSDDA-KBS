using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kbs.Wpf.Components;

public static class ByteArrayExtensions
{
    public static ImageSource ToImageSource(this byte[] source)
    {
        byte[] pngImageHeader = new byte[]
        {
            0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A
        };

        byte[] jpgImageHeader = new byte[]
        {
            0xFF, 0xD8, 0xFF
        };

        if (source == null || source.Length == 0)
        {
            source = File.ReadAllBytes("./Images/NoImageAvailable.jpg");
        }
        else if (!source.Take(pngImageHeader.Length).SequenceEqual(pngImageHeader) &&
                     !source.Take(jpgImageHeader.Length).SequenceEqual(jpgImageHeader))
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