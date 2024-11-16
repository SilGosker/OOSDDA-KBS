namespace Kbs.Business.User;

public class UserEntity
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; } = string.Empty;
    public Role Role { get; set; }

    public bool IsMember()
    {
        return Is(Role.Member);
    }

    public bool IsGameCommissioner()
    {
        return Is(Role.GameCommissioner);
    }

    public bool IsMaterialCommissioner()
    {
        return Is(Role.MaterialCommissioner);
    }

    public bool Is(Role role)
    {
        return (Role & role) != 0;
    }

    public void Encrypt()
    {
        Password = BCrypt.Net.BCrypt.HashPassword(Password);
    }
}