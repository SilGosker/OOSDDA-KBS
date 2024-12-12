using System.Windows;
using System.Windows.Controls;
using Kbs.Business.User;
using Kbs.Business.Session;
using Kbs.Data.User;
using Kbs.Wpf.Reservation.Read.Index;
using Kbs.Wpf.Boat.Read.Index;
using Kbs.Wpf.User.Ban;

namespace Kbs.Wpf.User.Update;

public partial class UpdateUserPage : Page
{
    private readonly UserValidator _userValidator = new(); 
    private readonly INavigationManager _navigationManager;
    private readonly UserRepository _userRepository = new();
    private UpdateUserViewModel ViewModel => (UpdateUserViewModel)DataContext;
    public UpdateUserPage(INavigationManager navigationManager)
    {
        InitializeComponent();
        this._navigationManager = navigationManager;
        ViewModel.InputEmail = SessionManager.Instance.Current.User.Email;
        ViewModel.InputName = SessionManager.Instance.Current.User.Name;
    }

    private void Submit(object sender, RoutedEventArgs e)
    {
        var sessionUser = SessionManager.Instance.Current.User;
        var user = new UserEntity()
        {
            Name = ViewModel.InputName,
            Email = ViewModel.InputEmail,
            Password = ViewModel.InputPassword
        };
        var validationResult = _userValidator.ValidatorForUpdate(user, ViewModel.InputConfirmPassword, _userRepository, sessionUser.Email);
            
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

        // When no errors are shown
        if (validationResult.Count == 0)
        {
            bool passwordUpdated = !string.IsNullOrEmpty(user.Password);
            SessionManager.Instance.UpdateSessionUser(user.Email, ((passwordUpdated) ? user.Password : null), user.Name);
            _userRepository.Update(sessionUser);
            MessageBox.Show("Gegevens zijn aangepast.");
            UserEntity updatedSessionUser = SessionManager.Instance.Current.User;
            // Go to default page based on role
            switch (updatedSessionUser.Role)
            {
                // Banned user: Navigate to ban user page.
                case UserRole.Banned:
                    _navigationManager.Navigate(() => new BanUserPage());
                    break;
                // MaterialCommissioner: Navigate to boat index page.
                case UserRole.MaterialCommissioner:
                    _navigationManager.Navigate(() => new ReadIndexBoatPage(_navigationManager));
                    break;
                // Member and other people: Navigate to reservation index page.
                default:
                    _navigationManager.Navigate(() => new ReadIndexReservationPage(_navigationManager));
                    break;
            }
            return;
        }
    }
    private void Cancel(object sender, RoutedEventArgs e)
    {
        UserEntity updatedSessionUser = SessionManager.Instance.Current.User;
        // Go to default page based on role
        switch (updatedSessionUser.Role)
        {
            // Banned user: Navigate to ban user page.
            case UserRole.Banned:
                _navigationManager.Navigate(() => new BanUserPage());
                break;
            // MaterialCommissioner: Navigate to boat index page.
            case UserRole.MaterialCommissioner:
                _navigationManager.Navigate(() => new ReadIndexBoatPage(_navigationManager));
                break;
            // Member and other people: Navigate to reservation index page.
            default:
                _navigationManager.Navigate(() => new ReadIndexReservationPage(_navigationManager));
                break;
        }
    }
    private void PasswordChanged(object sender, RoutedEventArgs e)
    {
        ViewModel.InputPassword = ((PasswordBox)sender).Password;
    }

    private void PasswordConfirmationChanged(object sender, RoutedEventArgs e)
    {
        ViewModel.InputConfirmPassword = ((PasswordBox)sender).Password;
    }
}