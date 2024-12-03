using Kbs.Business.User;
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

namespace Kbs.Wpf.User.ViewUser.ViewUserDetail
{
    /// <summary>
    /// Interaction logic for ViewUserValuesDetailedPage.xaml
    /// </summary>
    public partial class ViewUserValuesDetailedPage : Page
    {
        public ViewUserValuesDetailedPage(INavigationManager nav, int user )
        {
            InitializeComponent();
        }
    }
}
