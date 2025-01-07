using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Wpf.Components;

namespace Kbs.Wpf;

public class NavigationItemViewModel : ViewModel
{
    public NavigationItemViewModel(INavigationManager navigationManager, Func<Page> pageCreator)
    {
        Navigate = new RelayCommand<NavigationItemViewModel>(_ =>
        {
            navigationManager.Navigate(pageCreator);
        });
    }

    public string Name { get; set; }
    public bool StartsNewSection { get; set; }
    public ICommand Navigate { get; set; } 

}