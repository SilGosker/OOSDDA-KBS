using Kbs.Business.Reservation;
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
using Kbs.Data;
using Kbs.Data.Reservation;
using System.ComponentModel;
using System.Diagnostics;
using Kbs.Wpf.Reservation.MakeReservation.SelectTime;
using Kbs.Business.BoatType;
using Kbs.Data.BoatType;

namespace Kbs.Wpf.Reservation.NewFolder
    {
    /// <summary>
    /// Interaction logic for MakeReservation.xaml
    /// </summary>
    public partial class MakeReservation : Page
        {
        List<BoatTypeEntity> types;
        MainWindow mainWindow;

        public MakeReservation(MainWindow maindWindow)
            {
            InitializeComponent();
            this.mainWindow = maindWindow;


            ReservationRepository repo = new ReservationRepository();
            types = repo.GetAllTypesWithBoats();

            foreach (BoatTypeEntity type in types)
                {

                Button button = new Button()
                    {

                    Width = 550,
                    //Height = 120,
                    Margin = new Thickness(10),
                    Tag = type,
                    Background = Brushes.Gray,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(5),
                    FontSize = 35,
                    Foreground = Brushes.White,
                    Content = "\t" + type.Name+"\nSnelheid: " + type.Speed + " Niveau: " + type.RequiredExperience + "\nZitplaatsen: " + type.Seats + " Stuur: " + type.HasSteeringWheel
                    };
                button.Tag = type;
                button.Click += Button_Click;
                BoatTypeContainer.Items.Add(button);
                
                    
                }

            }

        private void Button_Click(object sender, RoutedEventArgs e)
            {
            Button send = (Button)sender;
            BoatTypeEntity boatType = (BoatTypeEntity)send.Tag;
            var selectTime = new SelectTime(mainWindow, boatType);
            mainWindow.NavigationFrame.Content = selectTime;
            }
        }
    }
