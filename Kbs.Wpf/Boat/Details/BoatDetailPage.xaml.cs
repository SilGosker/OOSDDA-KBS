using System.Windows.Controls;
using Kbs.Business.User;
using Kbs.Wpf.Attributes;

namespace Kbs.Wpf.Boat.Details;

[HasRole(Role.MaterialCommissioner)]
public partial class BoatDetailPage : Page
{
    public BoatDetailPage(int boatId)
    {
        InitializeComponent();
    }
}