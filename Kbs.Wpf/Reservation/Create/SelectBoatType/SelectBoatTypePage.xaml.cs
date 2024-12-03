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
    private readonly BoatRepository _boatRepository = new();
    private SelectBoatTypeViewModel ViewModel => (SelectBoatTypeViewModel)DataContext;
    public UserEntity User = SessionManager.Instance.Current.User;
    public int AmountOfBoats;
    public BoatTypeEntity SelectedBoatType;
    private SelectBoatTypeBoatTypeViewModel _previousSelection;
    public SelectBoatTypePage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        var types = _boatTypeRepository.GetAllWithBoats();
        foreach (BoatTypeEntity type in types)
        {
            ViewModel.Items.Add(new SelectBoatTypeBoatTypeViewModel(type, User.IsGameCommissioner()));
        }
        if (User.IsGameCommissioner())
        { 
            ViewModel.ComboboxVisability = "Visible";
        } else
        {
            ViewModel.ComboboxVisability = "Hidden";
        }
    }

    public void BoatTypeSelected(object sender, MouseButtonEventArgs e)
    {
        
        if (User.Role == UserRole.Member)
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
                _navigationManager.Navigate(() => new SelectTimePage(_navigationManager, boatType, 0));
            }
        }
        else
        {
            var listViewItem = (ListViewItem)sender;
            var dataContext = (SelectBoatTypeBoatTypeViewModel)listViewItem.DataContext;
            if (!(_previousSelection == null))
            {
                _previousSelection.SelectionLineVisability = "Hidden";
            }
            dataContext.SelectionLineVisability = "Visible";
            _previousSelection = dataContext;
            SelectedBoatType = _boatTypeRepository.GetById(dataContext.BoatTypeId);
            ViewModel.Amount.Clear();
            for (int i = 1; i <= _boatRepository.GetAvailableByType(SelectedBoatType.BoatTypeId).Count() ; i++)
            {
                ViewModel.Amount.Add(i);
            }
            AmountComboBox.SelectedIndex = 0;
            TimeBLockButton.IsEnabled = true;
            //_navigationManager.Navigate(() => new SelectTimePage(_navigationManager, SelectedBoatType, 0));
        }
    }

    private void ComboBoxAmount_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        if (comboBox.SelectedIndex == -1)
        {
        }else
        {
            AmountOfBoats = (int)comboBox.SelectedItem;
        }
    }

    private void ButtonTimeBlock_Click(object sender, RoutedEventArgs e)
    {
        _navigationManager.Navigate(() => new SelectTimePage(_navigationManager, SelectedBoatType, AmountOfBoats));
    }
}