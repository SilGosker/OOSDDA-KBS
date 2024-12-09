using Kbs.Business.Boat;
using Kbs.Business.Medal;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.Medal;
using Kbs.Data.User;
using Kbs.Wpf.Medal.Components;
using Kbs.Wpf.Reservation.Read.Index;
using System.Windows;
using System.Windows.Controls;

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
        public CreateMedalPage(INavigationManager navigationManager, int gameId)
        {
            ViewModel.SelectedGameId = gameId;
            _navigationManager = navigationManager;
            InitializeComponent();
            foreach(UserEntity user in _userRepository.Get())
            {
                ViewModel.Users.Add(new CreateMedalUserViewModel(user));
            }
            ViewModel.MedalMaterial.Add(new MedalMaterialViewModel(MedalMaterial.Bronze));
            ViewModel.MedalMaterial.Add(new MedalMaterialViewModel(MedalMaterial.Silver));
            ViewModel.MedalMaterial.Add(new MedalMaterialViewModel(MedalMaterial.Gold));

            foreach (BoatEntity boat in _boatRepository.GetManyByGame(ViewModel.SelectedGameId))
            {
                ViewModel.Boats.Add(new CreateMedalBoatViewModel(boat));
            }
        }

        private void ComboBoxBoats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var selected = (CreateMedalBoatViewModel)comboBox.SelectedItem;

            if (selected == null) return;
            ViewModel.SelectedBoat = selected;

        }

        private void ComboBoxUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var selected = (CreateMedalUserViewModel)comboBox.SelectedItem;

            if (selected == null) return;
            ViewModel.SelectedUser = selected;  
        }

        private void ComboBoxMedalMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var selected = (MedalMaterialViewModel)comboBox.SelectedItem;

            if (selected == null) return;
            ViewModel.SelectedMaterial = selected;
        }

        private void ButtonRewardMedal_Click(object sender, RoutedEventArgs e)
        {
            var medal = new MedalEntity();
            medal.Material = ViewModel.SelectedMaterial.MedalMaterial;
            medal.UserId = ViewModel.SelectedUser.UserId;
            medal.BoatId = ViewModel.SelectedBoat.BoatId;
            medal.GameId = ViewModel.SelectedGameId;

            _MedalRepository.Create(medal);

            _navigationManager.Navigate(() => new ReadIndexReservationPage(_navigationManager));
        }
    }
}
