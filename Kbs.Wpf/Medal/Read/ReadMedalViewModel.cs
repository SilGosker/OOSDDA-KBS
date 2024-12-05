using Accessibility;
using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.Medal.Read
{
    public class ReadMedalViewModel : ViewModel
    {
        public ObservableCollection<ReadMedalsGameViewModel> Games = new ObservableCollection<ReadMedalsGameViewModel>();
        private int _gold;
        private int _silver;
        private int _bronze;
        public int Gold
        {
            get => _gold; 
            set => SetField(ref _gold, value);
        }
        public int Silver
        {
            get => _silver;
            set => SetField(ref _silver, value); 
        }
        public int Bronze
        {
            get => _bronze;
            set => SetField(ref _bronze, value);
        }
    }
}
