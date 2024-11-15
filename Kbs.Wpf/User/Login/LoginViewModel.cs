
using Kbs.Wpf.Components;

namespace Kbs.Wpf.User.Login;

public class LoginViewModel : ViewModel
{
    private string _email;
    private string _password;
    private string _emailErrorMessage;
    private string _passwordErrorMessage;

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

    
}