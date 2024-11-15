using System.Runtime.CompilerServices;

namespace Kbs.Business.Helpers;

public static class ThrowHelper
{
    public static void ThrowIfNull<T>(T value, [CallerArgumentExpression(nameof(value))] string argument = null)
    {
        if (value == null)
        {
            throw new ArgumentNullException(argument);
        }
    }
}