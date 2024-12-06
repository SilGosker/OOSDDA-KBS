using System.Collections.ObjectModel;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Course.Read.Index;

public class ReadIndexCourseViewModel : ViewModel
{
    public ObservableCollection<ReadIndexCourseCourseViewModel> Items { get; } = new();
}