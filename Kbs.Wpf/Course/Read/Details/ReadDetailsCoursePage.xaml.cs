using System.IO;
using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Course;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Data.Course;
using Kbs.Wpf.Boat.Read.Index;
using Kbs.Wpf.Course.Read.Index;
using Kbs.Wpf.Reservation.Read.Index;
using Microsoft.Win32;

namespace Kbs.Wpf.Course.Read.Details;

public partial class ReadDetailsCoursePage : Page
{
    private readonly CourseRepository _courseRepository = new();
    private ReadDetailsCourseViewModel ViewModel => (ReadDetailsCourseViewModel)DataContext;
    private readonly INavigationManager _navigationManager;
    private readonly CourseValidator _courseValidator = new();
    public ReadDetailsCoursePage(int id, INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        var course = _courseRepository.GetById(id);

        ViewModel.Name = course.Name;
        ViewModel.Description = course.Description;
        ViewModel.Image = course.Image;
        ViewModel.Id = course.CourseId;

        foreach (CourseDifficulty difficulty in Enum.GetValues<CourseDifficulty>())
        {
            var model = new ReadDetailsCourseDifficultyViewModel(difficulty);
            ViewModel.Difficulties.Add(model);
            if (difficulty == course.Difficulty)
            {
                ViewModel.SelectedDifficulty = model;
            }
        }
    }

    private void SelectFile(object sender, System.Windows.RoutedEventArgs e)
    {
        var fileDialog = new OpenFileDialog
        {
            Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg"
        };

        var success = fileDialog.ShowDialog();
        if (success == true)
        {
            ViewModel.Image = File.ReadAllBytes(fileDialog.FileName);
        }
    }
    private void Remove(object sender, RoutedEventArgs e)
    {
        var entity = _courseRepository.GetById(ViewModel.Id);
        int id = entity.CourseId;
        MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (MessageBoxResult.Yes == result)
        {
            _courseRepository.Delete(id);
            _navigationManager.Navigate(() => new ReadIndexCoursePage(_navigationManager));
        }
    }

    private void Submit(object sender, RoutedEventArgs e)
    {
        var course = new CourseEntity
        {
            CourseId = ViewModel.Id,
            Name = ViewModel.Name,
            Description = ViewModel.Description,
            Difficulty = ViewModel.SelectedDifficulty?.Difficulty ?? default,
            Image = ViewModel.Image
        };

        var validationResult = _courseValidator.ValidateForUpdate(course);

        if (validationResult.TryGetValue(nameof(course.Name), out string nameErrorMessage))
        {
            ViewModel.NameErrorMessage = nameErrorMessage;
        }
        else
        {
            ViewModel.NameErrorMessage = null;
        }

        if (validationResult.TryGetValue(nameof(course.Image), out string imageErrorMessage))
        {
            ViewModel.ImageErrorMessage = imageErrorMessage;
        }
        else
        {
            ViewModel.ImageErrorMessage = null;
        }

        if (validationResult.TryGetValue(nameof(course.Difficulty), out string difficultyErrorMessage))
        {
            ViewModel.DifficultyErrorMessage = difficultyErrorMessage;
        }
        else
        {
            ViewModel.DifficultyErrorMessage = null;
        }

        if (validationResult.Count != 0)
        {
            return;
        }

        _courseRepository.Update(course);
        MessageBox.Show("Veranderingen zijn succesvol opgeslagen");
        _navigationManager.Navigate(() => new ReadDetailsCoursePage(course.CourseId, this._navigationManager));
    }
}