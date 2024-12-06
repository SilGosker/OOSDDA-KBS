using Kbs.Business.Boat;
using Kbs.Business.Medal;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Data.Medal;
using Kbs.Data.User;
using Kbs.Wpf.Boat.Read.Index;
using Kbs.Wpf.Medal.Components;
using Kbs.Wpf.Reservation.Read.Index;
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

namespace Kbs.Wpf.Medal.Create
{
    /// <summary>
    /// Interaction logic for CreateMedalPage.xaml
    /// </summary>
    public partial class CreateMedalPage : Page
    {
        private CreateMedalViewModel ViewModel => (CreateMedalViewModel)DataContext;
        private readonly UserRepository _userRepository = new();
        private readonly BoatRepository _boatRepository = new();
        private readonly MedalRepository _MedalRepository = new();
        private readonly INavigationManager _navigationManager;
        private int _boatId = 48;
        private int _userId;
        private int _gameId;
        private MedalMaterial _medalMaterial;
        public CreateMedalPage(INavigationManager navigationManager, int gameId)
        {
            _gameId = gameId;
            _navigationManager = navigationManager;
            InitializeComponent();
            foreach(UserEntity user in _userRepository.Get())
            {
                ViewModel.Users.Add(new CreateMedalUserViewModel(user));
            }
            ViewModel.MedalMaterial.Add(new MedalMaterialViewModel(MedalMaterial.Bronze));
            ViewModel.MedalMaterial.Add(new MedalMaterialViewModel(MedalMaterial.Silver));
            ViewModel.MedalMaterial.Add(new MedalMaterialViewModel(MedalMaterial.Gold));

            foreach (BoatEntity boat in _boatRepository.GetManyByGame(_gameId))
            {
                ViewModel.Boats.Add(new CreateMedalBoatViewModel(boat));
            }
        }

        private void ComboBoxBoats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var selected = (CreateMedalBoatViewModel)comboBox.SelectedItem;

            if (selected == null) return;
            _boatId = selected.BoatId;

        }

        private void ComboBoxUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var selected = (CreateMedalUserViewModel)comboBox.SelectedItem;

            if (selected == null) return;
            _userId = selected.UserId;  
        }

        private void ComboBoxMedalMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var selected = (MedalMaterialViewModel)comboBox.SelectedItem;

            if (selected == null) return;
            _medalMaterial = selected.MedalMaterial;
        }

        private void ButtonRewardMedal_Click(object sender, RoutedEventArgs e)
        {
            var medal = new MedalEntity();
            medal.Material = _medalMaterial;
            medal.UserId = _userId;
            medal.BoatId = _boatId;
            medal.GameId = _gameId;

            _MedalRepository.Create(medal);

            _navigationManager.Navigate(() => new ReadIndexReservationPage(_navigationManager));
        }
    }
}
