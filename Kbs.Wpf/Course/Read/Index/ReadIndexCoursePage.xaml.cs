using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Data.Course;
using Kbs.Wpf.Course.Read.Details;

namespace Kbs.Wpf.Course.Read.Index;

public partial class ReadIndexCoursePage : Page
{
    private readonly INavigationManager _navigationManager;
    private readonly CourseRepository _courseRepository = new();
    private ReadIndexCourseViewModel ViewModel => (ReadIndexCourseViewModel)DataContext;
    public ReadIndexCoursePage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();

        foreach (var course in _courseRepository.GetAll())
        {
            ViewModel.Items.Add(new ReadIndexCourseCourseViewModel(course));
        }
    }

    private void CourseClicked(object sender, MouseButtonEventArgs e)
    {
        var listItem = (ListViewItem)sender;
        var dataContext = (ReadIndexCourseCourseViewModel)listItem.DataContext;

        _navigationManager.Navigate(() => new ReadDetailsCoursePage(dataContext.CourseId));
    }
}