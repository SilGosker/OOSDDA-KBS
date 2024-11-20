using Kbs.Business.BoatType;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.CreateBoatType;

public class CreateBoatExperienceViewModel : ViewModel
{
    public CreateBoatExperienceViewModel(BoatTypeRequiredExperience requiredExperience)
    {
        RequiredExperience = requiredExperience;
    }
    public BoatTypeRequiredExperience RequiredExperience { get; }

    public override string ToString()
    {
        return RequiredExperience.ToDutchString();
    }
}