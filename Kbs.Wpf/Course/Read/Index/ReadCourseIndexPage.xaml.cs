using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Data.Parcours;
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

        foreach (var parcours in _courseRepository.GetAll())
        {
            ViewModel.Items.Add(new ReadCourseIndexCourseViewModel(parcours));
        }
    }

    private void ParcourClicked(object sender, MouseButtonEventArgs e)
    {
        var listItem = (ListViewItem)sender;
        var dataContext = (ReadCourseIndexCourseViewModel)listItem.DataContext;

        _navigationManager.Navigate(() => new ReadCourseDetailsPage(dataContext.ParcourId));
    }
}