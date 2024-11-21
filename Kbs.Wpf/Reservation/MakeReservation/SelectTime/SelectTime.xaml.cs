using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Data.Boat;
using Kbs.Data.Reservation;
using Kbs.Wpf.Reservation.MakeReservation.SelectLength;
using System;
using System.Collections.Generic;
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
        List<DateTime> thisWeek = new List<DateTime>();
        BoatRepository boatRepository = new BoatRepository();
        ReservationMaker maker = new ReservationMaker(new ReservationRepository());
        public SelectTime(MainWindow mainWindow, BoatTypeEntity boatType)
            {

            List<double> checklist = new List<double>();
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
            List<BoatEntity> boats = boatRepository.GetManyByType(boatType.BoatTypeID);
            BoatEntity boat = boats[0];

            for(int i = 0; i < 7; i++)
                {
                DateTime ree = new DateTime();
                ree = DateTime.Now;
                ree = ree.AddDays(i);
                thisWeek.Add(ree);

                foreach (IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS j in maker.MakeButtonList(ree, boat)) {

                    Tuple<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS, BoatEntity> t1 = new Tuple<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS, BoatEntity>(j, boat);

                    Button button = new Button()
                        {
                        
                        Width = 150,
                        Height = (j.length*42),
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
                
            
            DataContext = thisWeek;
            }

        public void Button_Clicky(object sender, RoutedEventArgs e)
            {
            
            Button send = (Button)sender;
            Tuple<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS, BoatEntity> chosenTimeAndBoat = (Tuple<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS, BoatEntity>)send.Tag;
            SelectLength.SelectLength selectLength = new SelectLength.SelectLength(mainWindow, chosenTimeAndBoat);
            mainWindow.NavigationFrame.Content = selectLength;
            }
        }
    }
