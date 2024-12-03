using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Data.Damage;
using Kbs.Wpf.Boat.Create;
using Kbs.Wpf.Boat.Read.Details;

namespace Kbs.Wpf.Boat.Read.Index;

[HasRole(UserRole.MaterialCommissioner)]
public partial class ReadIndexBoatPage : Page
{
    private readonly BoatRepository _boatRepository = new();
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private readonly INavigationManager _navigationManager;
    private readonly DamageRepository _damageRepository = new();
    private ReadIndexBoatViewModel ViewModel => (ReadIndexBoatViewModel)DataContext;
    public ReadIndexBoatPage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();

        foreach (BoatTypeEntity boatType in _boatTypeRepository.GetAll())
        {
            ViewModel.BoatTypes.Add(new ReadIndexBoatBoatTypeViewModel(boatType));
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

    private void TypeChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        var selected = (ReadIndexBoatBoatTypeViewModel)comboBox.SelectedItem;

        if (selected == null) return;
        
        ViewModel.BoatTypeId = ViewModel.BoatTypes
            .FirstOrDefault(boatType => boatType.Id == selected.Id)?.Id ?? 0;

        UpdateItems();
    }

    private void UpdateItems()
    {
        List<BoatEntity> boats;

        if (!string.IsNullOrEmpty(ViewModel.Name) && ViewModel.BoatTypeId > 0)
        {
            boats = _boatRepository.GetManyByNameAndType(ViewModel.Name, ViewModel.BoatTypeId);
        }
        else if (!string.IsNullOrEmpty(ViewModel.Name))
        {
            boats = _boatRepository.GetManyByName(ViewModel.Name);
        }
        else if (ViewModel.BoatTypeId > 0)
        {
            boats = _boatRepository.GetManyByType(ViewModel.BoatTypeId);
        }
        else
        {
            boats = _boatRepository.GetMany();
        }

        ViewModel.Items.Clear();
        foreach (var boat in boats)
        {
            var boatType = ViewModel.BoatTypes.SingleOrDefault(e => e.Id == boat.BoatTypeId);
            ViewModel.Items.Add(new ReadIndexBoatBoatViewModel(boat, boatType, _damageRepository.HasDamage(boat)));
        }
    }

    private void BoatClicked(object sender, MouseButtonEventArgs e)
    {
        var item = (ReadIndexBoatBoatViewModel)((ListViewItem)sender).DataContext;
        _navigationManager.Navigate(() => new ReadDetailsBoatPage(_navigationManager, item.BoatId));
    }
    
    private void CreateBoatButtonClicked(object sender, RoutedEventArgs e)
    {
        _navigationManager.Navigate(() => new CreateBoatPage(_navigationManager));
    }
}