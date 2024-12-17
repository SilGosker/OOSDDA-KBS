using Kbs.Wpf.Components;
using Kbs.Business.Game;
using Kbs.Business.Medal;
using Kbs.Business.Extentions;

namespace Kbs.Wpf.Medal.Read
{
    public class ReadMedalMedalViewModel : ViewModel
    {
        private string _material;
        private string _date;
        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }
        public string Material
        {
            get => _material;
            set => SetField(ref _material, value);
        }

        public string Date
        {
            get => _date;
            set => SetField(ref _date, value);
        }

        public ReadMedalMedalViewModel(GameEntity game, MedalMaterial material)
        {
            Date = game.Date.ToDutchString();
            Material = material.ToDutchString();  
            Name = game.Name;
        }
    }
    
}
