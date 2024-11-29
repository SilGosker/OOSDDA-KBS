using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using Kbs.Data.User;
using Kbs.Wpf.BoatType.Read.Details;
using Kbs.Wpf.BoatType.Read.Index;
using Kbs.Wpf.Reservation.Create.SelectTime;
using static Dapper.SqlMapper;

namespace Kbs.Wpf.Reservation.Create.SelectBoatType;

public partial class SelectBoatTypePage : Page
{
    private readonly INavigationManager _navigationManager;
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private readonly UserRepository _userRepository = new();
    private readonly ReservationValidator _reservationValidator = new();
    private readonly ReservationRepository _reservationRepository = new();
    private SelectBoatTypeViewModel ViewModel => (SelectBoatTypeViewModel)DataContext;
    public SelectBoatTypePage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();

        var types = _boatTypeRepository.GetAllWithBoats();
        foreach (BoatTypeEntity type in types)
        {
            var user = _userRepository.GetById(type.UserId);
            //Hij ziet user als null.............................
            if (user != null)
            {
                ViewModel.Items.Add(new SelectBoatTypeBoatTypeViewModel(type, user));
            }
        }
    }

    private void BoatTypeSelected(object sender, MouseButtonEventArgs e)
    {
        var listViewItem = (ListViewItem)sender;
        var dataContext = (SelectBoatTypeBoatTypeViewModel)listViewItem.DataContext;
        MessageBox.Show("UserId: " + dataContext.UserId); 
        var userId = dataContext.UserId;
        var count = _reservationRepository.GetTotalReservations(userId);
        if (count > 1)
        {
            MessageBoxResult result = MessageBox.Show("U heeft het maximale aantal reserveringen bereikt", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                return;
            }
            return;
        }
        else
        {
            ListViewItem item = (ListViewItem)sender;
            var boatType = _boatTypeRepository.GetById(dataContext.BoatTypeId);
            _navigationManager.Navigate(() => new SelectTimePage(_navigationManager, boatType));
        }
    }
}