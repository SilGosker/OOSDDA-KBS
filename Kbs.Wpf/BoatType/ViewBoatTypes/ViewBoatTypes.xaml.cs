using Kbs.Business.BoatType;
using Kbs.Business.User;
using Kbs.Data.BoatType;
using Kbs.Data.Reservation;
using Kbs.Wpf.Attributes;
using Kbs.Wpf.BoatType.CreateBoatType;
using Kbs.Wpf.BoatType.ViewDetailedBoatTypes;
using Kbs.Wpf.Reservation.ViewReservationGeneralPage;
using Kbs.Wpf.Reservation.ViewReservationSpecificPage;
using System.Windows;
using System.Windows.Controls;

namespace Kbs.Wpf.BoatType.ViewBoatTypes
{
    [HasRole(Role.GameCommissioner)]
    public partial class ViewBoatTypesPage : Page
    {

        private readonly INavigationManager _navigationManager;
        private ViewBoatTypesViewModel ViewModel => (ViewBoatTypesViewModel)DataContext;
        private readonly BoatTypeRepository _boatTypeRepository = new BoatTypeRepository();
        private readonly ReservationRepository _reservationRepository = new ReservationRepository();
        public ViewBoatTypesPage(INavigationManager navigationManager)
        {

            _navigationManager = navigationManager;
            InitializeComponent();
            foreach (var boatType in _boatTypeRepository.GetAll())
            {
                ViewModel.Items.Add(new ViewBoatTypeBoatTypeViewModel(boatType));
            }
        }
        
        private void BoatTypeSelected(object sender, RoutedEventArgs e)
        {
            var listViewItem = (ListViewItem)sender;
            var item = (ViewBoatTypeBoatTypeViewModel)listViewItem.DataContext;
            _navigationManager.Navigate(() => new ViewDetailedBoatTypesPage(this._navigationManager, item.Id));
        }

        private void AddBoatType(object sender, RoutedEventArgs e)
        {
            _navigationManager.Navigate(() => new CreateBoatTypePage(_navigationManager));
        }
    }
}
