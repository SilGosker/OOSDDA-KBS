using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.Game;
using Kbs.Data.Boat;
using Kbs.Data.Course;
using Kbs.Data.Game;
using Kbs.Wpf.Game.Read.Index;
using Kbs.Wpf.Reservation.Read.Details;

namespace Kbs.Wpf.Game.Read.Details;

public partial class ReadDetailsGamePage : Page
{
    private readonly GameRepository _gameRepository = new();
    private readonly CourseRepository _courseRepository = new();
    private readonly BoatRepository _boatRepository = new();
    private readonly INavigationManager _navigationManager;
    private readonly GameValidator _gameValidator;
    private ReadDetailsGameViewModel ViewModel => (ReadDetailsGameViewModel)DataContext;
    public ReadDetailsGamePage(INavigationManager navigationManager, int gameId)
    {
        _navigationManager = navigationManager;
        _gameValidator = new GameValidator();
        InitializeComponent();
        var game = _gameRepository.GetById(gameId);
        ViewModel.GameId = game.GameId;
        ViewModel.CourseId = game.CourseId;
        ViewModel.Name = game.Name;
        ViewModel.CourseName = _courseRepository.GetById(game.CourseId).Name;
        ViewModel.Date = game.Date;
        
        foreach (var boat in _boatRepository.GetManyByGame(game.GameId))
        {
            ViewModel.Boats.Add(new ReadDetailsGameBoatViewModel
            {
                ReservationId = boat.ReservationId,
                Name = boat.Name
            });
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
        };
        var validationResult = _gameValidator.ValidateForUpdate(game);
        if (validationResult.Count > 0)
        {
            ViewModel.NameError = validationResult.GetValueOrDefault(nameof(game.Name), "");
        }
        else
        {
            _gameRepository.Update(game);
            MessageBox.Show($"{game.Name} succesvol geüpdatet");
        }
    }
    
    private void DeleteGame(object sender, RoutedEventArgs e)
    {
        var game = _gameRepository.GetById(ViewModel.GameId);
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
}