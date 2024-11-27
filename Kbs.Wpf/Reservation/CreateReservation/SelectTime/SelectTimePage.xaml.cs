using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Data.Boat;
using Kbs.Data.Reservation;
using Kbs.Wpf.Reservation.CreateReservation.SelectBoatType;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Kbs.Wpf.Reservation.CreateReservation.SelectTime
{
    /// <summary>
    /// Interaction logic for SelectTime.xaml
    /// </summary>
    public partial class SelectTimePage : Page
    {
        private readonly INavigationManager _navigationManager;
        ReservationMaker maker = new ReservationMaker(new ReservationRepository());
        BoatEntity boatSelected;
        int daysFromToday = 0;
        private readonly BoatRepository _boatRepository = new();
        private SelectTimeViewModel ViewModel => (SelectTimeViewModel)DataContext;
        List<double> checklist = new List<double>();
        public SelectTimePage(INavigationManager navigationManager, BoatTypeEntity boatType)
        {

            checklist.Add(256);
            double gridRow = 6;
            for (int k = 0; k < 27; k++)
            {
                checklist.Add(gridRow);
                gridRow += 0.5;
            }

            InitializeComponent();
            _navigationManager = navigationManager;
            //change this

            var boats = _boatRepository.GetAvailableByType(boatType.BoatTypeId);
            foreach (BoatEntity boat in boats)
            {
                ViewModel.Boats.Add(new SelectTimeBoatViewModel(boat));
            }


        }

        public void TimeSlotButton_Click(object sender, RoutedEventArgs e)
        {

            Button send = (Button)sender;
            Tuple<ReservationTime, BoatEntity> chosenTimeAndBoat = (Tuple<ReservationTime, BoatEntity>)send.Tag;
            _navigationManager.Navigate(() => new SelectLength.SelectLengthPage(_navigationManager, chosenTimeAndBoat));
        }

        private void ComboBoxBoats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var comboBox = (ComboBox)sender;
            var selected = (SelectTimeBoatViewModel)comboBox.SelectedItem;
            boatSelected = selected.Boat;

            RefreshCalander();

        }

        private void RefreshCalander()
        {
            ViewModel.ThisWeek.Clear();
            buttons.Children.Clear();
            int countVar = 0;
            for (int i = daysFromToday; countVar < 7; i++)
            {
                DateTime weekday = new DateTime();
                weekday = DateTime.Now;
                weekday = weekday.AddDays(i);
                ViewModel.ThisWeek.Add(weekday);

                foreach (ReservationTime j in maker.MakeReservableTimes(weekday, boatSelected))
                {

                    Tuple<ReservationTime, BoatEntity> chosenTimeAndBoat = new Tuple<ReservationTime, BoatEntity>(j, boatSelected);
                    if (!(j.Length == 0))
                    {
                        Button button = new Button()
                        {

                            Width = 150,
                            Height = (j.Length * 42),
                            Tag = chosenTimeAndBoat,
                            Background = Brushes.Red,
                            BorderBrush = Brushes.Black,
                            FontSize = 28,
                            Content = "Reserveer"
                        };
                        button.Tag = chosenTimeAndBoat;
                        button.Click += TimeSlotButton_Click;

                        buttons.Children.Add(button);

                        double compare = 0;

                        if (j.StartTime.Minute == 30)
                        {
                            compare += 0.5;
                        }

                        compare += j.StartTime.Hour;
                        int rowspan = Convert.ToInt32(j.Length + j.Length);


                        Grid.SetRow(button, checklist.IndexOf(compare));
                        Grid.SetColumn(button, countVar);
                        Grid.SetRowSpan(button, rowspan);
                    }
                }
                countVar++;
            }
        }


        private void PreviousStep(object sender, System.Windows.RoutedEventArgs e)
        {
            _navigationManager.Navigate(() => new SelectBoatTypePage(_navigationManager));
        }

        private void NextWeekButton_Click(object sender, RoutedEventArgs e)
        {
            daysFromToday += 7;
            RefreshCalander();
        }

        private void BackWeekButton_Click(object sender, RoutedEventArgs e)
        {
            daysFromToday -= 7;
            RefreshCalander();
        }
    }
}
