using Kbs.Business.Helpers;
using System.Text.RegularExpressions;

namespace Kbs.Business.User;

public class UserValidator
{
    private static readonly Regex EmailValidationRegex =
        new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.Compiled);

    public Dictionary<string, string> ValidatorForLogIn(UserEntity user)
    {
        ThrowHelper.ThrowIfNull(user);
        var errors = new Dictionary<string, string>();

        if (string.IsNullOrEmpty(user.Password))
        {
            errors.Add(nameof(user.Password), "Wachtwoord is verplicht");
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            errors.Add(nameof(user.Email), "Email is verplicht");
        }
        else
        {
            if (!EmailValidationRegex.IsMatch(user.Email.Trim()) ||
                !System.Net.Mail.MailAddress.TryCreate(user.Email.Trim(), out _))
            {
                errors.Add(nameof(user.Email), "Ongeldig email adres");
            }
        }

        return errors;
    }
    
    public Dictionary<string, string> ValidatorForRegistration(UserEntity user, string passwordConfirmation)
    {
        ThrowHelper.ThrowIfNull(user);
        var errors = new Dictionary<string, string>();

        if (string.IsNullOrEmpty(user.Password))
        {
            errors.Add(nameof(user.Password), "Wachtwoord is verplicht");
        }
        else
        {
            string errorString = "";
            if (user.Password.Length < 8)
            {
                errorString += "\n- Wachtwoord moet minimaal 8 tekens lang zijn";
            }
            if (!user.Password.Any(char.IsUpper) || !user.Password.Any(char.IsLower))
            {
                errorString += "\n- Wachtwoord moet minimaal 1 hoofdletter en 1 kleine letter bevatten";
            }
            if (!user.Password.Any(char.IsDigit))
            { 
                errorString += "\n- Wachtwoord moet minimaal 1 cijfer bevatten";
            }
            Regex regex = new Regex(@"[!\""#\$%&'()*+,-./:;<=>?@[\]^_`{|}~€£¥₹©®™§°]");
            if (!user.Password.Any(c => regex.IsMatch(c.ToString())));
            {
                errorString += "\n- Wachtwoord moet minimaal 1 speciaal teken bevatten";
            }
            
            if (!string.IsNullOrEmpty(errorString))
            {
                errors.Add(nameof(user.Password), errorString);
            }
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            errors.Add(nameof(user.Email), "Email is verplicht");
        }
        else
        {
            if (!EmailValidationRegex.IsMatch(user.Email.Trim()) ||
                !System.Net.Mail.MailAddress.TryCreate(user.Email.Trim(), out _))
            {
                errors.Add(nameof(user.Email), "Ongeldig email adres");
            }
        }
        
        if(passwordConfirmation != user.Password)
        {
            errors.Add(nameof(passwordConfirmation), "Wachtwoorden komen niet overeen");
        }
        
        return errors;
    }
}