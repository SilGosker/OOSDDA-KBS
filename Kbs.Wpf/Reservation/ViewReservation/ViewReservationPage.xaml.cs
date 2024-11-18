using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Wpf.Components;
using Kbs.Wpf.User.Login;
using System;
using System.Collections.Generic;
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

namespace Kbs.Wpf.Reservation.ViewReservation
{
    /// <summary>
    /// Interaction logic for ViewReservationPage.xaml
    /// </summary>
    public partial class ViewReservationPage : Page
    {
        private ViewReservationViewModel ViewModel => (ViewReservationViewModel)DataContext;
        private readonly ReservationValidator res;

        public ViewReservationPage()
        {
            InitializeComponent();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            SessionManager.Instance.Logout();
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            //Close();
        }

        private void MyReservations(object sender, RoutedEventArgs e)
        {
            var myReservationsWindow = new ViewReservationPage();
            //myReservationsWindow.Show();
            //My reservations Window
        }
        private void PlaceReservation(object sender, RoutedEventArgs e)
        {
            //place reservations window
        }
        private void Settings(object sender, RoutedEventArgs e)
        {
            //settings window
        }
        private void SubmitButtonClicked(object sender, RoutedEventArgs e)
        {
            var reservation = new ReservationEntity()
            {
            };
        }
    }
}

