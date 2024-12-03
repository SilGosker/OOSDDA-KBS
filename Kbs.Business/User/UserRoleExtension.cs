using Kbs.Business.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                UserRole.Member => "Member",
                _ => "Onbekend"
            };
        }
    }
}
