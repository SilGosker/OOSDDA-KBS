using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.Create.SelectLength
{
    public class SelectLengthLengthViewModel : ViewModel
    {
        private bool _checkable;
        private TimeSpan _length;
        private bool _isChecked;
        private string _content;
        
        public bool Checkable
        {
            get => _checkable;
            set => SetField(ref _checkable, value);
        }
        public TimeSpan Length
        {
            get => _length;
            set => SetField(ref _length, value);
        }
        public bool IsChecked
        {
            get => _isChecked;
            set => SetField(ref _isChecked, value);
        }
        public string Content
        {
            get => _content;
            set => SetField(ref _content, value);
        }
        
        public SelectLengthLengthViewModel(bool checkable, TimeSpan length, bool ischecked)
        {
            Checkable = checkable;
            Length = length;
            IsChecked = ischecked;
            Content = length.TotalHours + " uur";
        }
    }
}
