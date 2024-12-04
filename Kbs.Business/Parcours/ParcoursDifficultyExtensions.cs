namespace Kbs.Business.Parcours;

public static class ParcoursDifficultyExtensions
{
    public static string ToDutchString(this ParcoursDifficulty difficulty)
    {
        return difficulty switch
        {
            ParcoursDifficulty.Easy => "Makkelijk",
            ParcoursDifficulty.Medium => "Gemiddeld",
            ParcoursDifficulty.Hard => "Moeilijk",
            ParcoursDifficulty.VeryHard => "Heel moeilijk",
            _ => "Onbekend"
        };
    }
}