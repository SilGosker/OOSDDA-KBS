using Kbs.Business.Course;

namespace Kbs.Business.Parcours;

public class CourseDifficultyExtensionsTests
{
    [Theory]
    [InlineData(CourseDifficulty.Easy, "Makkelijk")]
    [InlineData(CourseDifficulty.Medium, "Gemiddeld")]
    [InlineData(CourseDifficulty.Hard, "Moeilijk")]
    [InlineData(CourseDifficulty.VeryHard, "Heel moeilijk")]
    [InlineData((CourseDifficulty)int.MaxValue, "Onbekend")]
    [InlineData((CourseDifficulty)0, "Onbekend")]
    public void ToDutchString_TranslatesValue(CourseDifficulty difficulty, string text)
    {
        // Assert & Act
        var result = difficulty.ToDutchString();

        // Assert
        Assert.Equal(text, result);
    }
}