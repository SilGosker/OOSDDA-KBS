using Kbs.Wpf.Game.Read.Details;
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
using System.Windows.Shapes;

namespace Kbs.Wpf.Boat.Read.Details
{
    /// <summary>
    /// Interaction logic for ChangeStatusMaintainingWindow.xaml
    /// </summary>
    public partial class ChangeStatusMaintainingWindow : Window
    {
        private ChangeStatusMaintainingViewModel ViewModel => (ChangeStatusMaintainingViewModel)DataContext;

        public ChangeStatusMaintainingWindow()
        {
            InitializeComponent();
            //ViewModel.Date = DateTime.Now;
        }

        private void Submit(object sender, RoutedEventArgs e)
        {

        }
        private void Cancel(object sender, RoutedEventArgs e) => Close();
        
    }
}
