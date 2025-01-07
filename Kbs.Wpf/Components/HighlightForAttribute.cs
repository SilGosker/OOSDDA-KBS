namespace Kbs.Wpf.Components;

[AttributeUsage(AttributeTargets.Class)]
public class HighlightForAttribute : Attribute
{
    public HighlightForAttribute(Type type)
    {
        Type = type;
    }

    public Type Type { get; }
}