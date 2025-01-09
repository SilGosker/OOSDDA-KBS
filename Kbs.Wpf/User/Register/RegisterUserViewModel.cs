using Kbs.Wpf.Components;

namespace Kbs.Wpf.User.Register;

public class RegisterUserViewModel : ViewModel
{
    private string _email;
    private string _name;
    private string _password;
    private string _passwordConfirmation;
    private string _emailErrorMessage;
    private string _passwordErrorMessage;
    private string _passwordConfirmationErrorMessage;
    private string _nameErrorMessage;

    public string Email
    {
        get => _email;
        set => SetField(ref _email, value);
    }
    public string EmailErrorMessage
    {
        get => _emailErrorMessage;
        set => SetField(ref _emailErrorMessage, value);
    }
    
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public string Password
    {
        get => _password;
        set => SetField(ref _password, value);
    }
    public string PasswordErrorMessage
    {
        get => _passwordErrorMessage;
        set => SetField(ref _passwordErrorMessage, value);
    }
    public string PasswordConfirmation
    {
        get => _passwordConfirmation;
        set => SetField(ref _passwordConfirmation, value);
    }
    public string PasswordConfirmationErrorMessage
    {
        get => _passwordConfirmationErrorMessage;
        set => SetField(ref _passwordConfirmationErrorMessage, value);
    }
    public string NameErrorMessage
    {
        get => _nameErrorMessage;
        set => SetField(ref _nameErrorMessage, value);
    }
}