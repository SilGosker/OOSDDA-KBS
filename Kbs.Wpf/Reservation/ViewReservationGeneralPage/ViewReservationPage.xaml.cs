using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Wpf.Components;
using Kbs.Wpf.Reservation.ViewReservationSpecificPage;
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
            DataContext = new ViewReservationViewModel();
        }

        private void ZieMeer(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Show();
            }
            var viewReservationSpecificPage = new ViewPageSpecific();
            NavigationService.Navigate(viewReservationSpecificPage);
        }
    }
}

