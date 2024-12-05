namespace Kbs.Business.Course;

public class CourseEntityTests
{
    [Theory]
    [InlineData(1, new byte[] { 1, 2, 3 }, "Parcours 1", "Description 1", CourseDifficulty.Easy)]
    [InlineData(int.MaxValue, new byte[] { }, "", "", (CourseDifficulty)int.MaxValue)]
    [InlineData(int.MinValue, null, null, null, (CourseDifficulty)0)]
    public void Properties_ShouldSetProperties(int id, byte[] image, string name, string description,
        CourseDifficulty difficulty)

    {
        // Arrange
        var parcours = new CourseEntity();

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
