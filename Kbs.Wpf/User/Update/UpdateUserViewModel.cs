using Kbs.Wpf.Components;

namespace Kbs.Wpf.User.Update;

public class UpdateUserViewModel : ViewModel
{
    private string _inputName;
    private string _inputEmail;
    private string _inputPassword;
    private string _inputConfirmPassword;
    private string _emailErrorMessage;
    private string _passwordErrorMessage;

    public string InputName { get => _inputName; set => SetField(ref _inputName, value); }
    public string InputEmail { get => _inputEmail; set => SetField(ref _inputEmail, value); }
    public string InputPassword { get => _inputPassword; set => SetField(ref _inputPassword, value); }
    public string InputConfirmPassword { get => _inputConfirmPassword; set => SetField(ref _inputConfirmPassword, value); }
    public string EmailErrorMessage { get => _emailErrorMessage; set => SetField(ref _emailErrorMessage, value); }
    public string PasswordErrorMessage { get => _passwordErrorMessage; set => SetField(ref _passwordErrorMessage, value); }
}