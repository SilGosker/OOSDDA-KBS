using Kbs.Wpf.Boat.Index;
using Kbs.Wpf.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Wpf.Reservation.CreateReservation.SelectLength
    {
    public class SelectLengthViewModel : ViewModel
        {

        public ObservableCollection<string> AvailableStartTimes
            {
            get => _availableStartTimes;
            set => SetField(ref _availableStartTimes, value);
            }

        public string name
            {
            get => _name;
            set => SetField(ref _name, value);
            }

        public string day{
            get => _day;
            set => SetField(ref _day, value);
            }

        public string date{
            get => _date;
            set => SetField(ref _date, value);
            }

        private string _name;

        private string _day;

        private string _date;

        private ObservableCollection<string> _availableStartTimes;


        public  void MakeSelectLengthViewModel(ObservableCollection<string> availableStartTimes, string name, DateTime reservationDate)
            {
            this.AvailableStartTimes = availableStartTimes;
            this.name = name;
            this.day = reservationDate.DayOfWeek.ToString();
            this.date = reservationDate.Day + "/" + reservationDate.Month + "/" + reservationDate.Year;
            }

        }
    }
