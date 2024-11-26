using Kbs.Business.BoatType;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Wpf.Boat.Details;
using Kbs.Wpf.BoatType.CreateBoatType;
using Kbs.Wpf.BoatType.UpdateBoatType;
using Kbs.Wpf.BoatType.ViewBoatTypes;
using Kbs.Wpf.Components;
using Kbs.Wpf.Reservation.ViewReservationSpecificPage;
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

namespace Kbs.Wpf.BoatType.ViewDetailedBoatTypes
{
    /// <summary>
    /// Interaction logic for ViewDetailedBoatTypesPage.xaml
    /// </summary>
    public partial class ViewDetailedBoatTypesPage : Page
    {
        private readonly INavigationManager _navigationManager;
        private readonly BoatTypeRepository _boatTypeRepository = new BoatTypeRepository();
        private readonly BoatRepository _boatRepository = new BoatRepository();
        private ViewDetailedBoatTypesBoatViewModel ViewModel => (ViewDetailedBoatTypesBoatViewModel)DataContext;
        public ViewDetailedBoatTypesPage(INavigationManager navigationManager, int boatTypeId)
        {
            this._navigationManager = navigationManager;
            InitializeComponent();
            var boatType = _boatTypeRepository.GetByBoatTypeID(boatTypeId);
            ViewModel.Speed = boatType.Speed;
            ViewModel.BoatTypeId = boatType.BoatTypeId;
            ViewModel.Experience = boatType.RequiredExperience.ToDutchString();
            ViewModel.HasWheel = boatType.HasSteeringWheel;
            ViewModel.Seats = boatType.Seats.ToDutchString();
            ViewModel.Name = boatType.Name;

            var boats = _boatRepository.GetManyByType(boatTypeId);
            foreach (var boattype in boats)
            {
                ViewModel.Items.Add(new ViewBoatTypeBoatViewModel(boattype));
            }
        }

        private void RemoveBoatType(object sender, RoutedEventArgs e)
        {
            BoatTypeEntity entity = _boatTypeRepository.GetByBoatTypeID(ViewModel.BoatTypeId);

            MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _boatTypeRepository.Delete(entity);
            }
        }

        private void Wijzigen(object sender, RoutedEventArgs e)
        {
            _navigationManager.Navigate(() => new UpdateBoatTypePage(_navigationManager));
        }
        private void BoatSelected(object sender, MouseButtonEventArgs e)
        {
            var row = (DataGridRow)sender;
            if (row == null)
            {
                return;
            }

            var dataContext = (ViewBoatTypeBoatViewModel)row.DataContext;
            _navigationManager.Navigate(() => new BoatDetailPage(_navigationManager, dataContext.BoatId));
        }
    }
}
