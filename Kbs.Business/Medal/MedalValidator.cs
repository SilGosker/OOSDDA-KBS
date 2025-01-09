using Kbs.Business.Helpers;

namespace Kbs.Business.Medal
{
    public class MedalValidator
    {
        public Dictionary<string, string> ValidateForCreate(MedalEntity medal)
        {
            ThrowHelper.ThrowIfNull(medal);
            Dictionary<string, string> validationResult = new();

            if (medal.UserId <= 0)
            {
                validationResult.Add(nameof(medal.UserId), "UserId is verplicht");
            }

            if (medal.GameId <= 0)
            {
                validationResult.Add(nameof(medal.GameId), "GameId is verplicht");
            }

            if (medal.BoatId <= 0)
            {
                validationResult.Add(nameof(medal.BoatId), "BoatId moet positief zijn");
            }

            if (!Enum.IsDefined(medal.Material))
            {
                validationResult.Add(nameof(medal.Material), "Materiaal is verplicht");
            }

            return validationResult;
        }


    }
}
