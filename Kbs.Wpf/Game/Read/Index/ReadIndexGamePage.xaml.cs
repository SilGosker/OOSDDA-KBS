using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.Course;
using Kbs.Business.Game;
using Kbs.Business.User;
using Kbs.Data.Course;
using Kbs.Data.Game;
using Kbs.Wpf.Game.Create;
using Kbs.Wpf.Game.Read.Details;

namespace Kbs.Wpf.Game.Read.Index;

[HasRole(UserRole.GameCommissioner)]
public partial class ReadIndexGamePage : Page
{
    private readonly GameRepository _gameRepository = new();
    private readonly CourseRepository _courseRepository = new();
    private readonly INavigationManager _navigationManager;
    private ReadIndexGameViewModel ViewModel => (ReadIndexGameViewModel)DataContext;
    
    public ReadIndexGamePage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        
        foreach (CourseEntity game in _courseRepository.GetAll())
        {
            ViewModel.Courses.Add(new ReadIndexGameCourseViewModel(game));
        }

        UpdateItems();
    }
    
    private void NameChanged(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            UpdateItems();
        }
    }
    
    private void CourseChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        var selected = (ReadIndexGameCourseViewModel)comboBox.SelectedItem;

        if (selected == null) return;
        
        ViewModel.CourseId = ViewModel.Courses
            .FirstOrDefault(course => course.Id == selected.Id)?.Id ?? 0;

        UpdateItems();
    }
    
    private void UpdateItems()
    {
        List<GameEntity> games;

        if (!string.IsNullOrEmpty(ViewModel.Name) && ViewModel.CourseId > 0)
        {
            games = _gameRepository.GetManyByNameAndCourse(ViewModel.Name, ViewModel.CourseId);
        }
        else if (!string.IsNullOrEmpty(ViewModel.Name))
        {
            games = _gameRepository.GetManyByName(ViewModel.Name);
        }
        else if (ViewModel.CourseId > 0)
        {
            games = _gameRepository.GetManyByCourse(ViewModel.CourseId);
        }
        else
        {
            games = _gameRepository.GetMany();
        }

        ViewModel.Items.Clear();
        foreach (var game in games)
        {
            var course = ViewModel.Courses.SingleOrDefault(e => e.Id == game.CourseId);
            ViewModel.Items.Add(new ReadIndexGameGameViewModel(game, course));
        }
    }
    
    private void GameClicked(object sender, MouseButtonEventArgs e)
    {
        var item = (ReadIndexGameGameViewModel)((ListViewItem)sender).DataContext;
        _navigationManager.Navigate(() => new ReadDetailsGamePage(_navigationManager, item.Id));
    }
    
    private void AddGame(object sender, RoutedEventArgs e)
    {
        _navigationManager.Navigate(() => new CreateGamePage(_navigationManager));
    }
}