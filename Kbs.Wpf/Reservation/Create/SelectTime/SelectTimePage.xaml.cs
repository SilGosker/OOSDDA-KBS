using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Extentions;
using Kbs.Business.Game;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.Reservation;
using Kbs.Wpf.Components;
using Kbs.Wpf.Reservation.Create.SelectBoatType;
using Kbs.Wpf.Reservation.Create.SelectLength;

namespace Kbs.Wpf.Reservation.Create.SelectTime;

[HasRole(UserRole.Member)]
[HasRole(UserRole.GameCommissioner)]
[HighlightFor(typeof(ReservationTime))]
public partial class SelectTimePage : Page
{
    private readonly INavigationManager _navigationManager;
    private readonly ReservationMaker _maker = new(new ReservationRepository());
    private BoatEntity _boatSelected;
    private List<BoatEntity> _boatsSelected;
    private int _daysFromToday;
    private readonly BoatRepository _boatRepository = new();
    private SelectTimeViewModel ViewModel => (SelectTimeViewModel)DataContext;
    private readonly List<double> _checklist = new();
    private readonly GameEntity _game;

    public SelectTimePage(INavigationManager navigationManager, BoatTypeEntity boatType, GameEntity game) : this(navigationManager, boatType)
    {
        _game = game;
        ViewModel.GameCreateMessage = "U reserveert boten voor Wedstrijd #" + _game.GameId;
    }

    public SelectTimePage(INavigationManager navigationManager, BoatTypeEntity boatType)
    {
        _checklist.Add(256);
        double gridRow = 6;
        for (int k = 0; k < 27; k++)
        {
            _checklist.Add(gridRow);
            gridRow += 0.5;
        }

        InitializeComponent();
        _navigationManager = navigationManager;

        var boats = _boatRepository.GetAvailableByType(boatType.BoatTypeId);
        foreach (BoatEntity boat in boats)
        {
            ViewModel.Boats.Add(new SelectTimeBoatViewModel(boat));
        }

        ViewModel.GameCommissionerComboBoxVisibility = Visibility.Hidden;
        ViewModel.MemberComboBoxVisibility = Visibility.Hidden;
        ViewModel.NoBoatsSelectedError = "";

        if (SessionManager.Instance.Current.User.IsMember())
        {
            ViewModel.MemberComboBoxVisibility = Visibility.Visible;
        }
        else if (SessionManager.Instance.Current.User.IsGameCommissioner())
        {
            ViewModel.GameCommissionerComboBoxVisibility = Visibility.Visible;
        }

        RefreshCalendar();
    }

    private void TimeSlotButton_Click(object sender, RoutedEventArgs e)
    {
        if (SessionManager.Instance.Current.User.IsMember())
        {

            Button send = (Button)sender;
            Tuple<ReservationTime, List<BoatEntity>> chosenTimeAndBoat =
                (Tuple<ReservationTime, List<BoatEntity>>)send.Tag;
            _navigationManager.Navigate(() => new SelectLength.SelectLengthPage(_navigationManager, chosenTimeAndBoat));

        }
        else if (SessionManager.Instance.Current.User.IsGameCommissioner())
        {
            if (_boatsSelected.Count > 0)
            {
                Button send = (Button)sender;
                Tuple<ReservationTime, List<BoatEntity>> chosenTimeAndBoat =
                    (Tuple<ReservationTime, List<BoatEntity>>)send.Tag;

                if (_game != null)
                {
                    _navigationManager.Navigate(() => new SelectLengthPage(_navigationManager, chosenTimeAndBoat, _game));
                }
                else
                {
                    _navigationManager.Navigate(() => new SelectLengthPage(_navigationManager, chosenTimeAndBoat));
                }
            }
            else
            {
                ViewModel.NoBoatsSelectedError = "Selecteer een of meerdere boten";
            }
        } else
        {

            Button send = (Button)sender;
            Tuple<ReservationTime, List<BoatEntity>> chosenTimeAndBoat =
                (Tuple<ReservationTime, List<BoatEntity>>)send.Tag;
            if (_game != null)
            {
                _navigationManager.Navigate(() => new SelectLengthPage(_navigationManager, chosenTimeAndBoat, _game));
            }
            else
            {
                _navigationManager.Navigate(() => new SelectLengthPage(_navigationManager, chosenTimeAndBoat));
            }
        }
    }

    private void ComboBoxBoats_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        var selected = (SelectTimeBoatViewModel)comboBox.SelectedItem;
        _boatSelected = selected.Boat;
        RefreshCalendar();
    }

    private void ListBoxBoats_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        var selected = (SelectTimeBoatViewModel)comboBox.SelectedItem;
        _boatsSelected = selected.SelectedBoats;
        RefreshCalendar();
    }

    private void RefreshCalendar()
    {
        ViewModel.ThisWeek.Clear();
        ViewModel.DaysOfWeek.Clear();
        Buttons.Children.Clear();
        var countVar = 0;

        for (int i = _daysFromToday; countVar < 7; i++)
        {
            DateTime weekday = new DateTime();
            weekday = DateTime.Now;
            weekday = weekday.AddDays(i);
            ViewModel.ThisWeek.Add(weekday);
            ViewModel.DaysOfWeek.Add(weekday.ConvertDateTimeToDutchDayOfWeekString());
            
            if (SessionManager.Instance.Current.User.IsMember())
            {
                foreach (ReservationTime j in _maker.MakeReservableTimes(weekday, _boatSelected))
                {
                    var chosenTimeAndBoat =
                        new Tuple<ReservationTime, List<BoatEntity>>(j, new List<BoatEntity> { _boatSelected });
                    if (j.Length <= 0) continue;
                    var button = new Button()
                    {
                        Width = 150,
                        Height = (j.Length * 34),
                        Tag = chosenTimeAndBoat,
                        Foreground = (Brush)TryFindResource("ColorPrimaryText"),
                        Background = (Brush)TryFindResource("ColorSecondaryBackground"),
                        BorderBrush = (Brush)TryFindResource("ColorCaretBrush"),
                        FontSize = 28,
                        Content = "Reserveer"
                    };
                    button.Tag = chosenTimeAndBoat;
                    button.Click += TimeSlotButton_Click;

                    Buttons.Children.Add(button);

                    double compare = 0;

                    if (j.StartTime.Minute == 30)
                    {
                        compare += 0.5;
                    }

                    compare += j.StartTime.Hour;
                    var rowspan = Convert.ToInt32(j.Length + j.Length);


                    Grid.SetRow(button, _checklist.IndexOf(compare));
                    Grid.SetColumn(button, countVar);
                    Grid.SetRowSpan(button, rowspan);
                }
            }
            else if (SessionManager.Instance.Current.User.IsGameCommissioner())
            {
                _boatsSelected = ViewModel.Boats
                    .Where(e => e.IsSelected)
                    .Select(e => e.Boat)
                    .ToList();
                foreach (var j in _maker.MakeReservableTimes(weekday, ViewModel.Boats
                             .Where(e => e.IsSelected)
                             .Select(e => e.Boat)
                             .ToList()))
                {
                    var chosenTimeAndBoats = new Tuple<ReservationTime, List<BoatEntity>>(j, _boatsSelected);
                    if (j.Length <= 0) continue;
                    var button = new Button()
                    {
                        Width = 150,
                        Height = (j.Length * 34),
                        Tag = chosenTimeAndBoats,
                        Foreground = (Brush)TryFindResource("ColorPrimaryText"),
                        Background = (Brush)TryFindResource("ColorSecondaryBackground"),
                        BorderBrush = (Brush)TryFindResource("ColorCaretBrush"),
                        FontSize = 28,
                        Content = "Reserveer"
                    };
                    button.Tag = chosenTimeAndBoats;
                    button.Click += TimeSlotButton_Click;

                    Buttons.Children.Add(button);

                    double compare = 0;

                    if (j.StartTime.Minute == 30)
                    {
                        compare += 0.5;
                    }

                    compare += j.StartTime.Hour;
                    var rowspan = Convert.ToInt32(j.Length + j.Length);

                    Grid.SetRow(button, _checklist.IndexOf(compare));
                    Grid.SetColumn(button, countVar);
                    Grid.SetRowSpan(button, rowspan);
                }
            }

            countVar++;
        }
    }

    private void PreviousStep(object sender, RoutedEventArgs e)
    {
        if (_game != null)
        {
            _navigationManager.Navigate(() => new SelectBoatTypePage(_navigationManager, _game)); 
        }
        else
        {
            _navigationManager.Navigate(() => new SelectBoatTypePage(_navigationManager));
        }
    }

    private void NextWeekButton_Click(object sender, RoutedEventArgs e)
    {
        if (_daysFromToday > 7 && SessionManager.Instance.Current.User.IsMember()) return;
        _daysFromToday += 7;
        RefreshCalendar();
    }

    private void BackWeekButton_Click(object sender, RoutedEventArgs e)
    {
        if (_daysFromToday < 7) return;
        _daysFromToday -= 7;
        RefreshCalendar();
    }

    private void GameCommissionerComboBoxChanged(object sender, RoutedEventArgs e)
    {
        RefreshCalendar();
    }
}