
namespace Kbs.Business.User;

public class HasRoleAttribute : Attribute
{
    public UserRole UserRole { get; }

    public HasRoleAttribute(UserRole userRole)
    {
        UserRole = userRole;
    }
}