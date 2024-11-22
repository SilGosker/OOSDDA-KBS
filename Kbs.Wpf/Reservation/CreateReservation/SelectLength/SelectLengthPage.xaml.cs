using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using Kbs.Wpf.Reservation.CreateReservation.SelectTime;
using Kbs.Wpf.Reservation.ViewReservationSpecificPage;

namespace Kbs.Wpf.Reservation.CreateReservation.SelectLength;

public partial class SelectLengthPage : Page
{
    private SelectLengthViewModel ViewModel => (SelectLengthViewModel)DataContext;
    private readonly INavigationManager _navigationManager;
    private readonly BoatRepository _boatRepository = new();
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private readonly ReservationRepository _reservationRepository = new();
    private readonly ReservationTimeFactory _reservationTimeFactory;
    public SelectLengthPage(INavigationManager navigationManager, int selectedBoatId, ReservationTime selectedStartTime)
    {
        this._navigationManager = navigationManager;
        _reservationTimeFactory = new(_reservationRepository, _boatRepository);
        InitializeComponent();
        ViewModel.BoatId = selectedBoatId;
        ViewModel.SelectedStartTime = selectedStartTime.StartTime;

        foreach (var possibleReservationTime in _reservationTimeFactory.GetPossibleReservationTimes(selectedStartTime, selectedBoatId))
        {
            ViewModel.LengthOptions.Add(new SelectLengthLengthOptionViewModel(possibleReservationTime));
        }
    }

    private void Submit(object sender, RoutedEventArgs e)
    {
        if (ViewModel.SelectedLength == null)
        {
            ViewModel.LengthErrorMessage = "Een lengte moet geselecteerd worden";
            return;
        }
        
        ViewModel.LengthErrorMessage = "";

        var reservation = new ReservationEntity
        {
            BoatId = ViewModel.BoatId,
            IsForCompetition = false,
            Status = ReservationStatus.Active,
            UserId = SessionManager.Instance.Current.User.UserId,
            StartTime = ViewModel.SelectedStartTime,
            Length = ViewModel.SelectedLength.Length
        };

        _reservationRepository.Create(reservation);
        _navigationManager.Navigate(() => new ViewPageSpecific(reservation.ReservationId, _navigationManager));
    }

    private void LengthSelectionChanged(object sender, RoutedEventArgs e)
    {
        var radio = (RadioButton)sender;
        var dataContext = (SelectLengthLengthOptionViewModel)radio.DataContext;
        ViewModel.SelectedLength = dataContext;
    }

    private void PreviousStep(object sender, RoutedEventArgs e)
    {
        var boatType = _boatTypeRepository.GetByBoatId(ViewModel.BoatId);
        var boat = _boatRepository.GetById(ViewModel.BoatId);

        _navigationManager.Navigate(() => new SelectTimePage(_navigationManager, boatType, boat));
    }
}