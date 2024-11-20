using System.Collections.ObjectModel;
using Kbs.Business.BoatType;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.CreateBoatType;

public class CreateBoatTypeViewModel : ViewModel
{
    private string _name;
    private BoatTypeRequiredExperience _requiredExperience;
    private int _speed;
    private int _seats;
    private bool _hasSteeringWheel;

    public ObservableCollection<CreateBoatExperienceViewModel> PossibleExperiences { get; } = new();

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

    public int Seats
    {
        get => _seats;
        set => SetField(ref _seats, value);
    }

    public bool HasSteeringWheel
    {
        get => _hasSteeringWheel;
        set => SetField(ref _hasSteeringWheel, value);
    }
}