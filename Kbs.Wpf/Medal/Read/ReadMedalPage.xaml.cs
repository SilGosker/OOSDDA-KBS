using Kbs.Business.Medal;
using Kbs.Business.Session;
using Kbs.Data.Game;
using Kbs.Data.Medal;
using System.Windows.Controls;

namespace Kbs.Wpf.Medal.Read
{
    public partial class ReadMedalPage : Page
    {
        private readonly MedalRepository _medalRepository = new();
        private readonly GameRepository _gameRepository = new();
        public INavigationManager _navigationManager;
        public MedalEntity _medalEntity = new();
        private int _totalAmountOfGold; 
        private int _totalAmountOfSilver;
        private int _totalAmountOfBronze;
        private ReadMedalViewModel ViewModel => (ReadMedalViewModel)DataContext;
        private ReadMedalsGameViewModel peepee => (ReadMedalsGameViewModel)DataContext;
        public ReadMedalPage(INavigationManager _navigationManager)
        {
            InitializeComponent();
            var user = SessionManager.Instance.Current.User;
            var medals = _medalRepository.GetByUserId(user.UserId);

            foreach (var medal in medals)
            {
                var game = _gameRepository.GetById(medal.GameId);
                var gameViewModel = new ReadMedalsGameViewModel(game, medal.Material);

                ViewModel.Games.Add(gameViewModel);

                if (medal.Material == MedalMaterial.Gold)
                {
                    _totalAmountOfGold++;
                }
                else if (medal.Material == MedalMaterial.Silver)
                {
                    _totalAmountOfSilver++;
                }
                else if (medal.Material == MedalMaterial.Bronze)
                {
                    _totalAmountOfBronze++;
                }
            }
            ViewModel.Gold = _totalAmountOfGold;
            ViewModel.Silver = _totalAmountOfSilver;
            ViewModel.Bronze = _totalAmountOfBronze;
        }


        public void GameSelected(object sender, EventArgs e)
        {
            
        }
    }
}
