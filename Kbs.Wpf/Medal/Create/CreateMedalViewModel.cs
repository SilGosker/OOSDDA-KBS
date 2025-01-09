using Kbs.Wpf.Components;
using Kbs.Wpf.Medal.Components;
using System.Collections.ObjectModel;

namespace Kbs.Wpf.Medal.Create
{
    public class CreateMedalViewModel : ViewModel
    {
        private CreateMedalUserViewModel _selectedUser;
        private CreateMedalBoatViewModel _selectedBoat;
        private MedalMaterialViewModel _selectedMaterial;
        private int _selectedGameId;

        public ObservableCollection<CreateMedalUserViewModel> Users { get; } = new();
        public ObservableCollection<CreateMedalBoatViewModel> Boats { get; } = new();
        public ObservableCollection<MedalMaterialViewModel> MedalMaterial { get; } = new();

        public CreateMedalUserViewModel SelectedUser
        {
            get => _selectedUser;
            set => SetField(ref _selectedUser, value);
        }
        public CreateMedalBoatViewModel SelectedBoat
        {
            get => _selectedBoat;
            set => SetField(ref _selectedBoat, value);
        }
        public MedalMaterialViewModel SelectedMaterial
        {
            get => _selectedMaterial;
            set => SetField(ref _selectedMaterial, value);
        }
        public int SelectedGameId
        {
            get => _selectedGameId;
            set => SetField(ref _selectedGameId, value);
        }
    }
}
