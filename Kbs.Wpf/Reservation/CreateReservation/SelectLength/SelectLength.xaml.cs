﻿using Kbs.Business.Boat;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using Kbs.Wpf.Boat.Index;
using Kbs.Wpf.Reservation.CreateReservation.SelectBoatType;
using Kbs.Wpf.Reservation.CreateReservation.SelectTime;
using Kbs.Wpf.Reservation.ViewReservationGeneralPage;
using Kbs.Wpf.User;
using Microsoft.IdentityModel.Tokens;
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

namespace Kbs.Wpf.Reservation.CreateReservation.SelectLength
    {
    /// <summary>
    /// Interaction logic for SelectLength.xaml
    /// </summary>
    public partial class SelectLength : Page
        {
        private readonly INavigationManager _navigationManager;
        private readonly BoatRepository _boatRepository = new();
        private readonly BoatTypeRepository _boatTypeRepository = new();
        private readonly ReservationRepository _reservationRepository = new();
        private SelectLengthViewModel ViewModel => (SelectLengthViewModel)DataContext;
        Tuple<ReservationTime, BoatEntity> chosenTimeAndBoat;
        public TimeSpan lenghtSelected = TimeSpan.FromMinutes(30);
        public DateTime selectedStartTime = new DateTime();
        public SelectLength(INavigationManager navigationManager, Tuple<ReservationTime, BoatEntity> chosenTimeAndBoat)
            {
            _navigationManager = navigationManager;
            this.chosenTimeAndBoat = chosenTimeAndBoat;
            InitializeComponent();
            ViewModel.MakeSelectLengthViewModel(MakeComboboxAvailableTimes(),chosenTimeAndBoat.Item2.Name,chosenTimeAndBoat.Item1.StartTime);
            int unCheckablebuttons = 30;
            double unCheckablebuttonLength = 0.5;
            for (int i = 30; i <= 120; i += 30) 
            {
                TimeSpan length = TimeSpan.FromMinutes(i);
                if (i == 30)
                {
                    ViewModel.RadioButtons.Add(new SelectLengthLengthViewModel(true, length, true));
                }
                else if ((i == unCheckablebuttons) && (chosenTimeAndBoat.Item1.Length <= unCheckablebuttonLength))
                {
                    ViewModel.RadioButtons.Add(new SelectLengthLengthViewModel(false, length, false));
                }
                else
                {
                    ViewModel.RadioButtons.Add(new SelectLengthLengthViewModel(true, length, false));
                }
                unCheckablebuttons += 30;
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {

            var comboBox = (ComboBox)sender;
            string selected = (string)comboBox.SelectedItem;
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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            SelectLengthLengthViewModel dataContext = (SelectLengthLengthViewModel)button.DataContext;
            lenghtSelected = dataContext.length;
            ViewModel.AvailableStartTimes = MakeComboboxAvailableTimes();
        }
    }

    }
