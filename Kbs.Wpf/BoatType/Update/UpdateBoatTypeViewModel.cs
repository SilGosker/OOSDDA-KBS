using System.Collections.ObjectModel;
using Kbs.Business.BoatType;
using Kbs.Wpf.BoatType.Components;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.Update;

public class UpdateBoatTypeViewModel : ViewModel
{
    private string _name;
    private int _speed;
    private bool _hasSteeringWheel;
    private string _experienceErrorMessage;
    private string _speedErrorMessage;
    private string _seatsErrorMessage;
    private BoatTypeExperienceViewModel _selectedExperience;
    private BoatTypeSeatsViewModel _selectedSeats;
    
    
    public ObservableCollection<BoatTypeExperienceViewModel> PossibleExperiences { get; } = [];
    public ObservableCollection<BoatTypeSeatsViewModel> PossibleSeats { get; } = [];

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }
    
    public string BoatTypeNameString => $"Boottype: {Name}";

    public int Speed
    {
        get => _speed;
        set => SetField(ref _speed, value);
    }

    public bool HasSteeringWheel
    {
        get => _hasSteeringWheel;
        set => SetField(ref _hasSteeringWheel, value);
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
    
    public string NameErrorMessage
    {
        get => _speedErrorMessage;
        set => SetField(ref _speedErrorMessage, value);
    }

    public string SeatsErrorMessage
    {
        get => _seatsErrorMessage;
        set => SetField(ref _seatsErrorMessage, value);
    }
    
    public BoatTypeExperienceViewModel SelectedExperience
    {
        get => _selectedExperience;
        set => SetField(ref _selectedExperience, value);
    }

    public BoatTypeSeatsViewModel SelectedSeats
    {
        get => _selectedSeats;
        set => SetField(ref _selectedSeats, value);
    }
}