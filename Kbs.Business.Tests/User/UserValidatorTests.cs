namespace Kbs.Business.User;

public class UserValidatorTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    public void ValidateForLogin_WithNoEmail_ReturnsErrors(string email)
    {
        // Arrange
        var user = new UserEntity()
        {
            Password = "test",
            Email = email
        };

        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForLogIn(user);

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Email)));
        Assert.Equal("Email is verplicht", validationResult[nameof(user.Email)]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ValidateForLogin_WithNoPassword_ReturnsErrors(string password)
    {
        // Arrange
        var user = new UserEntity()
        {
            Password = password,
            Email = "test@gmail.com"
        };

        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForLogIn(user);

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Password)));
        Assert.Equal("Wachtwoord is verplicht", validationResult[nameof(user.Password)]);
    }

    [Fact]
    public void ValidateForLogin_WithInvalidEmail_ReturnsErrors()
    {
        // Arrange
        var user = new UserEntity()
        {
            Password = "test 123",
            Email = "test"
        };

        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForLogIn(user);

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Email)));
        Assert.Equal("Ongeldig email adres", validationResult[nameof(user.Email)]);
    }

    [Fact]
    public void ValidateForLogin_WithValidUser_ReturnsEmptyDictionary()
    {
        // Arrange
        var user = new UserEntity()
        {
            Password = "test 123",
            Email = "test@gmail.com"
        };

        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForLogIn(user);

        // Assert
        Assert.Empty(validationResult);
    }

    [Fact]
    public void ValidateForLogin_WithEmptyUser_ReturnsErrors()
    {
        // Arrange
        var user = new UserEntity();

        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForLogIn(user);

        // Assert
        Assert.Equal(2, validationResult.Count);
        Assert.True(validationResult.ContainsKey(nameof(user.Email)));
        Assert.True(validationResult.ContainsKey(nameof(user.Password)));
        Assert.Equal("Email is verplicht", validationResult[nameof(user.Email)]);
        Assert.Equal("Wachtwoord is verplicht", validationResult[nameof(user.Password)]);
    }
}