using System.Windows;
using System.Windows.Controls;
using Kbs.Business.BoatType;
using Kbs.Business.User;
using Kbs.Data.BoatType;
using Kbs.Wpf.Attributes;
using Kbs.Wpf.BoatType.CreateBoatType;

namespace Kbs.Wpf.BoatType.Update;

[HasRole(Role.MaterialCommissioner)]
public partial class UpdateBoatTypePage : Page
{
    private readonly BoatTypeValidator _boatTypeValidator = new();
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private readonly INavigationManager _navigationManager;
    private UpdateBoatTypeViewModel ViewModel => (UpdateBoatTypeViewModel)DataContext;
    private readonly BoatTypeEntity _boatType;
    
    public UpdateBoatTypePage(INavigationManager navigationManager, int boatTypeId)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        
        _boatType = _boatTypeRepository.GetById(boatTypeId);
        
        foreach (var requiredExperience in Enum.GetValues<BoatTypeRequiredExperience>())
        {
            ViewModel.PossibleExperiences.Add(new BoatTypeExperienceViewModel(requiredExperience));
        }
        foreach (BoatTypeSeats seats in Enum.GetValues<BoatTypeSeats>())
        {
            ViewModel.PossibleSeats.Add(new BoatTypeSeatsViewModel(seats));
        }
        
        ViewModel.Name = _boatType.Name;
        ViewModel.Speed = _boatType.Speed;
        ViewModel.HasSteeringWheel = _boatType.HasSteeringWheel;
        
        ViewModel.SelectedExperience = ViewModel.PossibleExperiences.FirstOrDefault(e => e.RequiredExperience == _boatType.RequiredExperience);
        ViewModel.SelectedSeats = ViewModel.PossibleSeats.FirstOrDefault(s => s.BoatTypeSeats == _boatType.Seats);
    }
    
    private void Submit(object sender, RoutedEventArgs e)
    {
        if((ViewModel.Name == null || ViewModel.Name == _boatType.Name)
           && ViewModel.SelectedExperience?.RequiredExperience == _boatType.RequiredExperience 
           && ViewModel.Speed == _boatType.Speed 
           && ViewModel.SelectedSeats?.BoatTypeSeats == _boatType.Seats 
           && ViewModel.HasSteeringWheel == _boatType.HasSteeringWheel)
        {
            _navigationManager.Navigate(() => new Page()); //todo: replace with BoatTypeIndexPage or BoatTypeDetailPage
            return;
        }
        
        _boatType.Name = ViewModel.Name;
        _boatType.RequiredExperience = ViewModel.SelectedExperience?.RequiredExperience ?? 0;
        _boatType.Speed = ViewModel.Speed;
        _boatType.Seats = ViewModel.SelectedSeats?.BoatTypeSeats ?? 0;
        _boatType.HasSteeringWheel = ViewModel.HasSteeringWheel;
        
        var validationResult = _boatTypeValidator.ValidatorForUpdate(_boatType);
        
        // Add error messages to the viewmodel properties
        ViewModel.ExperienceErrorMessage = validationResult.TryGetValue(nameof(_boatType.RequiredExperience), out string experienceError) ? experienceError : string.Empty;
        ViewModel.SeatsErrorMessage = validationResult.TryGetValue(nameof(_boatType.Seats), out string seatsErrorMessage) ? seatsErrorMessage : string.Empty;
        ViewModel.SpeedErrorMessage = validationResult.TryGetValue(nameof(_boatType.Speed), out string speedErrorMessage) ? speedErrorMessage : string.Empty;
        ViewModel.NameErrorMessage = validationResult.TryGetValue(nameof(_boatType.Name), out string nameErrorMessage) ? nameErrorMessage : string.Empty;
        
        // When no errors are shown
        if (validationResult.Count == 0)
        {
            _boatTypeRepository.Update(_boatType);
            MessageBox.Show("Boottype succesvol aangepast.");
            _navigationManager.Navigate(() => new Page()); //todo: replace with BoatTypeIndexPage or BoatTypeDetailPage
        }
    }
}