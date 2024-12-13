using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.Boat.Read.Details
{
    public class ChangeStatusMaintainingViewModel : ViewModel
    {
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetField(ref _date, value);
        }
    }
}
