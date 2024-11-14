using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kbs.Wpf.User.Login;

public class LoginViewModel : INotifyPropertyChanged
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

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}