namespace Kbs.Business.Parcours;

public class ParcoursDifficultyExtensionsTests
{
    [Theory]
    [InlineData(ParcoursDifficulty.Easy, "Makkelijk")]
    [InlineData(ParcoursDifficulty.Medium, "Gemiddeld")]
    [InlineData(ParcoursDifficulty.Hard, "Moeilijk")]
    [InlineData(ParcoursDifficulty.VeryHard, "Heel moeilijk")]
    [InlineData((ParcoursDifficulty)int.MaxValue, "Onbekend")]
    [InlineData((ParcoursDifficulty)0, "Onbekend")]
    public void ToDutchString_TranslatesValue(ParcoursDifficulty difficulty, string text)
    {
        // Assert & Act
        var result = difficulty.ToDutchString();

        // Assert
        Assert.Equal(text, result);
    }
}