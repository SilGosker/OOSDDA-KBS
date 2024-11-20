namespace Kbs.Business.BoatType;

public class BoatTypeRequiredExperienceExtensionsTests
{
    [Theory]
    [InlineData(BoatTypeRequiredExperience.Beginner, "Beginner")]
    [InlineData(BoatTypeRequiredExperience.Intermediate, "Gevorderd")]
    [InlineData(BoatTypeRequiredExperience.Expert, "Expert")]
    [InlineData((BoatTypeRequiredExperience)int.MaxValue, "Onbekend")]
    [InlineData((BoatTypeRequiredExperience)0, "Onbekend")]
    public void ToDutchString_ConvertsEnumToString(BoatTypeRequiredExperience experience, string expected)
    {
        // Act
        var result = experience.ToDutchString();

        // Assert
        Assert.Equal(expected, result);
    }
}