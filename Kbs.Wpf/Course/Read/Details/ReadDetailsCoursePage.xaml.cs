using System.Windows.Controls;
using Kbs.Business.Course;
using Kbs.Data.Course;

namespace Kbs.Wpf.Course.Read.Details;

public partial class ReadDetailsCoursePage : Page
{
    private readonly CourseRepository _courseRepository = new();
    private ReadDetailsCourseViewModel ViewModel => (ReadDetailsCourseViewModel)DataContext;
    public ReadDetailsCoursePage(int id)
    {
        InitializeComponent();
        var course = _courseRepository.GetById(id);

        ViewModel.Name = course.Name;
        ViewModel.Description = course.Description;
        ViewModel.Image = course.Image;
        ViewModel.Difficulty = course.Difficulty.ToDutchString();
        ViewModel.Id = course.CourseId;
    }
}