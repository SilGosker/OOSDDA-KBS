using System.Text;
using Kbs.Business.Helpers;
using System.Text.RegularExpressions;

namespace Kbs.Business.User;

public class UserValidator
{
    private static readonly Regex EmailValidationRegex =
        new Regex("^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$", RegexOptions.Compiled);
    
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
    public Dictionary<string, string> ValidatorForUpdate(UserEntity user, string passwordConfirmation, IUserRepository userRepository, string previousUserEmail)
    {
        ThrowHelper.ThrowIfNull(user);
        var errors = new Dictionary<string, string>();

        if (!string.IsNullOrEmpty(user.Password))
        {
            var errorStringBuilder = new StringBuilder();
            if (user.Password.Length < 8)
            {
                errorStringBuilder.AppendLine("- Wachtwoord moet minimaal 8 tekens lang zijn");
            }
            if (!user.Password.Any(char.IsUpper) || !user.Password.Any(char.IsLower))
            {
                errorStringBuilder.AppendLine("- Wachtwoord moet minimaal 1 hoofdletter en 1 kleine letter bevatten");
            }
            if (!user.Password.Any(char.IsDigit))
            {
                errorStringBuilder.AppendLine("- Wachtwoord moet minimaal 1 cijfer bevatten");
            }

            Regex regex = new Regex(@"[!\""#\$%&'()*+,-./:;<=>?@[\]^_`{|}~€£¥₹©®™§°]", RegexOptions.Compiled);
            if (!user.Password.Any(c => regex.IsMatch(c.ToString())))
            {
                errorStringBuilder.AppendLine("- Wachtwoord moet minimaal 1 speciaal teken bevatten");
            }

            if (passwordConfirmation != user.Password)
            {
                errorStringBuilder.AppendLine("- Wachtwoorden komen niet overeen");
            }

            string errorString = errorStringBuilder.ToString();

            if (!string.IsNullOrEmpty(errorString))
            {
                errors.Add(nameof(user.Password), errorString);
            }
        }

        if (!string.IsNullOrWhiteSpace(user.Email) && !user.Email.Equals(previousUserEmail))
        {
            if (!EmailValidationRegex.IsMatch(user.Email.Trim()) ||
                !System.Net.Mail.MailAddress.TryCreate(user.Email.Trim(), out _))
            {
                errors.Add(nameof(user.Email), "Ongeldig email adres");
            }
            else if (userRepository.GetByEmail(user.Email) != null)
            {
                errors.Add(nameof(user.Email), "Email adres bestaat al");
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
            var errorStringBuilder = new StringBuilder();
            if (user.Password.Length < 8)
            {
                errorStringBuilder.AppendLine("- Wachtwoord moet minimaal 8 tekens lang zijn");
            }
            if (!user.Password.Any(char.IsUpper) || !user.Password.Any(char.IsLower))
            {
                errorStringBuilder.AppendLine("- Wachtwoord moet minimaal 1 hoofdletter en 1 kleine letter bevatten");
            }
            if (!user.Password.Any(char.IsDigit))
            { 
                errorStringBuilder.AppendLine("- Wachtwoord moet minimaal 1 cijfer bevatten");
            }

            Regex regex = new Regex(@"[!\""#\$%&'()*+,-./:;<=>?@[\]^_`{|}~€£¥₹©®™§°]", RegexOptions.Compiled);
            if (!user.Password.Any(c => regex.IsMatch(c.ToString())))
            {
                errorStringBuilder.AppendLine("- Wachtwoord moet minimaal 1 speciaal teken bevatten");
            }

            if (passwordConfirmation != user.Password)
            {
                errorStringBuilder.AppendLine("- Wachtwoorden komen niet overeen");
            }

            string errorString = errorStringBuilder.ToString();
            
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
        
        return errors;
    }
}