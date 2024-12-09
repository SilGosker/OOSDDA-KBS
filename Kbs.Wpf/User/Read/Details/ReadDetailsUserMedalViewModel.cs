using Kbs.Business.Boat;
using Kbs.Business.Game;
using Kbs.Business.Medal;
using Kbs.Wpf.Components;
using System.Windows.Input;

namespace Kbs.Wpf.User.Read.Details
{
    public class ReadDetailsUserMedalViewModel : ViewModel
    {
        private int _medalId;
        private string _medal;
        private string _gameName;
        private string _boatName;
        private string _gameDate;
        private ICommand _removeMedalCommand;

        public ReadDetailsUserMedalViewModel(MedalEntity medal, GameEntity game, BoatEntity boat, Action<int> action)
        {
            MedalId = medal.MedalId;
            Medal = medal.Material.ToDutchString();
            GameName = game.Name;
            BoatName = boat.Name;
            GameDate = game.Date.ToString("yyyy-MM-dd");
            RemoveMedalCommand = new RelayCommand<int>(action);
        }
        public int MedalId 
        { 
            get => _medalId;
            set => SetField(ref _medalId, value);
        }
        public string Medal 
        { 
            get => _medal;
            set => SetField(ref _medal, value);
        }
        public string GameName 
        { 
            get => _gameName;
            set => SetField(ref _gameName, value);
        }
        public string BoatName 
        { 
            get => _boatName;
            set => SetField(ref _boatName, value);
        }
        public string GameDate 
        { 
            get => _gameDate;
            set => SetField(ref _gameDate, value);
        }
        public ICommand RemoveMedalCommand 
        { 
            get => _removeMedalCommand;
            set => SetField(ref _removeMedalCommand, value);
        }
    }
}
