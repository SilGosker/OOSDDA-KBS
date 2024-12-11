using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.Boat;
using Kbs.Business.Game;
using Kbs.Business.Reservation;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.Game;
using Kbs.Data.Medal;
using Kbs.Data.Reservation;
using Kbs.Data.User;
using Kbs.Wpf.BoatType.Read.Details;
using Kbs.Wpf.Reservation.Read.Details;
using static Dapper.SqlMapper;

namespace Kbs.Wpf.User.Read.Details
{
    public partial class ReadDetailsUserPage : Page
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly INavigationManager _navigationManager;
        private readonly ReservationRepository _reservationRepository = new ReservationRepository();
        private readonly BoatRepository _boatRepository = new BoatRepository();
        private readonly GameRepository _gameRepository = new GameRepository();
        private readonly MedalRepository _medalRepository = new MedalRepository();

        private ReadDetailsUserViewModel ViewModel => (ReadDetailsUserViewModel)DataContext;

        public ReadDetailsUserPage(INavigationManager navigationManager, int id)
        {
            _navigationManager = navigationManager;
            InitializeComponent();

            var user = _userRepository.GetById(id);
            ViewModel.Name = user.Name;
            ViewModel.UserId = user.UserId;
            ViewModel.Email = user.Email;
            ViewModel.Role = user.Role.ToDutchString();

            var reservations = _reservationRepository.GetByUserId(id);
            foreach (var reservation in reservations)
            {
                ViewModel.Reservations.Add(new ReadDetailsUserReservationViewModel(reservation));
            }
            var medals = _medalRepository.GetAllByUserId(id);
            foreach (var medal in medals)
            {
                BoatEntity boat = medal.BoatId == null? null : _boatRepository.GetById((int)medal.BoatId);
                GameEntity game = _gameRepository.GetById(medal.GameId);
                ViewModel.Medals.Add(new ReadDetailsUserMedalViewModel(medal, game, boat, RemoveMedal));
            }
        }

        private void ReservationSelected(object sender, MouseButtonEventArgs e)
        {
            var row = (DataGridRow)sender;
            
            if (row == null) return;

            var dataContext = (ReadDetailsUserReservationViewModel)row.DataContext;
            _navigationManager.Navigate(() => new ReadDetailsReservationPage(dataContext.ReservationId, _navigationManager));
        }

        private void Ban(object sender, RoutedEventArgs e)
        {
            var entity = _userRepository.GetById(ViewModel.UserId);
            var userId = entity.UserId;
            MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
            {
                _userRepository.ChangeRole(entity);
                _navigationManager.Navigate(() => new ReadDetailsUserPage(_navigationManager, userId));
            }
        }

        private void RemoveMedal(int medalId)
        {
            MessageBoxResult result = MessageBox.Show("Weet u het zeker dat u deze medaille wil intrekken?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
            {
                _medalRepository.RemoveById(medalId);
                _navigationManager.Navigate(() => new ReadDetailsUserPage(_navigationManager, ViewModel.UserId));
            }
        }
    }
}
