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
}