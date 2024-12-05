using Kbs.Business.Course;
using Kbs.Data.Course;
using Kbs.Wpf.Course.Components;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Kbs.Wpf.Course.Read.Details;

namespace Kbs.Wpf.Course.Create
{
    /// <summary>
    /// Interaction logic for CreateCoursePage.xaml
    /// </summary>
    public partial class CreateCoursePage : Page
    {
        private readonly CourseValidator _courseValidator = new();
        private readonly CourseRepository _courseRepository = new();
        private readonly INavigationManager _navigationManager;
        private CreateCourseViewModel ViewModel => (CreateCourseViewModel)DataContext;
        public CreateCoursePage(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            InitializeComponent();

            foreach (CourseDifficulty difficulties in Enum.GetValues<CourseDifficulty>())
            {
                ViewModel.PossibleDifficulties.Add(new CourseDifficultyViewModel(difficulties));
            }
        }

        private void SelectFileButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png",
                Title = "Selecteer een afbeelding"
            };

            if (dialog.ShowDialog() == true)
            {

                ViewModel.ImagePath = dialog.FileName;
            }
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            var course = new CourseEntity()
            {
                Name = ViewModel.Name,
                Description = ViewModel.Description,
                Difficulty = ViewModel.Difficulty
            };
            try
            {
                course.Image = File.ReadAllBytes(ViewModel.ImagePath);
            }
            catch
            {
                // ignored, validation will handle it
            }

            var validationResult = _courseValidator.ValidateForCreate(course);

            if (validationResult.TryGetValue(nameof(course.Image), out string imageError))
            {
                ViewModel.ImageErrorMessage = imageError;
            }
            else
            {
                ViewModel.ImageErrorMessage = string.Empty;
            }

            if (validationResult.TryGetValue(nameof(course.Name), out string nameError))
            {
                ViewModel.NameErrorMessage = nameError;
            }
            else
            {
                ViewModel.NameErrorMessage = string.Empty;
            }

            if (validationResult.Count != 0)
            {
                return;
            }

            _courseRepository.Create(course);
            _navigationManager.Navigate(() => new ReadDetailsCoursePage(course.CourseId));
        }

        private void DifficultyChanged(object sender, SelectionChangedEventArgs e)
        {
            var difficulties = (CourseDifficultyViewModel)((ComboBox)sender).SelectedItem;
            if (difficulties == null)
            {
                ViewModel.Difficulty = 0;
            }
            else
            {
                ViewModel.Difficulty = difficulties.CourseDifficulty;
            }
        }
    }
}
