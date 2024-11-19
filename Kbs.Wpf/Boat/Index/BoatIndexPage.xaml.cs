using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Wpf.Attributes;

namespace Kbs.Wpf.Boat.Index;

[HasRole(Role.MaterialCommissioner)]
public partial class BoatIndexPage : Page
{
    private readonly IBoatRepository _boatRepository = new BoatRepository();
    private readonly IBoatTypeRepository _boatTypeRepository = new BoatTypeRepository();
    public BoatIndexViewModel ViewModel => (BoatIndexViewModel)DataContext;

    public BoatIndexPage()
    {
        InitializeComponent();

        foreach (BoatTypeEntity boatType in _boatTypeRepository.GetAll())
        {
            ViewModel.BoatTypes.Add(new BoatIndexBoatTypeViewModel(boatType));
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
        var selected = (BoatIndexBoatTypeViewModel)comboBox.SelectedItem;

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
            ViewModel.Items.Add(new BoatIndexBoatViewModel(boat, boatType));
        }
    }
}