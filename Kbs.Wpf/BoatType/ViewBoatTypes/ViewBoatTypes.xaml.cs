using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using Kbs.Wpf.Boat.Index;
using Kbs.Wpf.Reservation.ViewReservationGeneralPage;
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

namespace Kbs.Wpf.BoatType.DetailedPage
{
    /// <summary>
    /// Interaction logic for DetailedPage.xaml
    /// </summary>
    public partial class DetailedPage : Page
    {
        private readonly INavigationManager _navigationManager;
        private ViewBoatTypesViewModel ViewModel => (ViewBoatTypesViewModel)DataContext;
        private readonly BoatTypeRepository _boatTypeRepository = new BoatTypeRepository();
        private readonly ReservationRepository _reservationRepository = new ReservationRepository();
        public DetailedPage(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            InitializeComponent();
            foreach (var boatType in _boatTypeRepository.GetName())
            {
                ViewModel.Items.Add(new BoatIndexBoatTypeViewModel(boatType));
            }
        }
        private void RemoveBoatType(object sender, RoutedEventArgs e)
        {
            BoatTypeEntity entity = _boatTypeRepository.GetByBoatTypeID(ViewModel.BoatTypeID);

            MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _boatTypeRepository.Delete(entity);
            }
        }
        private void BoatTypeSelected(object sender, RoutedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            BoatIndexBoatTypeViewModel context = (BoatIndexBoatTypeViewModel)item.DataContext;
            ViewModel.BoatTypeID = context.Id;
        }
    }
}
