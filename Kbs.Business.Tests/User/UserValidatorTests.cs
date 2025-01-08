using Kbs.Business.Mock;

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

    [Fact]
    public void ValidateForUpdate_PasswordLacksSmallLetter_ReturnsErrors()
    {
        // Arrange
        var userRepository = new MockUserRepository();
        UserEntity oldUser = new UserEntity()
        {
            Email = "tester@gmail.com",
            Password = "123456"
        };
        userRepository.Users.Add(oldUser);

        string password = "ABCD123!";
        var user = new UserEntity()
        {
            Password = password,
            Email = "test@gmail.com"
        };

        var validator = new UserValidator();
        string errorMessage = "- Wachtwoord moet minimaal 1 hoofdletter en 1 kleine letter bevatten\r\n";

        // Act
        var validationResult = validator.ValidatorForUpdate(user, password, userRepository, oldUser.Email);

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Password)));
        Assert.Equal(errorMessage, validationResult[nameof(user.Password)]);
    }

    [Fact]
    public void ValidateForUpdate_PasswordLacksBigLetter_ReturnsErrors()
    {
        // Arrange
        var userRepository = new MockUserRepository();
        UserEntity oldUser = new UserEntity()
        {
            Email = "tester@gmail.com",
            Password = "123456"
        };
        userRepository.Users.Add(oldUser);

        string password = "abcd123!";
        var user = new UserEntity()
        {
            Password = password,
            Email = "test@gmail.com"
        };

        var validator = new UserValidator();
        string errorMessage = "- Wachtwoord moet minimaal 1 hoofdletter en 1 kleine letter bevatten\r\n";

        // Act
        var validationResult = validator.ValidatorForUpdate(user, password, userRepository, oldUser.Email);

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Password)));
        Assert.Equal(errorMessage, validationResult[nameof(user.Password)]);
    }
    [Fact]
    public void ValidateForUpdate_PasswordLacksNumber_ReturnsErrors()
    {
        // Arrange
        var userRepository = new MockUserRepository();
        UserEntity oldUser = new UserEntity()
        {
            Email = "tester@gmail.com",
            Password = "123456"
        };
        userRepository.Users.Add(oldUser);

        string password = "Abcd#$@!";
        var user = new UserEntity()
        {
            Password = password,
            Email = "test@gmail.com"
        };

        var validator = new UserValidator();
        string errorMessage = "- Wachtwoord moet minimaal 1 cijfer bevatten\r\n";

        // Act
        var validationResult = validator.ValidatorForUpdate(user, password, userRepository, oldUser.Email);

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Password)));
        Assert.Equal(errorMessage, validationResult[nameof(user.Password)]);
    }

    [Fact]
    public void ValidateForUpdate_PasswordLacksSpecialCharacter_ReturnsErrors()
    {
        // Arrange
        var userRepository = new MockUserRepository();
        UserEntity oldUser = new UserEntity()
        {
            Email = "tester@gmail.com",
            Password = "123456"
        };
        userRepository.Users.Add(oldUser);

        string password = "Abcd1234";
        var user = new UserEntity()
        {
            Password = password,
            Email = "test@gmail.com"
        };

        var validator = new UserValidator();
        string errorMessage = "- Wachtwoord moet minimaal 1 speciaal teken bevatten\r\n";

        // Act
        var validationResult = validator.ValidatorForUpdate(user, password, userRepository, oldUser.Email);

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Password)));
        Assert.Equal(errorMessage, validationResult[nameof(user.Password)]);
    }

    [Fact]
    public void ValidateForUpdate_PasswordLacksLength_ReturnsErrors()
    {
        // Arrange
        var userRepository = new MockUserRepository();
        UserEntity oldUser = new UserEntity()
        {
            Email = "tester@gmail.com",
            Password = "123456"
        };
        userRepository.Users.Add(oldUser);

        string password = "Abc123!";
        var user = new UserEntity()
        {
            Password = password,
            Email = "test@gmail.com"
        };

        var validator = new UserValidator();
        string errorMessage = "- Wachtwoord moet minimaal 8 tekens lang zijn\r\n";

        // Act
        var validationResult = validator.ValidatorForUpdate(user, password, userRepository, oldUser.Email);

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Password)));
        Assert.Equal(errorMessage, validationResult[nameof(user.Password)]);
    }

    [Theory]
    [InlineData("Test123!!", "Appel123")]
    [InlineData("Test123!!", "Test123")]
    public void ValidateForUpdate_InvalidMatchingPassword_ReturnsErrors(string password, string confirmation)
    {
        // Arrange
        var userRepository = new MockUserRepository();
        UserEntity oldUser = new UserEntity()
        {
            Email = "tester@gmail.com",
            Password = "123456"
        };
        userRepository.Users.Add(oldUser);
        var user = new UserEntity()
        {
            Password = password,
            Email = "test@gmail.com"
        };

        var validator = new UserValidator();
        string errorMessage = "- Wachtwoorden komen niet overeen\r\n";

        // Act
        var validationResult = validator.ValidatorForUpdate(user, confirmation, userRepository, oldUser.Email);

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey("Password"));
        Assert.Equal(errorMessage, validationResult["Password"]);
    }

    [Theory]
    [InlineData("Test 123!!")]
    public void ValidateForUpdate_WithInvalidEmail_ReturnsErrors(string password)
    {
        // Arrange
        var userRepository = new MockUserRepository();
        UserEntity oldUser = new UserEntity()
        {
            Email = "tester@gmail.com",
            Password = "123456"
        };
        userRepository.Users.Add(oldUser);

        var user = new UserEntity()
        {
            Password = password,
            Email = "test"
        };

        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForUpdate(user, password, userRepository, oldUser.Email);
        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Email)));
        Assert.Equal("Ongeldig email adres", validationResult[nameof(user.Email)]);
    }

    [Theory]
    [InlineData("Test 123!!")]
    public void ValidateForUpdate_WithValidUser_ReturnsEmptyDictionary(string password)
    {
        // Arrange
        var userRepository = new MockUserRepository();
        UserEntity oldUser = new UserEntity()
        {
            Email = "tester@gmail.com",
            Password = "123456"
        };
        userRepository.Users.Add(oldUser);

        var user = new UserEntity()
        {
            Password = password,
            Email = "test@gmail.com"
        };

        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForUpdate(user, password, userRepository, oldUser.Email);
        // Assert
        Assert.Empty(validationResult);
    }

    [Fact]
    public void ValidatorForRegistration_WithEmptyPassword_ReturnsErrors()
    {
        // Arrange
        var user = new UserEntity { Email = "test@example.com", Password = "" };
        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForRegister(user, "");

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Password)));
        Assert.Equal("Wachtwoord is verplicht", validationResult[nameof(user.Password)]);
    }

    [Fact]
    public void ValidatorForRegistration_WithShortPassword_ReturnsErrors()
    {
        // Arrange
        var user = new UserEntity { Email = "test@example.com", Password = "Short1!" };
        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForRegister(user, "Short1!");

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Password)));
        Assert.Contains("Wachtwoord moet minimaal 8 tekens lang zijn", validationResult[nameof(user.Password)]);
    }

    [Fact]
    public void ValidatorForRegistration_WithPasswordLackingUppercase_ReturnsErrors()
    {
        // Arrange
        var user = new UserEntity { Email = "test@example.com", Password = "lowercase1!" };
        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForRegister(user, "lowercase1!");

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Password)));
        Assert.Contains("Wachtwoord moet minimaal 1 hoofdletter en 1 kleine letter bevatten", validationResult[nameof(user.Password)]);
    }

    [Fact]
    public void ValidatorForRegistration_WithPasswordLackingDigit_ReturnsErrors()
    {
        // Arrange
        var user = new UserEntity { Email = "test@example.com", Password = "Password!" };
        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForRegister(user, "Password!");

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Password)));
        Assert.Contains("Wachtwoord moet minimaal 1 cijfer bevatten", validationResult[nameof(user.Password)]);
    }

    [Fact]
    public void ValidatorForRegistration_WithPasswordLackingSpecialCharacter_ReturnsErrors()
    {
        // Arrange
        var user = new UserEntity { Email = "test@example.com", Password = "Password1" };
        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForRegister(user, "Password1");

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Password)));
        Assert.Contains("Wachtwoord moet minimaal 1 speciaal teken bevatten", validationResult[nameof(user.Password)]);
    }

    [Fact]
    public void ValidatorForRegistration_WithEmptyEmail_ReturnsErrors()
    {
        // Arrange
        var user = new UserEntity { Email = "", Password = "Password1!" };
        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForRegister(user, "Password1!");

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Email)));
        Assert.Equal("Email is verplicht", validationResult[nameof(user.Email)]);
    }

    [Fact]
    public void ValidatorForRegistration_WithInvalidEmail_ReturnsErrors()
    {
        // Arrange
        var user = new UserEntity { Email = "invalid-email", Password = "Password1!" };
        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForRegister(user, "Password1!");

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Email)));
        Assert.Equal("Ongeldig email adres", validationResult[nameof(user.Email)]);
    }

    [Fact]
    public void ValidatorForRegistration_WithNonMatchingPasswords_ReturnsErrors()
    {
        // Arrange
        var user = new UserEntity { Email = "test@example.com", Password = "Password1!" };
        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForRegister(user, "DifferentPassword1!");

        // Assert
        Assert.Single(validationResult);
        Assert.True(validationResult.ContainsKey(nameof(user.Password)));
        Assert.Contains("Wachtwoorden komen niet overeen", validationResult[nameof(user.Password)]);
    }

    [Fact]
    public void ValidatorForRegistration_WithValidInput_ReturnsNoErrors()
    {
        // Arrange
        var user = new UserEntity { Email = "test@example.com", Password = "Password1!" };
        var validator = new UserValidator();

        // Act
        var validationResult = validator.ValidatorForRegister(user, "Password1!");

        // Assert
        Assert.Empty(validationResult);
    }
}