using System.Windows.Controls;
using Kbs.Business.BoatType;
using Kbs.Business.User;
using Kbs.Data.BoatType;
using Kbs.Wpf.Attributes;
using Kbs.Wpf.Boat.Index;

namespace Kbs.Wpf.BoatType.CreateBoatType;

[HasRole(Role.MaterialCommissioner)]
public partial class CreateBoatTypePage : Page
{
    private CreateBoatTypeViewModel ViewModel => (CreateBoatTypeViewModel)DataContext;
    private readonly IBoatTypeRepository _boatTypeRepository = new BoatTypeRepository();
    private readonly INavigationManager _navigationManager;
    public CreateBoatTypePage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();

        foreach (var requiredExperience in Enum.GetValues<BoatTypeRequiredExperience>())
        {
            ViewModel.PossibleExperiences.Add(new CreateBoatExperienceViewModel(requiredExperience));
        }

        foreach (BoatTypeSeats seats in Enum.GetValues<BoatTypeSeats>())
        {
            ViewModel.PossibleSeats.Add(new CreateBoatSeatsViewModel(seats));
        }
    }

    private void ExperienceChanged(object sender, SelectionChangedEventArgs e)
    {
        var experience = (CreateBoatExperienceViewModel)((ComboBox)sender).SelectedItem;
        if (experience == null) return;

        ViewModel.RequiredExperience = experience.RequiredExperience;
    }

    private void SeatsChanged(object sender, SelectionChangedEventArgs e)
    {
        var experience = (CreateBoatSeatsViewModel)((ComboBox)sender).SelectedItem;
        if (experience == null) return;

        ViewModel.Seats = experience.BoatTypeSeats;

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

        if (validationResult.TryGetValue(nameof(boatType.Name), out string nameError))
        {
            ViewModel.NameErrorMessage = nameError;
        }
        else
        {
            ViewModel.NameErrorMessage = string.Empty;
        }

        if (validationResult.TryGetValue(nameof(boatType.RequiredExperience), out string experienceError))
        {
            ViewModel.ExperienceErrorMessage = experienceError;
        }
        else
        {
            ViewModel.ExperienceErrorMessage = string.Empty;
        }

        if (validationResult.TryGetValue(nameof(boatType.Seats), out string seatsErrorMessage))
        {
            ViewModel.SeatsErrorMessage = seatsErrorMessage;
        }
        else
        {
            ViewModel.SeatsErrorMessage = string.Empty;
        }

        if (validationResult.TryGetValue(nameof(boatType.Speed), out string speedErrorMessage))
        {
            ViewModel.SpeedErrorMessage = speedErrorMessage;
        }
        else
        {
            ViewModel.SpeedErrorMessage = string.Empty;
        }

        if (validationResult.Count != 0)
        {
            return;
        }

        _boatTypeRepository.Create(boatType);

        // TODO: navigate to details page
        _navigationManager.Navigate(() => new BoatIndexPage());
    }
}