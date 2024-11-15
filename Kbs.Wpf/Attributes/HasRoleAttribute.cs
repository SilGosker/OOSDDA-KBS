
using Kbs.Business.User;

namespace Kbs.Wpf.Attributes;

public class HasRoleAttribute : Attribute
{
    public Role Role { get; }

    public HasRoleAttribute(Role role)
    {
        Role = role;
    }
}