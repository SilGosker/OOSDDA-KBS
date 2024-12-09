using Kbs.Wpf.Components;
using System.Collections.ObjectModel;


namespace Kbs.Wpf.Medal.Read
{
    public class ReadMedalViewModel : ViewModel {
        public ObservableCollection<ReadMedalsGameViewModel> Games { get; set; } = new ObservableCollection<ReadMedalsGameViewModel>();
        private int _gold;
        private int _silver;
        private int _bronze;
        public int Gold
        {
            get => _gold;
            set
            {
                SetField(ref _gold, value);
                OnPropertyChanged(nameof(GoldFormatted));
            }
        }

        public int Silver
        {
            get => _silver;
            set
            {
                SetField(ref _silver, value);
                OnPropertyChanged(nameof(SilverFormatted));
            }
        }
        public int Bronze
        {
            get => _bronze;
            set
            {
                SetField(ref _bronze, value);
                OnPropertyChanged(nameof(BronzeFormatted));
            }
        }

        public string GoldFormatted => $"{Gold}x";
        public string SilverFormatted => $"{Silver}x";
        public string BronzeFormatted => $"{Bronze}x";
    }
}
