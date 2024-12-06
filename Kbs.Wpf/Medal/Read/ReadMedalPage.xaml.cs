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
                if (medal.Material == MedalMaterial.Gold)
                {
                    _totalAmountOfGold++;
                }
                if (medal.Material == MedalMaterial.Silver)
                {
                    _totalAmountOfSilver++;
                }
                if (medal.Material == MedalMaterial.Bronze)
                {
                    _totalAmountOfBronze++;
                }

                var game = _gameRepository.GetById(medal.GameId);
                ViewModel.Games.Add(new ReadMedalsGameViewModel(game));
            }

            ViewModel.Gold = _totalAmountOfGold; //mss ook nog todutchstring of andere manier om 1-> 1st etc te maken
            ViewModel.Silver = _totalAmountOfSilver;
            ViewModel.Bronze = _totalAmountOfBronze;
        }

        public void GameSelected(object sender, EventArgs e)
        {
            
        }
    }
}
