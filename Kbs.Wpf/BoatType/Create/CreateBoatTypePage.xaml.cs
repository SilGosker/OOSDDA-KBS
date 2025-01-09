using System.Windows.Controls;
using Kbs.Business.BoatType;
using Kbs.Business.User;
using Kbs.Data.BoatType;
using Kbs.Wpf.BoatType.Components;
using Kbs.Wpf.BoatType.Read.Details;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.Create;

[HasRole(UserRole.MaterialCommissioner)]
[HighlightFor(typeof(BoatTypeEntity))]
public partial class CreateBoatTypePage : Page
{
    private CreateBoatTypeViewModel ViewModel => (CreateBoatTypeViewModel)DataContext;
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private readonly INavigationManager _navigationManager;
    public CreateBoatTypePage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();

        foreach (var requiredExperience in Enum.GetValues<BoatTypeRequiredExperience>())
        {
            ViewModel.PossibleExperiences.Add(new BoatTypeExperienceViewModel(requiredExperience));
        }

        foreach (BoatTypeSeats seats in Enum.GetValues<BoatTypeSeats>())
        {
            ViewModel.PossibleSeats.Add(new BoatTypeSeatsViewModel(seats));
        }
    }

    private void ExperienceChanged(object sender, SelectionChangedEventArgs e)
    {
        var experience = (BoatTypeExperienceViewModel)((ComboBox)sender).SelectedItem;
        ViewModel.RequiredExperience = experience?.RequiredExperience ?? 0;
    }

    private void SeatsChanged(object sender, SelectionChangedEventArgs e)
    {
        var seats = (BoatTypeSeatsViewModel)((ComboBox)sender).SelectedItem;
        ViewModel.Seats = seats?.BoatTypeSeats ?? 0;
    }

    private void Submit(object sender, System.Windows.RoutedEventArgs e)
    {
        var boatType = new BoatTypeEntity()
        {
            Name = ViewModel.Name,
            HasSteeringWheel = ViewModel.HasSteeringWheel,
            RequiredExperience = ViewModel.RequiredExperience,
            Seats = ViewModel.Seats,
            Speed = ViewModel.Speed
        };

        var validationResult = new BoatTypeValidator().ValidateForCreate(boatType);

        ViewModel.NameErrorMessage = validationResult.TryGetValue(nameof(boatType.Name), out string nameError) ? nameError : string.Empty;
        ViewModel.ExperienceErrorMessage = validationResult.TryGetValue(nameof(boatType.RequiredExperience), out string experienceError) ? experienceError : string.Empty;
        ViewModel.SeatsErrorMessage = validationResult.TryGetValue(nameof(boatType.Seats), out string seatsErrorMessage) ? seatsErrorMessage : string.Empty;
        ViewModel.SpeedErrorMessage = validationResult.TryGetValue(nameof(boatType.Speed), out string speedErrorMessage) ? speedErrorMessage : string.Empty;

        if (validationResult.Count != 0)
        {
            return;
        }

        _boatTypeRepository.Create(boatType);
        _navigationManager.Navigate(() => new ReadDetailsBoatTypePage(_navigationManager, boatType.BoatTypeId));
    }
}