﻿using Kbs.Business.BoatType;
using Kbs.Business.Helpers;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Index;

public class BoatIndexBoatTypeViewModel : ViewModel
{
    public BoatIndexBoatTypeViewModel(BoatTypeEntity boatType)
    {
        ThrowHelper.ThrowIfNull(boatType);
        Name = boatType.Name;
        Id = boatType.BoatTypeID;
    }
    private string _name;
    private int _id;

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public int Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    // Override ToString to display the name in the dropdown
    public override string ToString()
    {
        return Name;
    }
}