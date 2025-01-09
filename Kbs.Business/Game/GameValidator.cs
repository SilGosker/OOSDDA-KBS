namespace Kbs.Business.Game;

public class GameValidator
{
    
    public Dictionary<string, string> ValidateForCreate(GameEntity game)
    {
        var result = new Dictionary<string, string>();
        if (string.IsNullOrWhiteSpace(game.Name))
        {
            result.Add(nameof(game.Name), "Naam is verplicht");
        }
        else if (game.Name.Length > 255)
        {
            result.Add(nameof(game.Name), "Naam mag niet langer zijn dan 255 karakters");
        }

        if (game.CourseId <= 0)
        {
            result.Add(nameof(game.CourseId), "Parcours is verplicht");
        }

        if (game.Date < DateTime.Now)
        {
            result.Add(nameof(game.Date), "Datum moet in de toekomst liggen");
        }

        return result;
    }
    
    public Dictionary<string, string> ValidateForUpdate(GameEntity game)
    {
        var result = new Dictionary<string, string>();
        if (string.IsNullOrWhiteSpace(game.Name))
        {
            result.Add(nameof(game.Name), "Naam is verplicht");
        }
        else if (game.Name.Length > 255)
        {
            result.Add(nameof(game.Name), "Naam mag niet langer zijn dan 255 karakters");
        }

        if (game.Date < DateTime.Now)
        {
            result.Add(nameof(game.Date), "Datum moet in de toekomst liggen");
        }

        if (game.CourseId <= 0)
        {
            result.Add(nameof(game.CourseId), "Parcours is verplicht");
        }

        return result;
    }
}