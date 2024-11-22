using Kbs.Business.BoatType;
using Kbs.Business.Helpers;

namespace Kbs.Business.Boat;

public class BoatValidator
{
    private readonly IBoatTypeRepository _boatTypeRepository;

    public BoatValidator(IBoatTypeRepository boatTypeRepository)
    {
        _boatTypeRepository = boatTypeRepository;
    }

    public Dictionary<string, string> ValidateForCreate(BoatEntity boat)
    {
        ThrowHelper.ThrowIfNull(boat);
        Dictionary<string, string> validationResult = new();

        // Validate Name
        if (string.IsNullOrWhiteSpace(boat.Name))
        {
            validationResult.Add(nameof(boat.Name), "Naam is verplicht");
        }

        // Validate BoatTypeId
        if (boat.BoatTypeId <= 0)
        {
            validationResult.Add(nameof(boat.BoatTypeId), "Boottype is verplicht");
        }

        return validationResult;
    }
}