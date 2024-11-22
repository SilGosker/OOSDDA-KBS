using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Data.Boat;
using Kbs.Data.Reservation;
using System.Windows.Controls;

namespace Kbs.Wpf.Reservation.MakeReservation.SelectTime;

public partial class SelectTimePage : Page
{
    private readonly INavigationManager _navigationManager;
    private readonly BoatRepository _boatRepository = new();
    private readonly ReservationRepository _reservationRepository = new();
    private readonly ReservationTimeFactory _reservationTimeFactory;
    private readonly TimeSpan _morning = new(9, 0, 0);
    private readonly TimeSpan _evening = new(17, 0, 0);
    private SelectTimeViewModel ViewModel => (SelectTimeViewModel)DataContext;

    public SelectTimePage(INavigationManager navigationManager, BoatTypeEntity boatType)
    {
        _navigationManager = navigationManager;
        _reservationTimeFactory = new(_reservationRepository, _boatRepository);
        InitializeComponent();

        var boats = _boatRepository.GetAvailableByType(boatType.BoatTypeId);
        ViewModel.SelectedBoatId = boats[0].BoatId;

        foreach (BoatEntity boat in boats)
        {
            ViewModel.Boats.Add(new SelectTimeBoatViewModel(boat));
        }

        foreach (var day in _reservationTimeFactory.GetDaysOfWeek(ViewModel.SelectedDate))
        {
            ViewModel.CurrentWeek.Add(new SelectTimeDayOfWeek(day));
        }

        foreach (var reservationTime in _reservationTimeFactory.GetPossibleReservationTimes(ViewModel.SelectedDate,
                     _morning,
                     _evening))
        {
            ViewModel.PossibleTimes.Add(new SelectTimePossibleTimeViewModel(reservationTime));
        }

        UpdateCalendar();
    }

    private void UpdateCalendar()
    {
        ViewModel.CurrentWeek.Clear();

        foreach (DateTime dateTime in _reservationTimeFactory.GetDaysOfWeek(ViewModel.SelectedDate))
        {
            var dayViewModel = new SelectTimeDayOfWeek(dateTime);
            var times = _reservationTimeFactory.GetPossibleReservationTimes(_morning, _evening, dateTime, ViewModel.SelectedBoatId);
            foreach (ReservationTime time in times)
            {
                dayViewModel.Times.Add(new SelectTimePossibleTimeViewModel(time));
            }
            ViewModel.CurrentWeek.Add(dayViewModel);
        }
    }
   
    private void ComboBoxBoats_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBox comboBox = (ComboBox)sender;
        SelectTimeBoatViewModel selectedBoat = (SelectTimeBoatViewModel)comboBox.SelectedItem;
        ViewModel.SelectedBoatId = selectedBoat.BoatId;
        UpdateCalendar();
    }
}