using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.User;
using Kbs.Data.Course;
using Kbs.Wpf.Course.Create;
using Kbs.Wpf.Course.Read.Details;

namespace Kbs.Wpf.Course.Read.Index;

[HasRole(UserRole.GameCommissioner)]
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

        _navigationManager.Navigate(() => new ReadDetailsCoursePage(dataContext.CourseId, _navigationManager));
    }
    
    private void CreateCourseButtonClicked(object sender, RoutedEventArgs e)
    {
        _navigationManager.Navigate(() => new CreateCoursePage(_navigationManager));
    }
}