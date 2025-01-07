using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.Game;
using Kbs.Business.Medal;
using Kbs.Business.User;
using Kbs.Data.Course;
using Kbs.Data.Game;
using Kbs.Data.Medal;
using Kbs.Data.Reservation;
using Kbs.Data.User;
using Kbs.Wpf.Components;
using Kbs.Wpf.Game.Read.Index;
using Kbs.Wpf.Medal.Create;
using Kbs.Wpf.Reservation.Create.SelectBoatType;
using Kbs.Wpf.Reservation.Read.Details;
using Kbs.Wpf.User.Read.Details;

namespace Kbs.Wpf.Game.Read.Details;

[HasRole(UserRole.GameCommissioner)]
[HighlightFor(typeof(GameEntity))]
public partial class ReadDetailsGamePage : Page
{
    private readonly GameRepository _gameRepository = new();
    private readonly CourseRepository _courseRepository = new();
    private readonly ReservationRepository _reservationRepository = new();
    private readonly INavigationManager _navigationManager;
    private readonly GameValidator _gameValidator = new();
    private readonly MedalRepository _medalRepository = new();
    private readonly UserRepository _userRepository = new();
    private ReadDetailsGameViewModel ViewModel => (ReadDetailsGameViewModel)DataContext;
    public ReadDetailsGamePage(INavigationManager navigationManager, int gameId)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        var game = _gameRepository.GetById(gameId);
        ViewModel.GameId = game.GameId;
        ViewModel.CourseId = game.CourseId;
        ViewModel.Name = game.Name;
        ViewModel.CourseName = _courseRepository.GetById(game.CourseId).Name;
        ViewModel.Date = game.Date;
        
        foreach (var reservation in _reservationRepository.GetManyByGameId(game.GameId))
        {
            ViewModel.Boats.Add(new ReadDetailsGameBoatViewModel
            {
                ReservationId = reservation.ReservationId,
                Name = reservation.Boat.Name
            });
        }

        foreach (var medal in _medalRepository.GetAllByGameId(gameId))
        {
            var user = _userRepository.GetById(medal.UserId);
            ViewModel.Medals.Add(new ReadDetailsGameMedalViewModel(user, medal));
        }


        foreach (var course in _courseRepository.GetAll())
        {
            var courseViewModel = new ReadDetailsGameCourseViewModel
            {
                Name = course.Name,
                Id = course.CourseId,
            };
            ViewModel.Courses.Add(courseViewModel);

            if (course.CourseId == ViewModel.CourseId)
            {
                ViewModel.SelectedCourse = courseViewModel;
            }
        }
    }
    
    private void UpdateGame(object sender, RoutedEventArgs e)
    {
        GameEntity game = new GameEntity()
        {
            GameId = ViewModel.GameId,
            CourseId = ViewModel.CourseId,
            Name = ViewModel.Name,
            Date = ViewModel.Date
        };
        var validationResult = _gameValidator.ValidateForUpdate(game);
        if (validationResult.Count > 0)
        {
            ViewModel.NameError = validationResult.GetValueOrDefault(nameof(game.Name), ""); 
            ViewModel.CourseErrorMessage = validationResult.GetValueOrDefault(nameof(game.CourseId), "");
            ViewModel.DateErrorMessage = validationResult.GetValueOrDefault(nameof(game.Date), "");
        }
        else
        {
            _gameRepository.Update(game);
            MessageBox.Show($"{game.Name} succesvol geüpdatet");
            _navigationManager.Navigate(() => new ReadDetailsGamePage(_navigationManager, game.GameId));
        }
    }
    
    private void DeleteGame(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (MessageBoxResult.No == result)
        {
            return;
        }
        var game = _gameRepository.GetById(ViewModel.GameId);
        var medals = _medalRepository.GetAllByGameId(game.GameId);
        foreach (MedalEntity medal in medals)
        {
            _medalRepository.RemoveById(medal.MedalId);
        }

        _gameRepository.DeleteById(game.GameId);
        MessageBox.Show($"{game.Name} succesvol verwijderd");
        _navigationManager.Navigate(() => new ReadIndexGamePage(_navigationManager));
    }

    private void CourseChanged(object sender, SelectionChangedEventArgs e)
    {
        var course = (ReadDetailsGameCourseViewModel)((ComboBox)sender).SelectedItem;
        ViewModel.CourseId = course.Id;
    }
    
    private void OnReservationClick(object sender, MouseButtonEventArgs e)
    {
        var reservation = (ReadDetailsGameBoatViewModel)((DataGridRow)sender).DataContext;
        _navigationManager.Navigate(() => new ReadDetailsReservationPage(reservation.ReservationId, _navigationManager));
    }

    private void AddReservations(object sender, RoutedEventArgs e)
    {
        var game = _gameRepository.GetById(ViewModel.GameId);
        _navigationManager.Navigate(() => new SelectBoatTypePage(_navigationManager, game));
    }

    private void GiveMedal(object sender, RoutedEventArgs e)
    {
        _navigationManager.Navigate(() => new CreateMedalPage(_navigationManager, ViewModel.GameId));
    }

    private void OnMedalClick(object sender, MouseButtonEventArgs e)
    {
        var medal = (ReadDetailsGameMedalViewModel)((DataGridRow)sender).DataContext;
        _navigationManager.Navigate(() => new ReadDetailsUserPage(_navigationManager, medal.UserId));
    }
}