using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.Reservation.CreateReservation.SelectLength
{
    public class SelectLengthLengthViewModel : ViewModel
    {
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
            get => _ischecked;
            set => SetField(ref _ischecked, value);
        }
        public string Content
        {
            get => _content;
            set => SetField(ref _content, value);
        }

        private bool _checkable;

        private TimeSpan _length;

        private bool _ischecked;

        private string _content;

        public SelectLengthLengthViewModel(bool checkable, TimeSpan length, bool ischecked)
        {
            this.Checkable = checkable;
            this.Length = length;
            this.IsChecked = ischecked;
            Content = length.TotalMinutes.ToString() + " minuten";
        }
    }
}
