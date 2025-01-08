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
        else if (boatType.Name.Length > 255)
        {
            validationResult.Add(nameof(boatType.Name), "Naam mag niet langer zijn dan 255 karakters");
        }

        if (!Enum.IsDefined(boatType.RequiredExperience))
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
    
    public Dictionary<string, string> ValidatorForUpdate(BoatTypeEntity boatType)
    {
        ThrowHelper.ThrowIfNull(boatType);
        Dictionary<string, string> validationResult = new();
        
        if (!Enum.IsDefined(boatType.RequiredExperience))
        {
            validationResult.Add(nameof(boatType.RequiredExperience), "Benodigde ervaring is verplicht");
        }
        if (!Enum.IsDefined(boatType.Seats))
        {
            validationResult.Add(nameof(boatType.Seats), "Stoelen zijn verplicht");
        }
        if (boatType.Speed <= 0)
        {
            validationResult.Add(nameof(boatType.Speed), "Snelheid moet groter zijn dan 0");
        }
        if (string.IsNullOrWhiteSpace(boatType.Name))
        {
            validationResult.Add(nameof(boatType.Name), "Naam is verplicht");
        }
        else if (boatType.Name.Length > 255)
        {
            validationResult.Add(nameof(boatType.Name), "Naam mag niet langer zijn dan 255 karakters");
        }

        return validationResult;
    }
}