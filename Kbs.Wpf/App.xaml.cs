using System.Configuration;
using System.Data;
using System.Windows;
using Kbs.Business.Session;
using Kbs.Data.User;

namespace Kbs.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            SessionManager.Instance = new SessionManager(new UserRepository());
        }
    }

}
