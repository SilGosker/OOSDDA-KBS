using System.Globalization;
using System.Windows.Data;

namespace Kbs.Wpf.Components;

public class WeekDayTranslationConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string text)
        {
            return text.Replace('T', 'D')
                .Replace('S', 'Z')
                .Replace('F', 'V');
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Optional: Handle reverse conversion if necessary
        return value;
    }
}