﻿using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using Kbs.Wpf.Components;
using Kbs.Wpf.Reservation.ViewReservationGeneralPage;
using Kbs.Wpf.Reservation.ViewReservationSpecificPage;
using System.Windows;
using System.Windows.Controls;

namespace Kbs.Wpf.Reservation.ViewReservationSpecificPage
{
    public partial class ViewPageSpecific : Page
    {
        //private readonly IReservationRepository _reservationRepository1 = new ReservationRepository();
        private readonly IBoatTypeRepository _boatTypeRepository = new BoatTypeRepository();
        private readonly ReservationRepository _reservationRepository = new ReservationRepository();
        public ViewPageSpecificViewModel ViewModel => (ViewPageSpecificViewModel)DataContext;

        public ViewPageSpecific(int reservationId)
        {
            InitializeComponent();
            var reservation = _reservationRepository.GetById(reservationId);
            var boatType = _boatTypeRepository.GetByReservationId(reservationId);


            ViewModel.ReservationID = reservation.ReservationID;
            ViewModel.Length = reservation.Length;
            ViewModel.StartTime = reservation.StartTime;
            ViewModel.HasSteeringWheel = boatType.HasSteeringWheel;
            ViewModel.Niveau = (int)boatType.RequiredExperience;
            ViewModel.Seats = boatType.Seats;
            ViewModel.Status = reservation.Status;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public void Annuleren(object sender, RoutedEventArgs e)
        {
            var entity = _reservationRepository.GetById(ViewModel.ReservationID);

            MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result) {
                _reservationRepository.Delete(entity);
            }

        }
    }
}
