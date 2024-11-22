using Kbs.Business.Boat;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Data.Reservation;
using Kbs.Wpf.Reservation.ViewReservationGeneralPage;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Kbs.Wpf.Reservation.MakeReservation.SelectLength;

public partial class SelectLength : Page
{
    private SelectLengthViewModel ViewModel => (SelectLengthViewModel)DataContext;
    private readonly INavigationManager _navigationManager;
    readonly Tuple<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS, BoatEntity> _chosenTimeAndBoat;
    private int _lenghtSelected;
    private DateTime _selectedStartTime;
    public SelectLength(Tuple<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS, BoatEntity> chosenTimeAndBoat, INavigationManager navigationManager)
    {
        this._chosenTimeAndBoat = chosenTimeAndBoat;
        _navigationManager = navigationManager;
        _lenghtSelected = 30;
        InitializeComponent();
        ViewModel.MakeSelectLengthViewModelGreatAgain(MakeComboboxAvailableTimes(),chosenTimeAndBoat.Item2.Name,chosenTimeAndBoat.Item1.startTime);
    }

    private void RadioButton30_Checked(object sender, RoutedEventArgs e)
    {
        _lenghtSelected = 30;
        ViewModel.AvailableStartTimes = MakeComboboxAvailableTimes();
    }

    private void RadioButton60_Checked(object sender, RoutedEventArgs e)
    {
        _lenghtSelected = 60;
        ViewModel.AvailableStartTimes = MakeComboboxAvailableTimes();
    }

    private void RadioButton90_Checked(object sender, RoutedEventArgs e)
    {
        _lenghtSelected = 90;
        ViewModel.AvailableStartTimes = MakeComboboxAvailableTimes();
    }

    private void RadioButton120_Checked(object sender, RoutedEventArgs e)
    {
        _lenghtSelected = 120;
        ViewModel.AvailableStartTimes = MakeComboboxAvailableTimes();
    }

    private ObservableCollection<string> MakeComboboxAvailableTimes()
    {
        ObservableCollection<string> availableTimes = new ObservableCollection<string>();
        for (DateTime i = _chosenTimeAndBoat.Item1.startTime; i <= _chosenTimeAndBoat.Item1.endTime.AddMinutes(-_lenghtSelected); i = i.AddMinutes(30))
        {
            availableTimes.Add(i.ToShortTimeString());

        }
        return availableTimes;
    }

    private void ButtonReservation_Click(object sender, RoutedEventArgs e)
    {
        ReservationEntity res = new ReservationEntity();
        res.BoatId = _chosenTimeAndBoat.Item2.BoatId;
        res.Status = ReservationStatus.Active;
        res.StartTime = _selectedStartTime;
            
        TimeSpan length = new TimeSpan();

        length = TimeSpan.FromMinutes(_lenghtSelected);

        res.Length = length;


        UserEntity user = SessionManager.Instance.Current.User;

        res.UserId = user.UserId;

        ReservationRepository repo = new ReservationRepository();
        repo.Create(res);
        _navigationManager.Navigate(() => new ViewReservationPage(_navigationManager));
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        string selected = (string)comboBox.SelectedItem;
        string[] selectedtime = selected.Split(":");

        DateTime selectedDate = new DateTime();
        selectedDate = selectedDate.AddDays(_chosenTimeAndBoat.Item1.startTime.DayOfYear - 2);
        selectedDate = selectedDate.AddYears(_chosenTimeAndBoat.Item1.startTime.Year - 1);

        selectedDate = selectedDate.AddHours(Int32.Parse(selectedtime[0]));

        selectedDate = selectedDate.AddMinutes(Int32.Parse(selectedtime[1]));

        _selectedStartTime = selectedDate;
    }
}