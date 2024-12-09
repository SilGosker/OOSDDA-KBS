using Kbs.Business.Medal;
using Kbs.Business.Session;
using Kbs.Data.Game;
using Kbs.Data.Medal;
using System.Windows.Controls;

namespace Kbs.Wpf.Medal.Read;

public partial class ReadMedalPage : Page
{
    private readonly MedalRepository _medalRepository = new();
    private readonly GameRepository _gameRepository = new();
    private ReadMedalViewModel ViewModel => (ReadMedalViewModel)DataContext;
    public ReadMedalPage()
    {
        InitializeComponent();
        var user = SessionManager.Instance.Current.User;
        var medals = _medalRepository.GetByUserId(user.UserId);

        int totalAmountOfGold = 0;
        int totalAmountOfSilver = 0;
        int totalAmountOfBronze = 0;

        foreach (var medal in medals)
        {
            var game = _gameRepository.GetById(medal.GameId);
            var gameViewModel = new ReadMedalMedalViewModel(game, medal.Material);

            ViewModel.Games.Add(gameViewModel);

            switch (medal.Material)
            {
                case MedalMaterial.Gold:
                    totalAmountOfGold++;
                    break;
                case MedalMaterial.Silver:
                    totalAmountOfSilver++;
                    break;
                case MedalMaterial.Bronze:
                    totalAmountOfBronze++;
                    break;
            }
        }

        ViewModel.Gold = totalAmountOfGold;
        ViewModel.Silver = totalAmountOfSilver;
        ViewModel.Bronze = totalAmountOfBronze;
    }
}