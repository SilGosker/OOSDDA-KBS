using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

    private string _name;
    private bool _startsNewSection;
    private bool _isHighlighted;
    public Type Type { get; set; }
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }
    public bool StartsNewSection
    {
        get => _startsNewSection;
        set => SetField(ref _startsNewSection, value);
    }
    public ICommand Navigate { get; set; }
    public bool IsHighlighted
    {
        get => _isHighlighted;
        set => SetField(ref _isHighlighted, value);
    }
}