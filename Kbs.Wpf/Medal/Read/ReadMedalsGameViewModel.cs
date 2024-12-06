﻿using Kbs.Wpf.Components;
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
        public ReadMedalsGameViewModel(GameEntity medal)
        {
            Date = medal.Date.ToShortDateString();
            Material = medal.Place.ToString();
        }
        public ReadMedalsGameViewModel(MedalMaterial medal)
        {
            if (medal == MedalMaterial.Bronze)
            {
                Material = MedalMaterial.Bronze.ToString();
            }
            if (medal == MedalMaterial.Silver)
            {
                Material = MedalMaterial.Silver.ToString();
            }
            if (medal == MedalMaterial.Gold)
            {
                Material = MedalMaterial.Gold.ToString();
            }
        }
    }
    
}
