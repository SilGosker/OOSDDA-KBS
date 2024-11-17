using Kbs.Business.Session;
using Kbs.Business.User;
using System.Windows;
using System.Windows.Controls;
using Kbs.Data.User;
using Kbs.Wpf.User.Login;

namespace Kbs.Wpf.User.Registration;

public partial class RegistrationWindow : Window
{
    
    private RegistrationViewModel ViewModel => (RegistrationViewModel)DataContext;
    private readonly UserValidator _userValidator = new UserValidator();
    private readonly LoginWindow _loginWindow;
    private readonly IUserRepository _userRepository = new UserRepository();
    public RegistrationWindow(LoginWindow loginWindow)
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
            Role = Role.Member
        };
        
        var validationResult = _userValidator.ValidatorForRegistration(user, ViewModel.PasswordConfirmation);

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
        
        if (validationResult.TryGetValue(nameof(ViewModel.PasswordConfirmation), out string passwordConfirmationMessage))
        {
            ViewModel.PasswordConfirmationErrorMessage = passwordConfirmationMessage;
        }
        else
        {
            ViewModel.PasswordConfirmationErrorMessage = string.Empty;
        }
        
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
    
    private void PasswordConfirmationChanged(object sender, RoutedEventArgs e)
    {
        ViewModel.PasswordConfirmation = ((PasswordBox)sender).Password;
    }
}