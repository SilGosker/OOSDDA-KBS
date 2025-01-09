using Kbs.Business.User;
using System.Windows.Controls;

namespace Kbs.Wpf.User.Ban
{
    [HasRole(UserRole.Banned)]
    public partial class BanUserPage : Page
    {
        public BanUserPage()
        {
            InitializeComponent();
        }
    }
}
