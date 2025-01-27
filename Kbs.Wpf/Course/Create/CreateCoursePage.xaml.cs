﻿using Kbs.Business.Course;
using Kbs.Data.Course;
using Kbs.Wpf.Course.Components;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Kbs.Wpf.Course.Read.Details;
using Kbs.Business.User;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Course.Create;

[HasRole(UserRole.GameCommissioner)]
[HighlightFor(typeof(CourseEntity))]
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

        ViewModel.ImageErrorMessage = validationResult.TryGetValue(nameof(course.Image), out string imageError) ? imageError : string.Empty;
        ViewModel.NameErrorMessage = validationResult.TryGetValue(nameof(course.Name), out string nameError) ? nameError : string.Empty;
        ViewModel.DifficultyErrorMessage = validationResult.TryGetValue(nameof(course.Difficulty), out string difficultyError) ? difficultyError : string.Empty;

        if (validationResult.Count != 0)
        {
            return;
        }

        _courseRepository.Create(course);
        _navigationManager.Navigate(() => new ReadDetailsCoursePage(course.CourseId, _navigationManager));
    }

    private void DifficultyChanged(object sender, SelectionChangedEventArgs e)
    {
        var difficulties = (CourseDifficultyViewModel)((ComboBox)sender).SelectedItem;
        ViewModel.Difficulty = difficulties?.CourseDifficulty ?? 0;
    }
}