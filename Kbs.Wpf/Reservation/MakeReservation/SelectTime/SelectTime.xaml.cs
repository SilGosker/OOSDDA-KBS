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

namespace Kbs.Wpf.Reservation.MakeReservation.SelectTime
    {
    /// <summary>
    /// Interaction logic for SelectTime.xaml
    /// </summary>
    public partial class SelectTime : Page
        {
        MainWindow mainWindow;
        BoatTypeTEMPORARYIFNEEDEDMAKEANEWONEPLSTHISISFORMYSANITYANDTESTINGIFIAMCOMPETENDliefsjonathan boatType;
        public SelectTime(MainWindow mainWindow, BoatTypeTEMPORARYIFNEEDEDMAKEANEWONEPLSTHISISFORMYSANITYANDTESTINGIFIAMCOMPETENDliefsjonathan boatType)
            {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.boatType = boatType;
            }
        }
    }
