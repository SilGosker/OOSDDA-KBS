
using Kbs.Business.Helpers;

namespace Kbs.Business.User;

public class UserValidator
{
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
            try
            {
                new System.Net.Mail.MailAddress(user.Email.Trim());
            }
            catch (Exception)
            {
                errors.Add(nameof(user.Email), "Ongeldig email adres");
            }
        }

        return errors;
    }
}