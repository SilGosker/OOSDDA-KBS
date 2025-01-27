﻿using System.Windows;
using System.Windows.Controls;
using Kbs.Business.BoatType;
using Kbs.Business.User;
using Kbs.Data.BoatType;
using Kbs.Wpf.BoatType.Create;
using Kbs.Wpf.BoatType.Read.Details;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.Read.Index;

[HasRole(UserRole.MaterialCommissioner)]
[HighlightFor(typeof(BoatTypeEntity))]
public partial class ViewBoatTypesPage : Page
{

    private readonly INavigationManager _navigationManager;
    private ReadBoatTypeIndexViewModel ReadBoatTypeIndexViewModel => (ReadBoatTypeIndexViewModel)DataContext;
    private readonly BoatTypeRepository _boatTypeRepository = new();
    public ViewBoatTypesPage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();
        foreach (var boatType in _boatTypeRepository.GetAll())
        {
            ReadBoatTypeIndexViewModel.Items.Add(new ReadBoatTypeIndexBoatTypeViewModel(boatType));
        }
    }
        
    private void BoatTypeSelected(object sender, RoutedEventArgs e)
    {
        var listViewItem = (ListViewItem)sender;
        var item = (ReadBoatTypeIndexBoatTypeViewModel)listViewItem.DataContext;
        _navigationManager.Navigate(() => new ReadDetailsBoatTypePage(this._navigationManager, item.Id));
    }

    private void AddBoatType(object sender, RoutedEventArgs e)
    {
        _navigationManager.Navigate(() => new CreateBoatTypePage(_navigationManager));
    }
}