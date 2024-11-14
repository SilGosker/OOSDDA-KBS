using System.Windows;
using System.Windows.Controls;

namespace Kbs.Wpf
{
    public partial class MainWindow : Window, INavigationManager
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Navigate<TPage>(Func<TPage> creator) where TPage : Page
        {
            var page = creator();

            NavigationFrame.Navigate(page);
        }
    }
}