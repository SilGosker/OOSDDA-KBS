using System.Windows.Controls;
using Kbs.Business.Course;
using Kbs.Data.Parcours;

namespace Kbs.Wpf.Course.Read.Details;

public partial class ReadCourseDetailsPage : Page
{
    private readonly CourseRepository _courseRepository = new();
    private ReadCourseDetailsViewModel ViewModel => (ReadCourseDetailsViewModel)DataContext;
    public ReadCourseDetailsPage(int id)
    {
        InitializeComponent();
        var parcours = _courseRepository.GetById(id);

        ViewModel.Name = parcours.Name;
        ViewModel.Description = parcours.Description;
        ViewModel.Image = parcours.Image;
        ViewModel.Difficulty = parcours.Difficulty.ToDutchString();
        ViewModel.Id = parcours.ParcoursId;
    }
}