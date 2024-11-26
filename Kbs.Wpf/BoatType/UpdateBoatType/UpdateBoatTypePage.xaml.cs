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

namespace Kbs.Wpf.BoatType.UpdateBoatType
{
    /// <summary>
    /// Interaction logic for UpdateBoatTypePage.xaml
    /// </summary>
    public partial class UpdateBoatTypePage : Page
    {
        private readonly INavigationManager _navigationManager;

        public UpdateBoatTypePage(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            InitializeComponent();
        }
    }
}
