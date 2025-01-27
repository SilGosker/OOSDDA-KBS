﻿using Kbs.Business.Boat;
using Kbs.Business.Medal;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.Medal;
using Kbs.Data.User;
using Kbs.Wpf.Medal.Components;
using System.Windows;
using System.Windows.Controls;
using Kbs.Wpf.Components;
using Kbs.Wpf.Game.Read.Details;

namespace Kbs.Wpf.Medal.Create;

[HasRole(UserRole.GameCommissioner)]
[HighlightFor(typeof(MedalEntity))]
public partial class CreateMedalPage : Page
{
    private CreateMedalViewModel ViewModel => (CreateMedalViewModel)DataContext;
    private readonly UserRepository _userRepository = new();
    private readonly BoatRepository _boatRepository = new();
    private readonly MedalRepository _medalRepository = new();
    private readonly INavigationManager _navigationManager;
    public CreateMedalPage(INavigationManager navigationManager, int gameId)
    {
        _navigationManager = navigationManager;
        InitializeComponent();

        foreach (UserEntity user in _userRepository.Get())
        {
            ViewModel.Users.Add(new CreateMedalUserViewModel(user));
        }
        ViewModel.SelectedGameId = gameId;
        ViewModel.MedalMaterial.Add(new MedalMaterialViewModel(MedalMaterial.Bronze));
        ViewModel.MedalMaterial.Add(new MedalMaterialViewModel(MedalMaterial.Silver));
        ViewModel.MedalMaterial.Add(new MedalMaterialViewModel(MedalMaterial.Gold));

        foreach (BoatEntity boat in _boatRepository.GetManyByGame(ViewModel.SelectedGameId))
        {
            ViewModel.Boats.Add(new CreateMedalBoatViewModel(boat));
        }

    }

    private void ComboBoxBoats_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        var selected = (CreateMedalBoatViewModel)comboBox.SelectedItem;

        if (selected == null) return;
        ViewModel.SelectedBoat = selected;

    }

    private void ComboBoxUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        var selected = (CreateMedalUserViewModel)comboBox.SelectedItem;

        if (selected == null) return;
        ViewModel.SelectedUser = selected;
    }

    private void ComboBoxMedalMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        var selected = (MedalMaterialViewModel)comboBox.SelectedItem;
        if (selected == null) return;
        ViewModel.SelectedMaterial = selected;
    }

    private void ButtonRewardMedal_Click(object sender, RoutedEventArgs e)
    {
        var medal = new MedalEntity();
        medal.Material = ViewModel.SelectedMaterial.MedalMaterial;
        medal.UserId = ViewModel.SelectedUser.UserId;
        medal.BoatId = ViewModel.SelectedBoat?.BoatId;
        medal.GameId = ViewModel.SelectedGameId;

        var validationResult = new MedalValidator().ValidateForCreate(medal);

        // else is not necessary since its functionally impossible to reach
        if (validationResult.Count == 0)
        {
            _medalRepository.Create(medal);

            _navigationManager.Navigate(() => new ReadDetailsGamePage(_navigationManager, ViewModel.SelectedGameId));
        }


    }
}
