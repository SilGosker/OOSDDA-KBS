using Kbs.Wpf.Boat.Create;
using Kbs.Wpf.Components;
using Kbs.Wpf.Medal.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.Medal.Create
{
    public class CreateMedalViewModel : ViewModel
    {
        public ObservableCollection<CreateMedalUserViewModel> Users { get; } = new();
        public ObservableCollection<CreateMedalBoatViewModel> Boats { get; } = new();
        public ObservableCollection<MedalMaterialViewModel> MedalMaterial { get; } = new();
    }
}
