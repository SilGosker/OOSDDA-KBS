using Kbs.Business.BoatType;
using Kbs.Data.BoatType;
using Kbs.Wpf.BoatType.ViewBoatTypes;
using Kbs.Wpf.Components;
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
        private ViewDetailedBoatTypesBoatViewModel ViewModel => (ViewDetailedBoatTypesBoatViewModel)DataContext;

        public ViewDetailedBoatTypesPage(int boatTypeId)
        {
            InitializeComponent();
            var boatType = _boatTypeRepository.GetByBoatTypeID(boatTypeId);
            ViewModel.Speed = boatType.Speed;
            ViewModel.BoatTypeId = boatType.BoattypeID;
            ViewModel.Experience = boatType.Experience;
            ViewModel.HasWheel = boatType.hasWheel;
            ViewModel.Seats = (int)boatType.Seats;
            ViewModel.Name = boatType.Name;
        }
        public ViewDetailedBoatTypesPage()
        {
            InitializeComponent();
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
    }
}
