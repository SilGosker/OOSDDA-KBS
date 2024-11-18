using System.Windows.Controls;
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
            ViewModel.BoatTypeNames.Add(boatType.Name);
        }

        UpdateItems();
    }

    private void NameChanged(object sender, TextChangedEventArgs e)
    {
        UpdateItems();
    }

    private void TypeChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        string selected = (string)comboBox.SelectedItem;
        ViewModel.BoatTypeId = ViewModel.BoatTypes
            .FirstOrDefault(boatType => boatType.Name.Equals(selected, StringComparison.CurrentCultureIgnoreCase))?.Id ?? 0;

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