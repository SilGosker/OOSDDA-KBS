using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.BoatType;
using Kbs.Business.Game;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using Kbs.Wpf.Components;
using Kbs.Wpf.Reservation.Create.SelectTime;

namespace Kbs.Wpf.Reservation.Create.SelectBoatType;

[HasRole(UserRole.Member)]
[HasRole(UserRole.GameCommissioner)]
[HighlightFor(typeof(ReservationTime))]
public partial class SelectBoatTypePage : Page
{
    private readonly INavigationManager _navigationManager;
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private readonly ReservationValidator _reservationValidator = new();
    private readonly ReservationRepository _reservationRepository = new();
    private readonly UserEntity _user = SessionManager.Instance.Current.User;
    private BoatTypeEntity _selectedBoatType;
    private SelectBoatTypeViewModel ViewModel => (SelectBoatTypeViewModel)DataContext;
    private readonly GameEntity _game;

    public SelectBoatTypePage(INavigationManager navigationManager, GameEntity game) : this(navigationManager)
    {
        _game = game;
        ViewModel.GameCreateMessage = "U reserveert boten voor Wedstrijd #" + _game.GameId;
    }

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
        //only users can make a maximum of 2 reservations, this excludes administrators.
        if (_user.Role == UserRole.Member)
        {
            int userId = SessionManager.Instance.Current.User.UserId;
            int totalReservations = _reservationRepository.CountByUser(userId);
            if (_reservationValidator.IsReservationLimitReached(totalReservations))
            {
                MessageBox.Show("U heeft het maximale aantal reserveringen bereikt");
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

            if (_game != null)
            {
                _navigationManager.Navigate(() => new SelectTimePage(_navigationManager, _selectedBoatType, _game));
            }
            else
            {
                _navigationManager.Navigate(() => new SelectTimePage(_navigationManager, _selectedBoatType));
            }
        }
    }
}