namespace Kbs.Business.BoatType;

public static class BoatTypeRequiredExperienceExtensions
{
    public static string ToDutchString(this BoatTypeRequiredExperience experience)
    {
        return experience switch
        {
            BoatTypeRequiredExperience.Beginner => "Beginner",
            BoatTypeRequiredExperience.Intermediate => "Gevorderd",
            BoatTypeRequiredExperience.Expert => "Expert",
            _ => "Onbekend"
        };
    }
}