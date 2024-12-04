
namespace Kbs.Business.User
{
    public static class UserRoleExtension
    {
        public static string ToDutchString(this UserRole role)
        {
            return role switch
            {
                UserRole.MaterialCommissioner => "Materiaalcommissaris",
                UserRole.GameCommissioner => "Wedstrijdcommissaris",
                UserRole.Member => "Lid",
                (UserRole)7 => "Administrator",
                default(UserRole) => "Geband",
                _ => "Onbekende combinatie"
            };
        }
    }
}
