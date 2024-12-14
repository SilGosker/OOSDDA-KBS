using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Read.Define
{
    public class ChangeStatusMaintainingViewModel : ViewModel
    {
        private int _boatId;
        private DateTime _endDate;
        private bool _isCancelled = false;

        public DateTime EndDate
        {
            get => _endDate;
            set => SetField(ref _endDate, value);
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
