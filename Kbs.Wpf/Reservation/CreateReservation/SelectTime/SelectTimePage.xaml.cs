using System.Windows.Controls;
using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Data.Boat;
using Kbs.Data.Reservation;
using Kbs.Wpf.Reservation.CreateReservation.SelectBoatType;

namespace Kbs.Wpf.Reservation.CreateReservation.SelectTime;

public partial class SelectTimePage : Page
{
    private readonly INavigationManager _navigationManager;
    private readonly BoatRepository _boatRepository = new();
    private readonly ReservationRepository _reservationRepository = new();
    private readonly ReservationTimeFactory _reservationTimeFactory;
    private readonly TimeSpan _morning = new(9, 0, 0);
    private readonly TimeSpan _evening = new(17, 0, 0);
    private SelectTimeViewModel ViewModel => (SelectTimeViewModel)DataContext;

    public SelectTimePage(INavigationManager navigationManager, BoatTypeEntity boatType, BoatEntity selectedBoat = null)
    {
        _navigationManager = navigationManager;
        _reservationTimeFactory = new(_reservationRepository, _boatRepository);
        InitializeComponent();

        var boats = _boatRepository.GetAvailableByType(boatType.BoatTypeId);

        foreach (BoatEntity boat in boats)
        {
            ViewModel.Boats.Add(new SelectTimeBoatViewModel(boat));
        }

        if (selectedBoat == null)
        {
            ViewModel.SelectedBoat = ViewModel.Boats.First();
        }
        else
        {
            ViewModel.SelectedBoat = ViewModel.Boats.First(b => b.BoatId == selectedBoat.BoatId);
        }

        foreach (var day in _reservationTimeFactory.GetDaysOfWeek(ViewModel.SelectedDate))
        {
            ViewModel.CurrentWeek.Add(new SelectTimeDayOfWeek(day));
        }

        foreach (var reservationTime in _reservationTimeFactory.GetPossibleReservationTimes(ViewModel.SelectedDate,
                     _morning,
                     _evening))
        {
            ViewModel.PossibleTimes.Add(new SelectTimePossibleTimeViewModel(reservationTime, TimeSelected));
        }

        UpdateCalendar();
    }

    private void UpdateCalendar()
    {
        ViewModel.CurrentWeek.Clear();

        foreach (DateTime dateTime in _reservationTimeFactory.GetDaysOfWeek(ViewModel.SelectedDate))
        {
            var dayViewModel = new SelectTimeDayOfWeek(dateTime);
            var times = _reservationTimeFactory.GetPossibleReservationTimes(_morning, _evening, dateTime, ViewModel.SelectedBoat.BoatId);
            foreach (ReservationTime time in times)
            {
                dayViewModel.Times.Add(new SelectTimePossibleTimeViewModel(time, TimeSelected));
            }
            ViewModel.CurrentWeek.Add(dayViewModel);
        }
    }
    
    private void BoatSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateCalendar();
    }

    private void TimeSelected(SelectTimePossibleTimeViewModel selectedTime)
    {
        _navigationManager.Navigate(() => new SelectLength.SelectLengthPage(_navigationManager, ViewModel.SelectedBoat.BoatId, selectedTime.ReservationTime));
    }

    private void PreviousStep(object sender, System.Windows.RoutedEventArgs e)
    {
        _navigationManager.Navigate(() => new SelectBoatTypePage(_navigationManager));
    }
}