using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Game;
using Kbs.Data.Game;
using Kbs.Wpf.Reservation.Create.SelectBoatType;

namespace Kbs.Wpf.Game.Create;

public partial class CreateGamePage : Page
{
    private readonly GameValidator _gameValidator = new();
    private readonly GameRepository _gameRepository = new();
    private readonly INavigationManager _navigationManager;
    private CreateGameViewModel ViewModel => (CreateGameViewModel)DataContext;
    public CreateGamePage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        ViewModel.Date = DateTime.Now;
    }

    private void SaveWithoutBoats(object sender, RoutedEventArgs e)
    {
        var game = new GameEntity
        {
            Name = ViewModel.Name,
            Date = ViewModel.Date
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

        if (validationResult.Count == 0)
        {
            _gameRepository.Create(game);
            // TODO: Navigate to game detail page
        }
    }

    private void SaveAndCreateReservation(object sender, RoutedEventArgs e)
    {
        var game = new GameEntity
        {
            Name = ViewModel.Name,
            Date = ViewModel.Date
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

        if (validationResult.Count == 0)
        {
            _gameRepository.Create(game);
            _navigationManager.Navigate(() => new SelectBoatTypePage(_navigationManager, game));
        }
    }
}