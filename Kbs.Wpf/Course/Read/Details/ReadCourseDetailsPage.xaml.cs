using System.Windows.Controls;
using Kbs.Business.Course;
using Kbs.Data.Course;

namespace Kbs.Wpf.Course.Read.Details;

public partial class ReadCourseDetailsPage : Page
{
    private readonly CourseRepository _courseRepository = new();
    private ReadCourseDetailsViewModel ViewModel => (ReadCourseDetailsViewModel)DataContext;
    public ReadCourseDetailsPage(int id)
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