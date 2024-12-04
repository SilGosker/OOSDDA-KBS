using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using Kbs.Wpf.Reservation.Create.SelectTime;

namespace Kbs.Wpf.Reservation.Create.SelectBoatType;

public partial class SelectBoatTypePage : Page
{
    private readonly INavigationManager _navigationManager;
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private readonly ReservationValidator _reservationValidator = new();
    private readonly ReservationRepository _reservationRepository = new();
    private SelectBoatTypeViewModel ViewModel => (SelectBoatTypeViewModel)DataContext;
    private UserEntity _user = SessionManager.Instance.Current.User;
    private BoatTypeEntity _selectedBoatType;
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

    public void BoatTypeSelected(object sender, MouseButtonEventArgs e)
    {

        if (_user.Role == UserRole.Member)
        {
            int userID = SessionManager.Instance.Current.User.UserId;
            int totalReservations = _reservationRepository.CountByUser(userID);
            if (_reservationValidator.IsReservationLimitReached(totalReservations))
            {
                MessageBox.Show("U heeft het maximale aantal reserveringen bereikt");
                return;
            }
            else
            {
                var listViewItem = (ListViewItem)sender;
                var dataContext = (SelectBoatTypeBoatTypeViewModel)listViewItem.DataContext;
                var boatType = _boatTypeRepository.GetById(dataContext.BoatTypeId);
                _navigationManager.Navigate(() => new SelectTimePage(_navigationManager, boatType));
            }
        }
        else
        {
            var listViewItem = (ListViewItem)sender;
            var dataContext = (SelectBoatTypeBoatTypeViewModel)listViewItem.DataContext;
            _selectedBoatType = _boatTypeRepository.GetById(dataContext.BoatTypeId);
            _navigationManager.Navigate(() => new SelectTimePage(_navigationManager, _selectedBoatType));
        }
    }
}
