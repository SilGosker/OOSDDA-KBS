namespace Kbs.Business.User;

public class UserEntityTests
{
    [Theory]
    [InlineData("user@test.com", "test 123", "User", UserRole.Member)]
    [InlineData(null, null, null, (UserRole)0)]
    [InlineData("test", "", "", UserRole.Member | UserRole.GameCommissioner | UserRole.MaterialCommissioner)]
    public void Properties_SetProperties(string email, string password, string name, UserRole userRole)
    {
        // Arrange
        var user = new UserEntity();

        // Act
        user.Name = name;
        user.Password = password;
        user.Email = email;
        user.Role = userRole;

        // Assert
        Assert.Equal(name, user.Name);
        Assert.Equal(password, user.Password);
        Assert.Equal(email, user.Email);
        Assert.Equal(userRole, user.Role);
    }

    [Theory]
    [InlineData(UserRole.Member)]
    [InlineData(UserRole.GameCommissioner)]
    [InlineData(UserRole.MaterialCommissioner)]
    [InlineData(UserRole.Member | UserRole.GameCommissioner)]
    [InlineData(UserRole.Member | UserRole.GameCommissioner | UserRole.MaterialCommissioner)]
    public void Is_ReturnsTrue_IfUserHasRole(UserRole userRole)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = userRole
        };

        // Act
        var hasRole = user.Is(userRole);

        // Assert
        Assert.True(hasRole);
    }

    [Theory]
    [InlineData(UserRole.Member, UserRole.GameCommissioner)]
    [InlineData(UserRole.GameCommissioner, UserRole.MaterialCommissioner)]
    [InlineData(UserRole.MaterialCommissioner, UserRole.Member)]
    [InlineData(UserRole.Member | UserRole.GameCommissioner, UserRole.MaterialCommissioner)]
    public void Is_ReturnsFalse_IfUseDoesNotHaveRole(UserRole given, UserRole check)
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
    [InlineData(UserRole.Member)]
    [InlineData(UserRole.Member | UserRole.GameCommissioner)]
    [InlineData(UserRole.Member | UserRole.MaterialCommissioner)]
    [InlineData(UserRole.Member | UserRole.GameCommissioner | UserRole.MaterialCommissioner)]
    public void IsMember_ReturnsTrue_IfUserIsMember(UserRole userRole)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = userRole
        };

        // Act
        var isMember = user.IsMember();

        // Assert
        Assert.True(isMember);
    }

    [Theory]
    [InlineData(UserRole.GameCommissioner)]
    [InlineData(UserRole.MaterialCommissioner)]
    [InlineData(UserRole.GameCommissioner | UserRole.MaterialCommissioner)]
    public void IsMember_ReturnsFalse_IfUserIsNotMember(UserRole userRole)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = userRole
        };

        // Act
        var isMember = user.IsMember();

        // Assert
        Assert.False(isMember);
    }

    [Theory]
    [InlineData(UserRole.GameCommissioner)]
    [InlineData(UserRole.Member | UserRole.GameCommissioner)]
    public void IsGameCommissioner_ReturnsTrue_IfUserIsGameCommissioner(UserRole userRole)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = userRole
        };

        // Act
        var isGameCommissioner = user.IsGameCommissioner();

        // Assert
        Assert.True(isGameCommissioner);
    }

    [Theory]
    [InlineData(UserRole.Member)]
    [InlineData(UserRole.MaterialCommissioner)]
    [InlineData(UserRole.Member | UserRole.MaterialCommissioner)]
    public void IsGameCommissioner_ReturnsFalse_IfUserIsNotGameCommissioner(UserRole userRole)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = userRole
        };

        // Act
        var isGameCommissioner = user.IsGameCommissioner();

        // Assert
        Assert.False(isGameCommissioner);
    }

    [Theory]
    [InlineData(UserRole.MaterialCommissioner)]
    [InlineData(UserRole.Member | UserRole.MaterialCommissioner)]
    public void IsMaterialCommissioner_ReturnsTrue_IfUserIsMaterialCommissioner(UserRole userRole)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = userRole
        };

        // Act
        var isMaterialCommissioner = user.IsMaterialCommissioner();

        // Assert
        Assert.True(isMaterialCommissioner);
    }

    [Theory]
    [InlineData(UserRole.Member)]
    [InlineData(UserRole.GameCommissioner)]
    [InlineData(UserRole.Member | UserRole.GameCommissioner)]
    public void IsMaterialCommissioner_ReturnsFalse_IfUserIsNotMaterialCommissioner(UserRole userRole)
    {
        // Arrange
        var user = new UserEntity()
        {
            Role = userRole
        };

        // Act
        var isMaterialCommissioner = user.IsMaterialCommissioner();

        // Assert
        Assert.False(isMaterialCommissioner);
    }
}