using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Course.Read.Index;

public class ReadCourseIndexViewModel : ViewModel
{
    public ObservableCollection<ReadCourseIndexCourseViewModel> Items { get; } = new();
}