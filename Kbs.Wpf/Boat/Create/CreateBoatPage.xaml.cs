using System.Windows.Controls;
using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Wpf.Boat.Index;
using Kbs.Wpf.BoatType.CreateBoatType;

namespace Kbs.Wpf.Boat.Create;

public partial class CreateBoatPage : Page
{
    private CreateBoatViewModel ViewModel => (CreateBoatViewModel)DataContext;
    private readonly IBoatRepository _boatRepository = new BoatRepository();
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private readonly INavigationManager _navigationManager;
    
    public CreateBoatPage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        
        foreach (var boatType in _boatTypeRepository.GetAll())
        {
            ViewModel.PossibleBoatTypes.Add(new CreateBoatBoatTypeViewModel(boatType));
        }
    }

    private void TypeChanged(object sender, SelectionChangedEventArgs e)
    {
        var type = (CreateBoatBoatTypeViewModel)((ComboBox)sender).SelectedItem;
        if (type == null) return;
        
        ViewModel.BoatTypeId = type.BoatTypeId;
    }
    
    private void Submit(object sender, System.Windows.RoutedEventArgs e)
    {
        var boat = new BoatEntity()
        {
            Name = ViewModel.Name,
            BoatTypeId = ViewModel.BoatTypeId
        };

        var validationResult = new BoatValidator(_boatTypeRepository).ValidateForCreate(boat);

        if (validationResult.TryGetValue(nameof(boat.Name), out string nameError))
        {
            ViewModel.NameErrorMessage = nameError;
        }
        else
        {
            ViewModel.NameErrorMessage = string.Empty;
        }

        if (validationResult.TryGetValue(nameof(boat.BoatTypeId), out string boatTypeError))
        {
            ViewModel.BoatTypeErrorMessage = boatTypeError;
        }
        else
        {
            ViewModel.BoatTypeErrorMessage = string.Empty;
        }
        
        if (validationResult.Count != 0)
        {
            return;
        }

        _boatRepository.Create(boat);

        _navigationManager.Navigate(() => new BoatIndexPage(_navigationManager));
    }
}