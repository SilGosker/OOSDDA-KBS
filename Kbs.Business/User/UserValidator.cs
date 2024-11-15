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
    public Dictionary<string, string> ValidatorForUpdate(UserEntity user)
    {
        ThrowHelper.ThrowIfNull(user);
        var errors = new Dictionary<string, string>();
        string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\p{P}\p{S}]).{8,}$";

        if (string.IsNullOrEmpty(user.Password))
        {
            errors.Add(nameof(user.Password), "Wachtwoord is verplicht");
        } else if (!Regex.IsMatch(user.Password, passwordRegex))
        {
            errors.Add(nameof(user.Password), "Wachtwoord voldoet niet aan de eisen (bevat 1 kleine letter, 1 hoofdletter, 1 cijfer, 1 leesteken, minimaal 8 karakters)");
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            errors.Add(nameof(user.Email), "Email is verplicht");
        }
        else
        {
            if (!EmailValidationRegex.IsMatch(user.Email.Trim()))
            {
                errors.Add(nameof(user.Email), "Ongeldig email adres");
            }
        }

        return errors;
    }
}