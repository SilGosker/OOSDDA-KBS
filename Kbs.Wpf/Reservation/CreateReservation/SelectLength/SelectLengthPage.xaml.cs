using Kbs.Business.Boat;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using Kbs.Wpf.Reservation.CreateReservation.SelectTime;
using Kbs.Wpf.Reservation.ViewReservationGeneralPage;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Kbs.Wpf.Reservation.CreateReservation.SelectLength
{
    /// <summary>
    /// Interaction logic for SelectLength.xaml
    /// </summary>
    public partial class SelectLengthPage : Page
    {
        private readonly INavigationManager _navigationManager;
        private readonly BoatTypeRepository _boatTypeRepository = new();
        private readonly ReservationRepository _reservationRepository = new();
        private ComboBox _starttimecombobox;
        private SelectLengthViewModel ViewModel => (SelectLengthViewModel)DataContext;
        Tuple<ReservationTime, BoatEntity> chosenTimeAndBoat;
        public TimeSpan lenghtSelected = TimeSpan.FromMinutes(30);
        public DateTime selectedStartTime = new DateTime();
        public SelectLengthPage(INavigationManager navigationManager, Tuple<ReservationTime, BoatEntity> chosenTimeAndBoat)
        {
            _navigationManager = navigationManager;
            this.chosenTimeAndBoat = chosenTimeAndBoat;
            InitializeComponent();
            ViewModel.MakeSelectLengthViewModel(MakeComboboxAvailableTimes(), chosenTimeAndBoat.Item2.Name, chosenTimeAndBoat.Item1.StartTime);

            double unCheckablebuttonLength = 0.5;
            for (int i = 30; i <= 120; i += 30)
            {
                TimeSpan length = TimeSpan.FromMinutes(i);
                if (i == 30)
                {
                    ViewModel.RadioButtons.Add(new SelectLengthLengthViewModel(true, length, true));
                }
                else if ((chosenTimeAndBoat.Item1.Length < unCheckablebuttonLength))
                {
                    ViewModel.RadioButtons.Add(new SelectLengthLengthViewModel(false, length, false));
                }
                else
                {
                    ViewModel.RadioButtons.Add(new SelectLengthLengthViewModel(true, length, false));
                }

                unCheckablebuttonLength += 0.5;
            }
        }

        private ObservableCollection<string> MakeComboboxAvailableTimes()
        {
            ObservableCollection<string> availableTimes = new ObservableCollection<string>();
            for (DateTime i = chosenTimeAndBoat.Item1.StartTime; i <= chosenTimeAndBoat.Item1.EndTime.Subtract(lenghtSelected); i = i.AddMinutes(30))
            {
                availableTimes.Add(i.ToString("HH:mm"));

            }
            return availableTimes;
        }

        private void ButtonReservation_Click(object sender, RoutedEventArgs e)
        {
            ReservationEntity res = new ReservationEntity();
            res.BoatId = chosenTimeAndBoat.Item2.BoatId;
            res.Status = ReservationStatus.Active;
            res.StartTime = selectedStartTime;

            res.Length = lenghtSelected;

            UserEntity user = SessionManager.Instance.Current.User;

            res.UserId = user.UserId;

            _reservationRepository.Create(res);

            _navigationManager.Navigate(() => new ViewReservationPage(_navigationManager));
        }

        private void ComboBoxStartTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            _starttimecombobox = (ComboBox)sender;
            string selected = (string)_starttimecombobox.SelectedItem;
            if (selected.IsNullOrEmpty())
            {
                return;
            }
            var timespan = TimeSpan.Parse(selected);

            DateTime selectedDate = new DateTime();
            selectedDate = selectedDate.AddDays(chosenTimeAndBoat.Item1.StartTime.DayOfYear - 2);
            selectedDate = selectedDate.AddYears(chosenTimeAndBoat.Item1.StartTime.Year - 1);

            selectedDate = selectedDate.Add(timespan);

            selectedStartTime = selectedDate;
        }
        private void PreviousStep(object sender, RoutedEventArgs e)
        {
            var boatType = _boatTypeRepository.GetByBoatId(chosenTimeAndBoat.Item2.BoatId);

            _navigationManager.Navigate(() => new SelectTimePage(_navigationManager, boatType));
        }

        private void RadioButtLength_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            SelectLengthLengthViewModel dataContext = (SelectLengthLengthViewModel)button.DataContext;
            lenghtSelected = dataContext.Length;
            ViewModel.AvailableStartTimes = MakeComboboxAvailableTimes();
            _starttimecombobox.SelectedIndex = 0;
        }
    }

}
