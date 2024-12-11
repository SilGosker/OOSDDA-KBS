namespace Kbs.Business.Course;

public static class CourseDifficultyExtensions
{
    public static string ToDutchString(this CourseDifficulty difficulty)
    {
        return difficulty switch
        {
            CourseDifficulty.Easy => "Makkelijk",
            CourseDifficulty.Medium => "Gemiddeld",
            CourseDifficulty.Hard => "Moeilijk",
            CourseDifficulty.VeryHard => "Heel moeilijk",
            _ => "Onbekend"
        };
    }
}