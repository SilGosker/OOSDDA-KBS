namespace Kbs.Business.Damage;

public static class DamageStatusExtensions
{
    public static string ToDutchString(this DamageStatus status)
    {
        return status switch
        {
            DamageStatus.UnSolved => "Open",
            DamageStatus.Solved => "Gesloten",
            _ => "Onbekend"
        };
    }
}