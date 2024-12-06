using Kbs.Wpf.Components;
using Kbs.Business.Game;
using Kbs.Business.Medal;

namespace Kbs.Wpf.Medal.Read
{
    public class ReadMedalsGameViewModel : ViewModel
    {
        private string _material;
        private string _date;

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
        public ReadMedalsGameViewModel(GameEntity game, MedalMaterial material)
        {
            Date = game.Date.ToShortDateString();  
            Material = material.ToDutchString();  
        }
    }
    
}
