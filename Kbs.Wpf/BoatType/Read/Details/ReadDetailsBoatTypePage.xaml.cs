﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Business.BoatType;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Wpf.Boat.Read.Details;
using Kbs.Wpf.BoatType.Components;
using Kbs.Wpf.BoatType.Read.Index;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.Read.Details;

[HasRole(UserRole.MaterialCommissioner)]
[HighlightFor(typeof(BoatTypeEntity))]
public partial class ReadDetailsBoatTypePage : Page
{
    private readonly INavigationManager _navigationManager;
    private readonly BoatTypeRepository _boatTypeRepository = new();
    private readonly BoatRepository _boatRepository = new();
    private readonly BoatTypeValidator _boatTypeValidator = new();
    private ReadDetailsBoatTypeViewModel ReadDetailsBoatTypeViewModel => (ReadDetailsBoatTypeViewModel)DataContext;
    public ReadDetailsBoatTypePage(INavigationManager navigationManager, int boatTypeId)
    {
        this._navigationManager = navigationManager;
        InitializeComponent();
        var boatType = _boatTypeRepository.GetByBoatTypeID(boatTypeId);
        ReadDetailsBoatTypeViewModel.Speed = boatType.Speed;
        ReadDetailsBoatTypeViewModel.BoatTypeId = boatType.BoatTypeId;
        ReadDetailsBoatTypeViewModel.Experience = boatType.RequiredExperience;
        ReadDetailsBoatTypeViewModel.HasWheel = boatType.HasSteeringWheel;
        ReadDetailsBoatTypeViewModel.Seats = boatType.Seats;
        ReadDetailsBoatTypeViewModel.Name = boatType.Name;

        var boats = _boatRepository.GetManyByType(boatTypeId);
        foreach (var boat in boats)
        {
            ReadDetailsBoatTypeViewModel.Items.Add(new ReadDetailsBoatTypeBoatViewModel(boat));
        }
        foreach (var requiredExperience in Enum.GetValues<BoatTypeRequiredExperience>())
        {
            var experienceViewModel = new BoatTypeExperienceViewModel(requiredExperience);
            ReadDetailsBoatTypeViewModel.PossibleExperiences.Add(experienceViewModel);
            if (requiredExperience == ReadDetailsBoatTypeViewModel.Experience)
            {
                ReadDetailsBoatTypeViewModel.SelectedBoatTypeExperience = experienceViewModel;
            }
        }
        foreach (var seats in Enum.GetValues<BoatTypeSeats>())
        {
            var seatsViewModel = new BoatTypeSeatsViewModel(seats);
            ReadDetailsBoatTypeViewModel.PossibleSeats.Add(seatsViewModel);
            if (seats == ReadDetailsBoatTypeViewModel.Seats)
            {
                ReadDetailsBoatTypeViewModel.SelectedBoatTypeSeats = seatsViewModel;
            }
        }
    }
    public void Refresh()
    {
        _navigationManager.Navigate(() => new ViewBoatTypesPage(_navigationManager));
    }

    private void RemoveBoatType(object sender, RoutedEventArgs e)
    {
        BoatTypeEntity entity = _boatTypeRepository.GetByBoatTypeID(ReadDetailsBoatTypeViewModel.BoatTypeId);
        MessageBoxResult result = MessageBox.Show("Weet u het zeker?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            _boatTypeRepository.Delete(entity);
            this.Refresh();
        }
    }

    private void UpdateData(object sender, RoutedEventArgs e)
    {
        BoatTypeEntity boatType = new BoatTypeEntity()
        {
            Name = ReadDetailsBoatTypeViewModel.Name,
            BoatTypeId = ReadDetailsBoatTypeViewModel.BoatTypeId,
            HasSteeringWheel = ReadDetailsBoatTypeViewModel.HasWheel,
            RequiredExperience = ReadDetailsBoatTypeViewModel.Experience,
            Seats = ReadDetailsBoatTypeViewModel.Seats,
            Speed = ReadDetailsBoatTypeViewModel.Speed
        };

        var validationResult = _boatTypeValidator.ValidatorForUpdate(boatType);
        if (validationResult.Count > 0)
        {
            ReadDetailsBoatTypeViewModel.NameError = validationResult.TryGetValue(nameof(boatType.Name), out string nameError) ? nameError : string.Empty;
            ReadDetailsBoatTypeViewModel.SeatsError = validationResult.TryGetValue(nameof(boatType.Seats), out string seatsError) ? seatsError : string.Empty;
            ReadDetailsBoatTypeViewModel.ExperienceError = validationResult.TryGetValue(nameof(boatType.RequiredExperience), out string experienceError) ? experienceError : string.Empty;
            ReadDetailsBoatTypeViewModel.SpeedError = validationResult.TryGetValue(nameof(boatType.Speed), out string speedError) ? speedError : string.Empty;
        }
        else
        {
            _boatTypeRepository.Update(boatType);
            MessageBox.Show($"{boatType.Name} succesvol geüpdatet");
            this.Refresh();
        }
    }
    private void BoatSelected(object sender, MouseButtonEventArgs e)
    {
        var row = (DataGridRow)sender;
        if (row == null)
        {
            return;
        }

        var dataContext = (ReadDetailsBoatTypeBoatViewModel)row.DataContext;
        _navigationManager.Navigate(() => new ReadDetailsBoatPage(_navigationManager, dataContext.BoatId));
    }

    private void SeatsChanged(object sender, SelectionChangedEventArgs e)
    {
        var type = (BoatTypeSeatsViewModel)((ComboBox)sender).SelectedItem;
        ReadDetailsBoatTypeViewModel.Seats = type?.BoatTypeSeats ?? BoatTypeSeats.One;
    }

    private void ExperienceChanged(object sender, SelectionChangedEventArgs e)
    {
        var type = (BoatTypeExperienceViewModel)((ComboBox)sender).SelectedItem;
        ReadDetailsBoatTypeViewModel.Experience = type?.RequiredExperience ?? BoatTypeRequiredExperience.Beginner;
    }
}