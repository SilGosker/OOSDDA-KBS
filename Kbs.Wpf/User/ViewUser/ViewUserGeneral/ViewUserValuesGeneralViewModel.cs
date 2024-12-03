using Kbs.Business.BoatType;
using Kbs.Business.Helpers;
using Kbs.Business.User;
using Kbs.Wpf.Boat.Create;
using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.User.ViewUser.ViewUserGeneral
{
    public class ViewUserValuesGeneralViewModel : ViewModel
    {
        public ObservableCollection<ViewUserValuesValuesGeneralViewModel> Items { get; } = new();
    }
}
