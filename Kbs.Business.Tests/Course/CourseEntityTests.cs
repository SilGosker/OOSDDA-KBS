namespace Kbs.Business.Course;

public class CourseEntityTests
{
    [Theory]
    [InlineData(1, new byte[] { 1, 2, 3 }, "Course 1", "Description 1", CourseDifficulty.Easy)]
    [InlineData(int.MaxValue, new byte[] { }, "", "", (CourseDifficulty)int.MaxValue)]
    [InlineData(int.MinValue, null, null, null, (CourseDifficulty)0)]
    public void Properties_ShouldSetProperties(int id, byte[] image, string name, string description,
        CourseDifficulty difficulty)

    {
        // Arrange
        var course = new CourseEntity();

        // Act
        course.ParcoursId = id;
        course.Image = image;
        course.Name = name;
        course.Description = description;
        course.Difficulty = difficulty;

        // Assert
        Assert.Equal(id, course.ParcoursId);
        Assert.Equal(image, course.Image);
        Assert.Equal(name, course.Name);
        Assert.Equal(description, course.Description);
        Assert.Equal(difficulty, course.Difficulty);
    }
}
