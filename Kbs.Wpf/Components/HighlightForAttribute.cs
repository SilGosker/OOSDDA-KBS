namespace Kbs.Wpf.Components;

[AttributeUsage(AttributeTargets.Class)]
public class HighlightForAttribute(Type type) : Attribute
{
    public Type Type { get; } = type;
}