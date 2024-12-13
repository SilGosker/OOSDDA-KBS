using System.Windows.Controls;
using Kbs.Business.Helpers;

namespace Kbs.Wpf.Components;

public class PageHistoryService
{
    private readonly Stack<Page> _pages = new();

    public bool TryPush(Page page)
    {
        ThrowHelper.ThrowIfNull(page);

        if (_pages.TryPeek(out var currentPage) && page.GetType() == currentPage.GetType())
        {
            return false;
        }

        _pages.Push(page);

        return true;
    }

    public Page Previous()
    {
        if (_pages.Count > 1)
        {
            _pages.TryPop(out var currentPage);
        }

        _pages.TryPeek(out Page previousPage);

        return previousPage;
    }
}