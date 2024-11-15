using Kbs.Business.Session;
using Kbs.Business.User;
using System.Windows;
using System.Windows.Controls;

namespace Kbs.Wpf.User.Login;

public partial class LoginWindow : Window
{
    private readonly UserValidator _userValidator = new UserValidator();
    public LoginViewModel ViewModel => (LoginViewModel)DataContext;

    public LoginWindow()
    {
        InitializeComponent();
    }

    private void SubmitButtonClicked(object sender, RoutedEventArgs e)
    {
        var user = new UserEntity()
        {
            Email = ViewModel.Email,
            Password = ViewModel.Password
        };

        var validationResult = _userValidator.ValidatorForLogIn(user);

        if (validationResult.TryGetValue(nameof(user.Email), out string emailMessage))
        {
            ViewModel.EmailErrorMessage = emailMessage;
        }
        else
        {
            ViewModel.EmailErrorMessage = string.Empty;
        }

        if (validationResult.TryGetValue(nameof(user.Password), out string passwordMessage))
        {
            ViewModel.PasswordErrorMessage = passwordMessage;
        }
        else
        {
            ViewModel.PasswordErrorMessage = string.Empty;
        }

        if (validationResult.Count > 0)
        {
            return;
        }

        if (!SessionManager.Instance.TryCreate(user, out var session))
        {
            MessageBox.Show(this, "Email/wachtwoord combinatie niet gevonden", "Kon niet inloggen", MessageBoxButton.OK);
            return;
        }

        var window = new MainWindow();
        window.Show();
        Close();
    }

    // have to use a event handler because PasswordBox doesn't support binding
    private void PasswordChanged(object sender, RoutedEventArgs e)
    {
        ViewModel.Password = ((PasswordBox)sender).Password;

    }
}