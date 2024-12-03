using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Wpf.Boat.Read.Details;
using Kbs.Wpf.BoatType.Read.Index;
using Kbs.Wpf.BoatType.Update;

namespace Kbs.Wpf.BoatType.Read.Details
{
    [HasRole(UserRole.MaterialCommissioner)]
    public partial class ReadDetailsBoatTypePage : Page
    {
        private readonly INavigationManager _navigationManager;
        private readonly BoatTypeRepository _boatTypeRepository = new BoatTypeRepository();
        private readonly BoatRepository _boatRepository = new BoatRepository();
        private readonly ReservationEntity _reservationEntity = new ReservationEntity();
        private ReadDetailsBoatTypeViewModel ReadDetailsBoatTypeViewModel => (ReadDetailsBoatTypeViewModel)DataContext;
        public ReadDetailsBoatTypePage(INavigationManager navigationManager, int boatTypeId)
        {
            this._navigationManager = navigationManager;
            InitializeComponent();
            var boatType = _boatTypeRepository.GetByBoatTypeID(boatTypeId);
            ReadDetailsBoatTypeViewModel.Speed = boatType.Speed;
            ReadDetailsBoatTypeViewModel.BoatTypeId = boatType.BoatTypeId;
            ReadDetailsBoatTypeViewModel.Experience = boatType.RequiredExperience.ToDutchString();
            ReadDetailsBoatTypeViewModel.HasWheel = boatType.HasSteeringWheel;
            ReadDetailsBoatTypeViewModel.Seats = boatType.Seats.ToDutchString();
            ReadDetailsBoatTypeViewModel.Name = boatType.Name;

            var boats = _boatRepository.GetManyByType(boatTypeId);
            foreach (var boattype in boats)
            {
                ReadDetailsBoatTypeViewModel.Items.Add(new ReadDetailsBoatTypeBoatViewModel(boattype));
            }
        }
        public void Refresh()
        {
            _navigationManager.Navigate(() => new ViewBoatTypesPage(_navigationManager));
        }

        private void RemoveBoatType(object sender, RoutedEventArgs e)
        {
            BoatTypeEntity entity = _boatTypeRepository.GetByBoatTypeID(ReadDetailsBoatTypeViewModel.BoatTypeId);
            MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _boatTypeRepository.Delete(entity);
                this.Refresh();
            }
            
        }

        private void Wijzigen(object sender, RoutedEventArgs e)
        {
            _navigationManager.Navigate(() => new UpdateBoatTypePage(_navigationManager, ReadDetailsBoatTypeViewModel.BoatTypeId));
        }
        private void BoatSelected(object sender, MouseButtonEventArgs e)
        {
            var row = (DataGridRow)sender;
            if (row == null)
            {
                return;
            }

            var dataContext = (ReadDetailsBoatTypeBoatViewModel)row.DataContext;
            _navigationManager.Navigate(() => new ReadDetailsBoatPage(_navigationManager, dataContext.BoatId));
        }
    }
}
