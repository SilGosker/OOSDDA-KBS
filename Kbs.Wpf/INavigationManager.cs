using System.Windows.Controls;

namespace Kbs.Wpf;

public interface INavigationManager
{
    public void Navigate<TPage>(Func<TPage> creator) where TPage : Page;
}