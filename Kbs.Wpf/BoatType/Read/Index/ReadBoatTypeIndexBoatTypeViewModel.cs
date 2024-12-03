using Kbs.Business.BoatType;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.Read.Index
{
    public class ReadBoatTypeIndexBoatTypeViewModel : ViewModel
    {
        private string _name;
        private int _id;
        public ReadBoatTypeIndexBoatTypeViewModel(BoatTypeEntity boatType)
        {
            this.Name = boatType.Name;
            this.Id = boatType.BoatTypeId;
        }

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

    }
}
