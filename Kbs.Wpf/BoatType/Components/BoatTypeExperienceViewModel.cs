using Kbs.Business.BoatType;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.CreateBoatType;

public class BoatTypeExperienceViewModel : ViewModel
{
    public BoatTypeExperienceViewModel(BoatTypeRequiredExperience requiredExperience)
    {
        RequiredExperience = requiredExperience;
    }
    public BoatTypeRequiredExperience RequiredExperience { get; }

    // Override ToString to display the RequiredExperience in Dutch
    public override string ToString()
    {
        return RequiredExperience.ToDutchString();
    }
}