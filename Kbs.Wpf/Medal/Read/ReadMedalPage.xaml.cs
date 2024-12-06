using Kbs.Business.Medal;
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

namespace Kbs.Wpf.Medal.Read
{
    public partial class ReadMedalPage : Page
    {
        private readonly IMedalRepository _medalRepository;
        //private readonly GameRepository _gameRepository;
        public INavigationManager _navigationManager;
        public ReadMedalPage(INavigationManager _navigationManager)
        {
            InitializeComponent();
            //var skibidi = _medalRepository.
        }
        public void GameSelected(object sender, EventArgs e)
        {
            
        }
    }
}
