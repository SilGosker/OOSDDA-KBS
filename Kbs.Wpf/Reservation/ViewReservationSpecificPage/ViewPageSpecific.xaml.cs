﻿using Kbs.Business.Reservation;
using Kbs.Data.Reservation;
using System.Windows;
using System.Windows.Controls;

namespace Kbs.Wpf.Reservation.ViewReservationSpecificPage
{
    public partial class ViewPageSpecific : Page
    {
        private readonly IReservationRepository _reservationRepository = new ReservationRepository();
        public ViewPageSpecificViewModel ViewModel => (ViewPageSpecificViewModel)DataContext;
        public ViewPageSpecific(int reservationId)
        {
            InitializeComponent();

            var reservation = _reservationRepository.GetById(reservationId);
            ViewModel.Tijdsduur = reservation.Duration;
            ViewModel.Tijdsstip = reservation.StartTime;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public void Annuleren(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }
}
