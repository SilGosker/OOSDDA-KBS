using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Course;
using Kbs.Business.Game;
using Kbs.Business.User;
using Kbs.Data.Course;
using Kbs.Data.Game;
using Kbs.Wpf.Components;
using Kbs.Wpf.Game.Read.Details;
using Kbs.Wpf.Reservation.Create.SelectBoatType;

namespace Kbs.Wpf.Game.Create;

[HasRole(UserRole.GameCommissioner)]
[HighlightFor(typeof(GameEntity))]
public partial class CreateGamePage : Page
{
    private readonly GameValidator _gameValidator;
    private readonly GameRepository _gameRepository = new();
    private readonly CourseRepository _courseRepository = new();
    private readonly INavigationManager _navigationManager;
    private CreateGameViewModel ViewModel => (CreateGameViewModel)DataContext;

    public CreateGamePage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        _gameValidator = new();
        InitializeComponent();
        ViewModel.Date = DateTime.Now;

        Calendar.BlackoutDates.AddDatesInPast();

        foreach (CourseEntity course in _courseRepository.GetAll())
        {
            ViewModel.PossibleCourses.Add(new CreateGameCourseViewModel(course));
        }
    }

    private GameEntity Validate()
    {
        var game = new GameEntity
        {
            Name = ViewModel.Name,
            Date = ViewModel.Date,
            CourseId = ViewModel.SelectedCourse.Id
        };

        var validationResult = _gameValidator.ValidateForCreate(game);

        if (validationResult.TryGetValue(nameof(game.Name), out string nameErrorMessage))
        {
            ViewModel.NameErrorMessage = nameErrorMessage;
        }
        else
        {
            ViewModel.NameErrorMessage = string.Empty;
        }

        if (validationResult.TryGetValue(nameof(game.Date), out string dateErrorMessage))
        {
            ViewModel.DateErrorMessage = dateErrorMessage;
        }
        else
        {
            ViewModel.DateErrorMessage = string.Empty;
        }

        if (validationResult.TryGetValue(nameof(game.CourseId), out string courseErrorMessage))
        {
            ViewModel.CourseErrorMessage = courseErrorMessage;
        }
        else
        {
            ViewModel.CourseErrorMessage = string.Empty;
        }

        return validationResult.Count == 0 ? game : null;
    }
    private void SaveWithoutBoats(object sender, RoutedEventArgs e)
    {
        var game = Validate();

        if (game != null)
        {
            _gameRepository.Create(game);
            _navigationManager.Navigate(() => new ReadDetailsGamePage(_navigationManager, game.GameId)); ;
        }
    }

    private void SaveAndCreateReservation(object sender, RoutedEventArgs e)
    {
        var game = Validate();

        if (game != null)
        {
            _gameRepository.Create(game);
            _navigationManager.Navigate(() => new SelectBoatTypePage(_navigationManager, game));
        }
    }
}