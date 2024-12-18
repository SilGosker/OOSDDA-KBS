namespace Kbs.Web.Extensions;

public static class ByteArrayExtensions
{
    public static string ToHtmlImageSource(this byte[] bytes)
    {
        if (bytes == null || bytes.Length == 0)
        {
            return string.Empty;
        }
        var base64 = Convert.ToBase64String(bytes);
        return $"data:image/png;base64,{base64}";
    }
}