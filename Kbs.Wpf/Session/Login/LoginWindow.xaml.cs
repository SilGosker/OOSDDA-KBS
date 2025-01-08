using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Wpf.Boat.Read.Index;
using Kbs.Wpf.Reservation.Read.Index;
using Kbs.Wpf.User.Ban;
using Kbs.Wpf.User.Register;

namespace Kbs.Wpf.Session.Login;

public partial class LoginWindow : Window
{
    private readonly UserValidator _userValidator = new UserValidator();
    private RegisterUserWindow _registrationWindow;
    private LoginViewModel ViewModel => (LoginViewModel)DataContext;
    public LoginWindow()
    {
        InitializeComponent();
        _registrationWindow = new(this);
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

        Login(user);
    }


    public void Login(UserEntity user)
    {
        if (!SessionManager.Instance.TryCreate(user, out var session))
        {
            MessageBox.Show(this, "Email/wachtwoord combinatie niet gevonden", "Kon niet inloggen", MessageBoxButton.OK);
            return;
        }

        _registrationWindow.Close();
        var window = new MainWindow();
        window.Show();

        // Go to default page based on role
        switch (session.User.Role)
        {
            // Banned user: Navigate to ban user page.
            case UserRole.Banned:
                window.Navigate(() => new BanUserPage());
                break;
            // MaterialCommissioner: Navigate to boat index page.
            case UserRole.MaterialCommissioner:
                window.Navigate(() => new ReadIndexBoatPage(window));
                break;
            // Member and other people: Navigate to reservation index page.
            default:
                window.Navigate(() => new ReadIndexReservationPage(window));
                break;
        }

        Close();
    }

    protected override void OnClosed(EventArgs e)
    {
        try
        {
            _registrationWindow.Close();
        }
        catch (Exception)
        {
            // ignored
        }
        base.OnClosed(e);
    }

    private void RegistrationButtonClicked(object sender, RoutedEventArgs e)
    {
        _registrationWindow.Show();
    }

    // have to use a event handler because PasswordBox doesn't support binding
    private void PasswordChanged(object sender, RoutedEventArgs e)
    {
        ViewModel.Password = ((PasswordBox)sender).Password;

    }
    
    public void RegistrationClosed()
    {
        _registrationWindow = new(this);
    }
}