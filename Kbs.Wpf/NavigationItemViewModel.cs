using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Wpf.Components;

namespace Kbs.Wpf;

public delegate void NavigationDelegate(object sender, EventArgs args);
public class NavigationItemViewModel : ViewModel
{
    private readonly Func<Page> _pageCreator;
    private readonly INavigationManager _navigationManager;
    public NavigationItemViewModel(INavigationManager navigationManager, Func<Page> pageCreator)
    {
        _pageCreator = pageCreator;
        _navigationManager = navigationManager;
        Navigate = new RelayCommand<NavigationItemViewModel>(_ =>
        {
            _navigationManager.Navigate(_pageCreator);
        });
    }

    public string Name { get; set; }

    public ICommand Navigate { get; set; } 

}