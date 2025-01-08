using Kbs.Business.User;
using System.Windows;
using System.Windows.Controls;
using Kbs.Data.User;
using Kbs.Wpf.Session.Login;

namespace Kbs.Wpf.User.Register;

public partial class RegisterUserWindow : Window
{
    private RegisterUserViewModel ViewModel => (RegisterUserViewModel)DataContext;
    private readonly UserValidator _userValidator = new();
    private readonly LoginWindow _loginWindow;
    private readonly UserRepository _userRepository = new();
    public RegisterUserWindow(LoginWindow loginWindow)
    {
        _loginWindow = loginWindow;
        InitializeComponent();
    }
    
    private void SubmitButtonClicked(object sender, RoutedEventArgs e)
    {
        var user = new UserEntity()
        {
            Email = ViewModel.Email,
            Name = ViewModel.Name ?? string.Empty,
            Password = ViewModel.Password,
            Role = UserRole.Member
        };
        
        var validationResult = _userValidator.ValidatorForRegister(user, ViewModel.PasswordConfirmation);

        ViewModel.EmailErrorMessage = validationResult.TryGetValue(nameof(user.Email), out string emailMessage) ? emailMessage : string.Empty;
        ViewModel.PasswordErrorMessage = validationResult.TryGetValue(nameof(user.Password), out string passwordMessage) ? passwordMessage : string.Empty;
        ViewModel.PasswordConfirmationErrorMessage = validationResult.TryGetValue(nameof(ViewModel.PasswordConfirmation), out string passwordConfirmationMessage) ? passwordConfirmationMessage : string.Empty;
        ViewModel.NameErrorMessage = validationResult.TryGetValue(nameof(user.Name), out string nameMessage) ? nameMessage : string.Empty;
        
        if (validationResult.Count > 0)
        {
            return;
        }
        
        if (_userRepository.GetByEmail(user.Email) != null)
        {
            ViewModel.EmailErrorMessage = "Er is al een account met dit e-mailadres";
            return;
        }
        
        // Create temporary variable to store the password before encryption
        string password = user.Password;
        
        // Encrypt the password before storing it in the database
        user.Encrypt();
        _userRepository.Create(user);
        
        // Set the password back to the original value so the login check can be done with the original password
        user.Password = password;
        
        _loginWindow.Login(user);
    }
    
    private void CancelButtonClicked(object sender, RoutedEventArgs e)
    {
        Hide();
    }
    
    // event handler because PasswordBox doesn't support binding
    private void PasswordChanged(object sender, RoutedEventArgs e)
    {
        ViewModel.Password = ((PasswordBox)sender).Password;
    }

    protected override void OnClosed(EventArgs e)
    {
        _loginWindow.RegistrationClosed();
        base.OnClosed(e);
    }

    private void PasswordConfirmationChanged(object sender, RoutedEventArgs e)
    {
        ViewModel.PasswordConfirmation = ((PasswordBox)sender).Password;
    }
}