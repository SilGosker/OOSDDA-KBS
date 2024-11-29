using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Data.Boat;
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
    private readonly BoatRepository _boatRepository = new();
    private readonly BoatTypeEntity _boatTypeEntity = new();
    private SelectBoatTypeViewModel ViewModel => (SelectBoatTypeViewModel)DataContext;
    public SelectBoatTypePage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        var types = _boatTypeRepository.GetAllWithBoats();
        foreach (BoatTypeEntity type in types)
        {
            var user = _userRepository.GetUserIdByBoatTypeId(type.BoatTypeId);
            if (user != null)
            {
                ViewModel.Items.Add(new SelectBoatTypeBoatTypeViewModel(type, user));
            }
        }
    }
    public void BoatTypeSelected(object sender, MouseButtonEventArgs e)
    {
        var listViewItem = (ListViewItem)sender;
        var dataContext = (SelectBoatTypeBoatTypeViewModel)listViewItem.DataContext;
        var types = _boatTypeRepository.GetAllWithBoats();
        //var user = _userRepository.GetUserIdByBoatTypeId(_boatTypeEntity.BoatTypeId);  
        var userId = dataContext.UserId;
        var count = _reservationRepository.GetTotalReservations(userId);

        if (count != null)
        {
            int totalReservations = _reservationRepository.GetTotalReservations(userId);
            if (totalReservations > 1)
            {
                MessageBoxResult result = MessageBox.Show("U heeft het maximale aantal reserveringen bereikt");
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
}