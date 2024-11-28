using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.BoatType;
using Kbs.Data.BoatType;
using Kbs.Wpf.Reservation.Create.SelectTime;

namespace Kbs.Wpf.Reservation.Create.SelectBoatType;

public partial class SelectBoatTypePage : Page
{
    private readonly INavigationManager _navigationManager;
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private SelectBoatTypeViewModel ViewModel => (SelectBoatTypeViewModel)DataContext;
    public SelectBoatTypePage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();

        var types = _boatTypeRepository.GetAllWithBoats();
        foreach (BoatTypeEntity type in types)
        {
            ViewModel.Items.Add(new SelectBoatTypeBoatTypeViewModel(type));
        }
    }

    private void BoatTypeSelected(object sender, MouseButtonEventArgs e)
    {
        ListViewItem item = (ListViewItem)sender;
        var dataContext = (SelectBoatTypeBoatTypeViewModel)item.DataContext;
        var boatType = _boatTypeRepository.GetById(dataContext.BoatTypeId);
        _navigationManager.Navigate(() => new SelectTimePage(_navigationManager, boatType));
    }
}