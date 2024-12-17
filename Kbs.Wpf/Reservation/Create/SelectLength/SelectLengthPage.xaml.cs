using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Boat;
using Kbs.Business.Game;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using Kbs.Wpf.Game.Read.Details;
using Kbs.Wpf.Reservation.Create.SelectTime;
using Kbs.Wpf.Reservation.Read.Index;
using Microsoft.IdentityModel.Tokens;

namespace Kbs.Wpf.Reservation.Create.SelectLength;

[HasRole(UserRole.Member)]
[HasRole(UserRole.GameCommissioner)]
public partial class SelectLengthPage : Page
{
    private readonly INavigationManager _navigationManager;
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private readonly ReservationRepository _reservationRepository = new();
    private ComboBox _starTimeComboBox;
    private double _unCheckableButtonLength;
    private SelectLengthViewModel ViewModel => (SelectLengthViewModel)DataContext;
    Tuple<ReservationTime, List<BoatEntity>> _chosenTimeAndBoat;
    public TimeSpan LengthSelected = TimeSpan.FromMinutes(30);
    public DateTime SelectedStartTime;
    private readonly GameEntity _game;
    public SelectLengthPage(INavigationManager navigationManager,
        Tuple<ReservationTime, List<BoatEntity>> chosenTimeAndBoat, GameEntity game) : this(navigationManager, chosenTimeAndBoat)
    {
        _game = game;
        ViewModel.GameCreateMessage = "U reserveert boten voor Wedstrijd #" + _game.GameId;
    }
    public SelectLengthPage(INavigationManager navigationManager, Tuple<ReservationTime, List<BoatEntity>> chosenTimeAndBoat)
    {
        int reservationLengthIncrementMinutes;
        int maxReservationLength;
        _navigationManager = navigationManager;
        _chosenTimeAndBoat = chosenTimeAndBoat;
        InitializeComponent();
        string boatName;
        if (chosenTimeAndBoat.Item2.Count == 1)
        {
            boatName = chosenTimeAndBoat.Item2[0].Name;
        }
        else
        {
            boatName = chosenTimeAndBoat.Item2.Count + " boten";
        }

        ViewModel.MakeSelectLengthViewModel(MakeComboboxAvailableTimes(), boatName,
            chosenTimeAndBoat.Item1.StartTime);

        if (SessionManager.Instance.Current.User.IsMember())
        {
            _unCheckableButtonLength = 0.5;
            reservationLengthIncrementMinutes = 30;
            maxReservationLength = 120;
        }
        else
        {
            _unCheckableButtonLength = 1;
            reservationLengthIncrementMinutes = 60;
            maxReservationLength = 480;
            ViewModel.RadioButtons.Add(new SelectLengthLengthViewModel(true, TimeSpan.FromMinutes(30), true));
        }

        double unCheckableButtonLength = _unCheckableButtonLength;

        for (int i = reservationLengthIncrementMinutes;
             i <= maxReservationLength;
             i += reservationLengthIncrementMinutes)
        {
            TimeSpan length = TimeSpan.FromMinutes(i);
            if (i == 30)
            {
                ViewModel.RadioButtons.Add(new SelectLengthLengthViewModel(true, length, true));
            }
            else if ((chosenTimeAndBoat.Item1.Length < unCheckableButtonLength))
            {
                ViewModel.RadioButtons.Add(new SelectLengthLengthViewModel(false, length, false));
            }
            else
            {
                ViewModel.RadioButtons.Add(new SelectLengthLengthViewModel(true, length, false));
            }

            unCheckableButtonLength += _unCheckableButtonLength;
        }
    }

    private ObservableCollection<string> MakeComboboxAvailableTimes()
    {
        ObservableCollection<string> availableTimes = new ObservableCollection<string>();
        for (DateTime i = _chosenTimeAndBoat.Item1.StartTime;
             i <= _chosenTimeAndBoat.Item1.EndTime.Subtract(LengthSelected);
             i = i.AddMinutes(30))
        {
            availableTimes.Add(i.ToString("HH:mm"));
        }

        return availableTimes;
    }

    private void ButtonReservation_Click(object sender, RoutedEventArgs e)
    {
        var validator = new ReservationValidator();
        foreach (BoatEntity boat in _chosenTimeAndBoat.Item2)
        {
            ReservationEntity res = new();
            res.BoatId = boat.BoatId;
            res.Status = ReservationStatus.Active;
            res.StartTime = SelectedStartTime;
            res.GameId = _game?.GameId;
            res.Length = LengthSelected;

            UserEntity user = SessionManager.Instance.Current.User;

            res.UserId = user.UserId;
            if (validator.IsWithinDaylightHours(res))
            {
                _reservationRepository.Create(res);
            }
        }

        if (_game == null)
        {
            _navigationManager.Navigate(() => new ReadIndexReservationPage(_navigationManager));
        }
        else
        {
            _navigationManager.Navigate(() => new ReadDetailsGamePage(_navigationManager, _game.GameId));
        }
    }

    private void ComboBoxStartTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _starTimeComboBox = (ComboBox)sender;
        string selected = (string)_starTimeComboBox.SelectedItem;
        if (selected.IsNullOrEmpty())
        {
            return;
        }

        var timespan = TimeSpan.Parse(selected);

        DateTime selectedDate = new DateTime();
        //2025 is speciel in its days an needs a diffrent - value
        if (_chosenTimeAndBoat.Item1.StartTime.Year == 2025)
        {
            selectedDate = selectedDate.AddDays(_chosenTimeAndBoat.Item1.StartTime.AddDays(-1).DayOfYear);
        }
        else
        {
            selectedDate = selectedDate.AddDays(_chosenTimeAndBoat.Item1.StartTime.AddDays(-2).DayOfYear);
        }

        //if the year is 2 its 1 to high so needs a diffrent value
        if (selectedDate.Year == 2)
        {
            selectedDate = selectedDate.AddYears(_chosenTimeAndBoat.Item1.StartTime.AddYears(-2).Year);
        }
        else
        {
            selectedDate = selectedDate.AddYears(_chosenTimeAndBoat.Item1.StartTime.AddYears(-1).Year);
        }

        selectedDate = selectedDate.Add(timespan);

        SelectedStartTime = selectedDate;
    }

    private void PreviousStep(object sender, RoutedEventArgs e)
    {
        var boatType = _boatTypeRepository.GetByBoatId(_chosenTimeAndBoat.Item2[0].BoatId);
        if (_game != null)
        {
            _navigationManager.Navigate(() => new SelectTimePage(_navigationManager, boatType, _game));
        }
        else
        {
            _navigationManager.Navigate(() => new SelectTimePage(_navigationManager, boatType));
        }
    }

    private void RadioButtLength_Checked(object sender, RoutedEventArgs e)
    {
        RadioButton button = (RadioButton)sender;
        SelectLengthLengthViewModel dataContext = (SelectLengthLengthViewModel)button.DataContext;
        LengthSelected = dataContext.Length;
        ViewModel.AvailableStartTimes = MakeComboboxAvailableTimes();
        _starTimeComboBox.SelectedIndex = 0;
    }
}