using Kbs.Business.BoatType;
using Kbs.Business.Helpers;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.Create.SelectBoatType;

public class SelectBoatTypeBoatTypeViewModel : ViewModel
{
    public SelectBoatTypeBoatTypeViewModel(BoatTypeEntity boatType)
    {
        
        ThrowHelper.ThrowIfNull(boatType);
        Seats = boatType.Seats.ToDutchString();
        Experience = boatType.RequiredExperience.ToDutchString();
        HasSteeringWheel = boatType.HasSteeringWheel;
        Name = boatType.Name;
        BoatTypeId = boatType.BoatTypeId;
    }

    private string _seats;
    private string _experience;
    private bool _hasSteeringWheel;
    private string _name;
    private int _boatTypeId;
    public string Seats
    {
        get => _seats;
        set => SetField(ref _seats, value);
    }
    public string Experience
    {
        get => _experience;
        set => SetField(ref _experience, value);
    }
    public bool HasSteeringWheel
    {
        get => _hasSteeringWheel;
        set => SetField(ref _hasSteeringWheel, value);
    }
    public string HasSteeringWheelString => HasSteeringWheel ? "Ja" : "Nee";
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public int BoatTypeId
    {
        get => _boatTypeId;
        set => SetField(ref _boatTypeId, value);
    }
}