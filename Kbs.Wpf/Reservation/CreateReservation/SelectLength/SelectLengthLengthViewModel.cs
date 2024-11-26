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
        public bool checkable
        {
            get => _checkable;
            set => SetField(ref _checkable, value);
        }

        public TimeSpan length
        {
            get => _length;
            set => SetField(ref _length, value);
        }
        public bool ischecked
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
            this.checkable = checkable;
            this.length = length;
            this.ischecked = ischecked;
            Content = length.TotalMinutes.ToString() + " minuten";
        }
    }
}
