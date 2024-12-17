using System.ComponentModel.DataAnnotations.Schema;

namespace Kbs.Business.User;

public class UserEntity
{
    [Column("UserID")]
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public UserRole Role { get; set; }

    public bool IsMember()
    {
        return Is(UserRole.Member);
    }

    public bool IsGameCommissioner()
    {
        return Is(UserRole.GameCommissioner);
    }

    public bool IsMaterialCommissioner()
    {
        return Is(UserRole.MaterialCommissioner);
    }

    public bool Is(UserRole userRole)
    {
        // banned is 0, bitwise AND will always return 0, resulting in false while the user is banned
        if (userRole == UserRole.Banned)
        {
            return Role == UserRole.Banned;
        }

        return (Role & userRole) != 0;
    }

    public void Encrypt()
    {
        Password = BCrypt.Net.BCrypt.HashPassword(Password);
    }
}