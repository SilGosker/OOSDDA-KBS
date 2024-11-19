using System.Windows;
using Kbs.Business.Session;
using Kbs.Data.User;

namespace Kbs.Wpf;

public partial class App : Application
{
    public App()
    {
        SessionManager.Instance = new SessionManager(new UserRepository(), TimeSpan.FromSeconds(30));
    }
}