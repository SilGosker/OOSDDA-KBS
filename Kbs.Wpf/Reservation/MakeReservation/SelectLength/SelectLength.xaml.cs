using Kbs.Business.Boat;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Data.Reservation;
using Kbs.Wpf.Boat.Index;
using Kbs.Wpf.Reservation.ViewReservation;
using Kbs.Wpf.User.Login;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kbs.Wpf.Reservation.MakeReservation.SelectLength
    {
    /// <summary>
    /// Interaction logic for SelectLength.xaml
    /// </summary>
    public partial class SelectLength : Page
        {
        private SelectLengthViewModel ViewModel => (SelectLengthViewModel)DataContext;
        MainWindow mainWindow;
        Tuple<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS, BoatEntity> chosenTimeAndBoat;
        public int lenghtSelected;
        public DateTime selectedStartTime = new DateTime();
        public SelectLength(MainWindow mainWindow, Tuple<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS, BoatEntity> chosenTimeAndBoat)
            {
            this.mainWindow = mainWindow;
            this.chosenTimeAndBoat = chosenTimeAndBoat;
            lenghtSelected = 30;
            InitializeComponent();
            ViewModel.MakeSelectLengthViewModelGreatAgain(MakeComboboxAvailableTimes(),chosenTimeAndBoat.Item2.Name,chosenTimeAndBoat.Item1.startTime);
            }

        private void RadioButton30_Checked(object sender, RoutedEventArgs e)
            {
            lenghtSelected = 30;
            ViewModel.AvailableStartTimes = MakeComboboxAvailableTimes();
            }

        private void RadioButton60_Checked(object sender, RoutedEventArgs e)
            {
            lenghtSelected = 60;
            ViewModel.AvailableStartTimes = MakeComboboxAvailableTimes();
            }

        private void RadioButton90_Checked(object sender, RoutedEventArgs e)
            {
            lenghtSelected = 90;
            ViewModel.AvailableStartTimes = MakeComboboxAvailableTimes();
            }

        private void RadioButton120_Checked(object sender, RoutedEventArgs e)
            {
            lenghtSelected = 120;
            ViewModel.AvailableStartTimes = MakeComboboxAvailableTimes();
            }

        private ObservableCollection<string> MakeComboboxAvailableTimes()
            {
            ObservableCollection<string> availableTimes = new ObservableCollection<string>();
            for (DateTime i = chosenTimeAndBoat.Item1.startTime; i <= chosenTimeAndBoat.Item1.endTime.AddMinutes(-lenghtSelected); i = i.AddMinutes(30))
                {
                availableTimes.Add(i.ToShortTimeString());

                }
            return availableTimes;
            }

        private void ButtonReservation_Click(object sender, RoutedEventArgs e)
            {
            ReservationEntity res = new ReservationEntity();
            res.BoatId = chosenTimeAndBoat.Item2.BoatID;
            res.Status = ReservationStatus.Active;
            res.StartTime = selectedStartTime;
            
        TimeSpan length = new TimeSpan();

            length = TimeSpan.FromMinutes(lenghtSelected);

            res.Length = length;


            UserEntity user = SessionManager.Instance.Current.User;

            res.UserId = user.UserId;

            ReservationRepository repo = new ReservationRepository();
            repo.Create(res);
            mainWindow.NavigationFrame.Content = new ViewReservationPage();
            }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {

            var comboBox = (ComboBox)sender;
            string selected = (string)comboBox.SelectedItem;
            string[] selectedtime = selected.Split(":");

            DateTime selectedDate = new DateTime();
            selectedDate = selectedDate.AddDays(chosenTimeAndBoat.Item1.startTime.DayOfYear - 2);
            selectedDate = selectedDate.AddYears(chosenTimeAndBoat.Item1.startTime.Year - 1);

            selectedDate = selectedDate.AddHours(Int32.Parse(selectedtime[0]));

            selectedDate = selectedDate.AddMinutes(Int32.Parse(selectedtime[1]));

            selectedStartTime = selectedDate;
            }
        }
    }
