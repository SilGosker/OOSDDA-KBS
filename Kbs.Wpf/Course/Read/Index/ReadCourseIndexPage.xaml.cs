using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Data.Course;
using Kbs.Wpf.Course.Read.Details;

namespace Kbs.Wpf.Course.Read.Index;

public partial class ReadCourseIndexPage : Page
{
    private readonly INavigationManager _navigationManager;
    private readonly CourseRepository _courseRepository = new();
    private ReadCourseIndexViewModel ViewModel => (ReadCourseIndexViewModel)DataContext;
    public ReadCourseIndexPage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();

        foreach (var course in _courseRepository.GetAll())
        {
            ViewModel.Items.Add(new ReadCourseIndexCourseViewModel(course));
        }
    }

    private void CourseClicked(object sender, MouseButtonEventArgs e)
    {
        var listItem = (ListViewItem)sender;
        var dataContext = (ReadCourseIndexCourseViewModel)listItem.DataContext;

        _navigationManager.Navigate(() => new ReadCourseDetailsPage(dataContext.CourseId));
    }
}