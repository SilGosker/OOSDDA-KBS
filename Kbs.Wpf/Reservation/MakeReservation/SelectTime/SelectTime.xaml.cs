using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Data.Boat;
using Kbs.Data.Reservation;
using Kbs.Wpf.Boat.Index;
using Kbs.Wpf.Reservation.MakeReservation.SelectLength;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Kbs.Wpf.Reservation.MakeReservation.SelectTime
    {
    /// <summary>
    /// Interaction logic for SelectTime.xaml
    /// </summary>
    public partial class SelectTime : Page
        {
        MainWindow mainWindow;
        BoatTypeEntity boatType;
        BoatRepository boatRepository = new BoatRepository();
        ReservationRepository reservationRepository = new ReservationRepository();
        ReservationMaker maker = new ReservationMaker(new ReservationRepository());
        BoatEntity boatSelected;
        private SelectTimeViewModel ViewModel => (SelectTimeViewModel)DataContext;

        ObservableCollection<string> BoatNames = new ObservableCollection<string>();
        ObservableCollection<BoatEntity> Boats = new ObservableCollection<BoatEntity>();
        ObservableCollection<DateTime> ThisWeek = new ObservableCollection<DateTime>();
        List<double> checklist = new List<double>();
        public SelectTime(MainWindow mainWindow, BoatTypeEntity boatType)
            {

            checklist.Add(256);
            double tempy = 6;
            for (int k = 0; k < 27; k++)
                {
                checklist.Add(tempy);
                tempy += 0.5;
                }

            InitializeComponent();
            this.mainWindow = mainWindow;
            this.boatType = boatType;
            //change this


            foreach (BoatEntity boat in reservationRepository.GetAvailableBoatsByType(boatType.BoatTypeID))
                {
                BoatNames.Add(boat.Name);
                Boats.Add(boat);
                }
            ViewModel.BoatNames = BoatNames;
            ViewModel.Boats = Boats;


            for(int i = 0; i < 7; i++)
                {
                DateTime ree = new DateTime();
                ree = DateTime.Now;
                ree = ree.AddDays(i);
                ThisWeek.Add(ree);

                

                    }
            ViewModel.ThisWeek = ThisWeek;
                
            }

        public void Button_Clicky(object sender, RoutedEventArgs e)
            {
            
            Button send = (Button)sender;
            Tuple<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS, BoatEntity> chosenTimeAndBoat = (Tuple<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS, BoatEntity>)send.Tag;
            SelectLength.SelectLength selectLength = new SelectLength.SelectLength(mainWindow, chosenTimeAndBoat);
            mainWindow.NavigationFrame.Content = selectLength;
            }

        private void ComboBoxBoats_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {

            var comboBox = (ComboBox)sender;
            int selected = comboBox.SelectedIndex;
            boatSelected = Boats[selected];
            buttons.Children.Clear();

            for (int i = 0; i < 7; i++)
                {
                DateTime ree = new DateTime();
                ree = DateTime.Now;
                ree = ree.AddDays(i);

                foreach (IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS j in maker.MakeButtonList(ree, boatSelected))
                    {

                    Tuple<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS, BoatEntity> t1 = new Tuple<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS, BoatEntity>(j, boatSelected);

                    Button button = new Button()
                        {

                        Width = 150,
                        Height = (j.length * 42),
                        Tag = t1,
                        Background = Brushes.Red,
                        BorderBrush = Brushes.Black,
                        FontSize = 28,
                        Content = "Reserveer"
                        };
                    button.Tag = t1;
                    button.Click += Button_Clicky;

                    buttons.Children.Add(button);

                    double compare = 0;

                    if (j.startTime.Minute == 30)
                        {
                        compare += 0.5;
                        }

                    compare += j.startTime.Hour;


                    Grid.SetRow(button, checklist.IndexOf(compare));
                    Grid.SetColumn(button, i);
                    Grid.SetRowSpan(button, 50);

                    }

                }

            }
        }
    }
