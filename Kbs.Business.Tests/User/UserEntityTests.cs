namespace Kbs.Business.User;

public class UserEntityTests
{
    [Theory]
    [InlineData("user@test.com", "test 123", "User", Role.Member)]
    [InlineData(null, null, null, (Role)0)]
    [InlineData("test", "", "", Role.Member | Role.GameCommissioner | Role.MaterialCommissioner)]
    public void Properties_SetProperties(string email, string password, string name, Role role)
    {
        // Arrange
        var user = new UserEntity();

        // Act
        user.Name = name;
        user.Password = password;
        user.Email = email;
        user.Role = role;

        // Assert
        Assert.Equal(name, user.Name);
        Assert.Equal(password, user.Password);
        Assert.Equal(email, user.Email);
        Assert.Equal(role, user.Role);
    }

    [Theory]
    [InlineData(Role.Member)]
    [InlineData(Role.GameCommissioner)]
    [InlineData(Role.MaterialCommissioner)]
    [InlineData(Role.Member | Role.GameCommissioner)]
    [InlineData(Role.Member | Role.GameCommissioner | Role.MaterialCommissioner)]
    public void Is_ReturnsTrue_IfUserHasRole(Role role)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = role
        };

        // Act
        var hasRole = user.Is(role);

        // Assert
        Assert.True(hasRole);
    }

    [Theory]
    [InlineData(Role.Member, Role.GameCommissioner)]
    [InlineData(Role.GameCommissioner, Role.MaterialCommissioner)]
    [InlineData(Role.MaterialCommissioner, Role.Member)]
    [InlineData(Role.Member | Role.GameCommissioner, Role.MaterialCommissioner)]
    public void Is_ReturnsFalse_IfUseDoesNotHaveRole(Role given, Role check)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = given
        };

        // Act
        var hasRole = user.Is(check);

        // Assert
        Assert.False(hasRole);
    }

    [Theory]
    [InlineData(Role.Member)]
    [InlineData(Role.Member | Role.GameCommissioner)]
    [InlineData(Role.Member | Role.MaterialCommissioner)]
    [InlineData(Role.Member | Role.GameCommissioner | Role.MaterialCommissioner)]
    public void IsMember_ReturnsTrue_IfUserIsMember(Role role)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = role
        };

        // Act
        var isMember = user.IsMember();

        // Assert
        Assert.True(isMember);
    }

    [Theory]
    [InlineData(Role.GameCommissioner)]
    [InlineData(Role.MaterialCommissioner)]
    [InlineData(Role.GameCommissioner | Role.MaterialCommissioner)]
    public void IsMember_ReturnsFalse_IfUserIsNotMember(Role role)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = role
        };

        // Act
        var isMember = user.IsMember();

        // Assert
        Assert.False(isMember);
    }

    [Theory]
    [InlineData(Role.GameCommissioner)]
    [InlineData(Role.Member | Role.GameCommissioner)]
    public void IsGameCommissioner_ReturnsTrue_IfUserIsGameCommissioner(Role role)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = role
        };

        // Act
        var isGameCommissioner = user.IsGameCommissioner();

        // Assert
        Assert.True(isGameCommissioner);
    }

    [Theory]
    [InlineData(Role.Member)]
    [InlineData(Role.MaterialCommissioner)]
    [InlineData(Role.Member | Role.MaterialCommissioner)]
    public void IsGameCommissioner_ReturnsFalse_IfUserIsNotGameCommissioner(Role role)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = role
        };

        // Act
        var isGameCommissioner = user.IsGameCommissioner();

        // Assert
        Assert.False(isGameCommissioner);
    }

    [Theory]
    [InlineData(Role.MaterialCommissioner)]
    [InlineData(Role.Member | Role.MaterialCommissioner)]
    public void IsMaterialCommissioner_ReturnsTrue_IfUserIsMaterialCommissioner(Role role)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = role
        };

        // Act
        var isMaterialCommissioner = user.IsMaterialCommissioner();

        // Assert
        Assert.True(isMaterialCommissioner);
    }

    [Theory]
    [InlineData(Role.Member)]
    [InlineData(Role.GameCommissioner)]
    [InlineData(Role.Member | Role.GameCommissioner)]
    public void IsMaterialCommissioner_ReturnsFalse_IfUserIsNotMaterialCommissioner(Role role)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = role
        };

        // Act
        var isMaterialCommissioner = user.IsMaterialCommissioner();

        // Assert
        Assert.False(isMaterialCommissioner);
    }
}