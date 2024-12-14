using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Read.Define
{
    public class ChangeStatusMaintainingViewModel : ViewModel
    {
        private int _boatId;
        private DateTime _date;
        private bool _isCancelled = false;

        public DateTime Date
        {
            get => _date;
            set => SetField(ref _date, value);
        }
        public int BoatId
        {
            get => _boatId;
            set => SetField(ref _boatId, value);
        }
        public bool IsCancelled
        {
            get => _isCancelled;
            set => SetField(ref _isCancelled, value);
        }
    }
}
