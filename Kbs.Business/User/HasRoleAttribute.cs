
namespace Kbs.Business.User;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class HasRoleAttribute : Attribute
{
    public UserRole UserRole { get; }

    public HasRoleAttribute(UserRole userRole)
    {
        UserRole = userRole;
    }
}