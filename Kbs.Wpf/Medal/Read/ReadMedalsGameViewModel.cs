using System;
using Kbs.Wpf.Components;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kbs.Business.Medal;

namespace Kbs.Wpf.Medal.Read
{
    public class ReadMedalsGameViewModel : ViewModel
    {
        private string _place;
        private string _date;

        public string Place
        {
            get => _place;
            set => SetField(ref _place, value);
        }
        public string Date
        {
            get => _date;
            set => SetField(ref _date, value);
        }
        public ReadMedalsGameViewModel(MedalEntity medal)
        {

        }
    }
    
}
