namespace Kbs.Business.Damage;

public class DamageValidator
{
    public Dictionary<string, string> ValidateForUpload(DamageEntity damage)
    {
        var result = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(damage.Description))
        {
            result.Add(nameof(damage.Description), "Omschrijving is verplicht");
        }

        byte[] pngImageHeader = new byte[]
        {
            0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A
        };

        byte[] jpgImageHeader = new byte[]
        {
            0xFF, 0xD8, 0xFF
        };

        if (damage.Image == null || damage.Image.Length == 0)
        {
            result.Add(nameof(damage.Image), "Afbeelding is verplicht");
        }
        else if (!damage.Image.Take(pngImageHeader.Length).SequenceEqual(pngImageHeader) &&
                 !damage.Image.Take(jpgImageHeader.Length).SequenceEqual(jpgImageHeader))
        {
            result.Add(nameof(damage.Image), "Afbeelding is geen PNG of JPG");
        }
        
        return result;
    }
}