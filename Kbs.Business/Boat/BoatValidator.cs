using Kbs.Business.BoatType;
using Kbs.Business.Helpers;

namespace Kbs.Business.Boat;

public class BoatValidator
{
    private readonly IBoatTypeRepository _boatTypeRepository;
    public static TimeSpan RequestDeletionTime { get; internal set; } = TimeSpan.FromMinutes(30);

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
        if (_boatTypeRepository.GetById(boat.BoatTypeId) == null)
        {
            validationResult.Add(nameof(boat.BoatTypeId), "Boottype is verplicht");
        }

        return validationResult;
    }

    public Dictionary<string, string> ValidateForUpdate(BoatEntity boat)
    {
        ThrowHelper.ThrowIfNull(boat);
        Dictionary<string, string> validationResult = new();

        // Validate Name
        if (string.IsNullOrWhiteSpace(boat.Name))
        {
            validationResult.Add(nameof(boat.Name), "Naam is verplicht");
        }

        if (!Enum.IsDefined(boat.Status))
        {
            validationResult.Add(nameof(boat.Status), "Geldige status is verplicht");
        }

        if (boat.Status == BoatStatus.Operational && boat.DeleteRequestDate != null)
        {
            validationResult.Add(nameof(boat.Status), "Status mag niet aangepast worden vanwege de verwijdering aanvraag");
        }

        return validationResult;
    }

    public Dictionary<string, string> IsValidForPermanentDeletion(BoatEntity boat)
    {
        ThrowHelper.ThrowIfNull(boat);
        Dictionary<string, string> validationResult = new();

        if (boat.DeleteRequestDate == null)
        {
            validationResult.Add(nameof(boat.DeleteRequestDate), "Wachttijd is nog niet gestart.");
        }
        else
        {
            TimeSpan? waitTime = RequestDeletionTime - (DateTime.Now - boat.DeleteRequestDate);
            if (waitTime > TimeSpan.Zero)
            {
                validationResult.Add(nameof(boat.DeleteRequestDate), "Wachttijd is nog niet verstreken. Nog " + waitTime.Value.ToString(@"dd\.hh\:mm\:ss"));
            }
        }

        return validationResult;
    }
}