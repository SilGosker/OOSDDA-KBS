using System.Collections.ObjectModel;
using Kbs.Business.BoatType;
using Kbs.Wpf.BoatType.Components;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.Create;

public class CreateBoatTypeViewModel : ViewModel
{
    private string _name;
    private BoatTypeRequiredExperience _requiredExperience;
    private BoatTypeSeats _seats;
    private int _speed;
    private bool _hasSteeringWheel;
    private string _nameErrorMessage;
    private string _experienceErrorMessage;
    private string _speedErrorMessage;
    private string _seatsErrorMessage;

    public ObservableCollection<BoatTypeExperienceViewModel> PossibleExperiences { get; } = new();
    public ObservableCollection<BoatTypeSeatsViewModel> PossibleSeats { get; } = new();

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public BoatTypeRequiredExperience RequiredExperience
    {
        get => _requiredExperience;
        set => SetField(ref _requiredExperience, value);
    }

    public int Speed
    {
        get => _speed;
        set => SetField(ref _speed, value);
    }

    public BoatTypeSeats Seats
    {
        get => _seats;
        set => SetField(ref _seats, value);
    }

    public bool HasSteeringWheel
    {
        get => _hasSteeringWheel;
        set => SetField(ref _hasSteeringWheel, value);
    }

    public string NameErrorMessage
    {
        get => _nameErrorMessage;
        set => SetField(ref _nameErrorMessage, value);
    }

    public string ExperienceErrorMessage
    {
        get => _experienceErrorMessage;
        set => SetField(ref _experienceErrorMessage, value);
    }

    public string SpeedErrorMessage
    {
        get => _speedErrorMessage;
        set => SetField(ref _speedErrorMessage, value);
    }

    public string SeatsErrorMessage
    {
        get => _seatsErrorMessage;
        set => SetField(ref _seatsErrorMessage, value);
    }
}