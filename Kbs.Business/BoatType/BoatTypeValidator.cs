using Kbs.Business.Helpers;

namespace Kbs.Business.BoatType;

public class BoatTypeValidator
{
    public Dictionary<string, string> ValidateForCreate(BoatTypeEntity boatType)
    {
        ThrowHelper.ThrowIfNull(boatType);
        Dictionary<string, string> validationResult = new();

        if (string.IsNullOrWhiteSpace(boatType.Name))
        {
            validationResult.Add(nameof(boatType.Name), "Naam is verplicht");
        }

        if (boatType.RequiredExperience == default)
        {
            validationResult.Add(nameof(boatType.RequiredExperience), "Benodigde ervaring is verplicht");
        }

        if (!Enum.IsDefined(boatType.Seats))
        {
            validationResult.Add(nameof(boatType.Seats), "Stoelen zijn verplicht");    
        }

        if (boatType.Speed <= 0)
        {
            validationResult.Add(nameof(boatType.Speed), "Snelheid is verplicht en moet groter zijn dan 0");
        }

        return validationResult;
    }
}