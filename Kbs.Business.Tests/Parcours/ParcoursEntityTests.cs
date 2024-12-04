namespace Kbs.Business.Parcours;

public class ParcoursEntityTests
{
    [Theory]
    [InlineData(1, new byte[] { 1, 2, 3 }, "Parcours 1", "Description 1", ParcoursDifficulty.Easy)]
    [InlineData(int.MaxValue, new byte[] { }, "", "", (ParcoursDifficulty)int.MaxValue)]
    [InlineData(int.MinValue, null, null, null, (ParcoursDifficulty)0)]
    public void Properties_ShouldSetProperties(int id, byte[] image, string name, string description,
        ParcoursDifficulty difficulty)

    {
        // Arrange
        var parcours = new ParcoursEntity();

        // Act
        parcours.ParcoursId = id;
        parcours.Image = image;
        parcours.Name = name;
        parcours.Description = description;
        parcours.Difficulty = difficulty;

        // Assert
        Assert.Equal(id, parcours.ParcoursId);
        Assert.Equal(image, parcours.Image);
        Assert.Equal(name, parcours.Name);
        Assert.Equal(description, parcours.Description);
        Assert.Equal(difficulty, parcours.Difficulty);
    }
}
