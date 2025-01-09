using System.Collections.ObjectModel;
using Kbs.Wpf.Components;
using Kbs.Business.Extentions;

namespace Kbs.Wpf.Reservation.Create.SelectLength
{
    public class SelectLengthViewModel : ViewModel
    {
        public ObservableCollection<string> AvailableStartTimes
        {
            get => _availableStartTimes;
            set => SetField(ref _availableStartTimes, value);
        }

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public string Day
        {
            get => _day;
            set => SetField(ref _day, value);
        }

        public string Date
        {
            get => _date;
            set => SetField(ref _date, value);
        }

        private string _name;

        private string _day;

        private string _date;

        private ObservableCollection<string> _availableStartTimes;
        private string _gameCreateMessage;
        public ObservableCollection<SelectLengthLengthViewModel> RadioButtons { get; } = new();
        public string GameCreateMessage
        {
            get => _gameCreateMessage;
            set => SetField(ref _gameCreateMessage, value);
        }


        public void MakeSelectLengthViewModel(ObservableCollection<string> availableStartTimes, string name,
            DateTime reservationDate)
        {
            this.AvailableStartTimes = availableStartTimes;
            this.Name = name;
            this.Day = reservationDate.DayOfWeek.ToDutchString();
            this.Date = reservationDate.ToDutchString();
        }
    }
}