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

namespace Kbs.Wpf.Reservation.NewFolder
    {
    /// <summary>
    /// Interaction logic for MakeReservation.xaml
    /// </summary>
    public partial class MakeReservation : Page
        {
        List<BoatTypeTEMPORARYIFNEEDEDMAKEANEWONEPLSTHISISFORMYSANITYANDTESTINGIFIAMCOMPETENDliefsjonathan> types;
        Window mainWindow;

        public MakeReservation(Window maindWindow)
            {
            InitializeComponent();
            this.mainWindow = maindWindow;
            

            ReservationRepository repo = new ReservationRepository();
            types = repo.GetTypesPLS();

            foreach (BoatTypeTEMPORARYIFNEEDEDMAKEANEWONEPLSTHISISFORMYSANITYANDTESTINGIFIAMCOMPETENDliefsjonathan type in types)
                {

                Button button = new Button()
                    {

                    //Width = 220,
                    //Height = 120,
                    Margin = new Thickness(10),
                    Tag = type,
                    Background = Brushes.Red,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(5),
                    FontSize = 35,
                    Content = type.Name+"\n Snelheid: " + type.Speed + " Niveau: " + type.RequiredExperience + "\n Zitplaatsen: " + type.Seats + " Stuur: " + type.HasSteeringwheel
                    };
                button.Tag = type;
                button.Click += Button_Click;
                BoatTypeContainer.Items.Add(button);
                
                    
                }

            }

        private void Button_Click(object sender, RoutedEventArgs e)
            {
            throw new NotImplementedException();
            }
        }
    }
