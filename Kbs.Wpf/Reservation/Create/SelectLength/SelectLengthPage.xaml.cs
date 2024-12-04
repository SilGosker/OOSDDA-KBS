using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Boat;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using Kbs.Wpf.Reservation.Create.SelectTime;
using Kbs.Wpf.Reservation.Read.Index;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace Kbs.Wpf.Reservation.Create.SelectLength
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
        private double _unCheckablebuttonLength;
        private int _reservationLengthIncrementMinutes;
        private int _maxReservationLength;
        public UserEntity User = SessionManager.Instance.Current.User;
        private SelectLengthViewModel ViewModel => (SelectLengthViewModel)DataContext;
        Tuple<ReservationTime, List<BoatEntity>> chosenTimeAndBoat;
        public TimeSpan lenghtSelected = TimeSpan.FromMinutes(30);
        public DateTime selectedStartTime = new DateTime();
        public SelectLengthPage(INavigationManager navigationManager, Tuple<ReservationTime, List<BoatEntity>> chosenTimeAndBoat)
        {
            _navigationManager = navigationManager;
            this.chosenTimeAndBoat = chosenTimeAndBoat;
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
            ViewModel.MakeSelectLengthViewModel(MakeComboboxAvailableTimes(), boatName, chosenTimeAndBoat.Item1.StartTime);

            if (SessionManager.Instance.Current.User.IsMember())
            {
                _unCheckablebuttonLength = 0.5;
                _reservationLengthIncrementMinutes = 30;
                _maxReservationLength = 120;
            } else
            {
                _unCheckablebuttonLength = 1;
                _reservationLengthIncrementMinutes = 60;
                _maxReservationLength = 480;
                ViewModel.RadioButtons.Add(new SelectLengthLengthViewModel(true, TimeSpan.FromMinutes(30), true));
            }

            double unCheckableButtonLength = _unCheckablebuttonLength;

            for (int i = _reservationLengthIncrementMinutes; i <= _maxReservationLength; i += _reservationLengthIncrementMinutes)
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

                unCheckableButtonLength += _unCheckablebuttonLength;
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
            foreach (BoatEntity boat in chosenTimeAndBoat.Item2)
            {
                ReservationEntity res = new ReservationEntity();
                res.BoatId = boat.BoatId;
                res.Status = ReservationStatus.Active;
                res.StartTime = selectedStartTime;

                res.Length = lenghtSelected;

                UserEntity user = SessionManager.Instance.Current.User;

                res.UserId = user.UserId;

                _reservationRepository.Create(res);
            }

            _navigationManager.Navigate(() => new ReadIndexReservationPage(_navigationManager));
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
            if (chosenTimeAndBoat.Item1.StartTime.Year == 2025)
            {
                selectedDate = selectedDate.AddDays(chosenTimeAndBoat.Item1.StartTime.AddDays(-1).DayOfYear);
                
            }
            else
            {
                selectedDate = selectedDate.AddDays(chosenTimeAndBoat.Item1.StartTime.AddDays(-2).DayOfYear);
            }
            
            
            if (selectedDate.Year == 2)
            {
                selectedDate = selectedDate.AddYears(chosenTimeAndBoat.Item1.StartTime.AddYears(-2).Year);
            }else
            {
                selectedDate = selectedDate.AddYears(chosenTimeAndBoat.Item1.StartTime.AddYears(-1).Year);
            }

            selectedDate = selectedDate.Add(timespan);

            selectedStartTime = selectedDate;
        }
        private void PreviousStep(object sender, RoutedEventArgs e)
        {
            var boatType = _boatTypeRepository.GetByBoatId(chosenTimeAndBoat.Item2[0].BoatId);

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
