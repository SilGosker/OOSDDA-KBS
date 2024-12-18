using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Components
{
    public class DatePickPopupViewModel : ViewModel
    {
        private int _boatId;
        private DateTime _endDate;
        private bool _isCancelled = false;
        private string _title;
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

        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }
    }
}
