using System.Windows;
using Kbs.Business.Session;
using Kbs.Data.User;

namespace Kbs.Wpf;

public partial class App : Application
{
    public App()
    {
        SessionManager.Instance = new SessionManager(new UserRepository(), TimeSpan.FromMinutes(15));
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
    }
}