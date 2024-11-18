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

namespace Kbs.Wpf.Reservation.ViewReservationSpecificPage
{
    /// <summary>
    /// Interaction logic for ViewPageSpecific.xaml
    /// </summary>
    public partial class ViewPageSpecific : Page
    {
        public ViewPageSpecific()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public void Annuleren(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }
}
