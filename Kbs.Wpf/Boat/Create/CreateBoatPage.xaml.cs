using System.Windows.Controls;
using Kbs.Business.Boat;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Wpf.Boat.Read.Details;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Create;

[HasRole(UserRole.MaterialCommissioner)]
[HighlightFor(typeof(BoatEntity))]
public partial class CreateBoatPage : Page
{
    private CreateBoatViewModel ViewModel => (CreateBoatViewModel)DataContext;
    private readonly BoatRepository _boatRepository = new();
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
        ViewModel.BoatTypeId = type?.BoatTypeId ?? 0;
    }

    private void Submit(object sender, System.Windows.RoutedEventArgs e)
    {
        var boat = new BoatEntity()
        {
            Name = ViewModel.Name,
            BoatTypeId = ViewModel.BoatTypeId,
            Status = BoatStatus.Operational
        };

        var validationResult = new BoatValidator(_boatTypeRepository).ValidateForCreate(boat);

        ViewModel.NameErrorMessage = validationResult.TryGetValue(nameof(boat.Name), out string nameError) ? nameError : string.Empty;
        ViewModel.BoatTypeErrorMessage = validationResult.TryGetValue(nameof(boat.BoatTypeId), out string boatTypeError) ? boatTypeError : string.Empty;

        if (validationResult.Count != 0)
        {
            return;
        }

        _boatRepository.Create(boat);
        _navigationManager.Navigate(() => new ReadDetailsBoatPage(_navigationManager, boat.BoatId));
    }
}