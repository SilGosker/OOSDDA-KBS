using Kbs.Business.BoatType;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.Components;

public class BoatTypeExperienceViewModel(BoatTypeRequiredExperience requiredExperience) : ViewModel
{
    public BoatTypeRequiredExperience RequiredExperience { get; } = requiredExperience;

    // Override ToString to display the RequiredExperience in Dutch
    public override string ToString()
    {
        return RequiredExperience.ToDutchString();
    }
}