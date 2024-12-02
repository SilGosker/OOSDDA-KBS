using System.Windows.Controls;
using Kbs.Data.Damage;

namespace Kbs.Wpf.Damage.Read.Details;

public partial class ReadDamageDetails : Page
{
    private readonly DamageRepository _damageRepository = new();
    private ReadDamageDetailsViewModel ViewModel => (ReadDamageDetailsViewModel)DataContext;
    public ReadDamageDetails(int damageId)
    {
        InitializeComponent();

    }
}