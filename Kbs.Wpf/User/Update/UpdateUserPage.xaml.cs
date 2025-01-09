using System.Windows;
using System.Windows.Controls;
using Kbs.Business.User;
using Kbs.Business.Session;
using Kbs.Data.User;
using Kbs.Wpf.Boat.Read.Index;
using Kbs.Wpf.Components;
using Kbs.Wpf.Reservation.Read.Index;
using Kbs.Wpf.User.Ban;

namespace Kbs.Wpf.User.Update;

[HasRole(UserRole.Member)]
[HasRole(UserRole.GameCommissioner)]
[HasRole(UserRole.MaterialCommissioner)]
[HighlightFor(typeof(Kbs.Business.Session.Session))]
public partial class UpdateUserPage : Page
{
    private readonly UserValidator _userValidator = new(); 
    private readonly INavigationManager _navigationManager;
    private readonly UserRepository _userRepository = new();
    private UpdateUserViewModel ViewModel => (UpdateUserViewModel)DataContext;
    public UpdateUserPage(INavigationManager navigationManager)
    {
        InitializeComponent();
        _navigationManager = navigationManager;
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
            
        ViewModel.EmailErrorMessage = validationResult.TryGetValue(nameof(user.Email), out string emailMessage) ? emailMessage : string.Empty;
        ViewModel.PasswordErrorMessage = validationResult.TryGetValue(nameof(user.Password), out string passwordMessage) ? passwordMessage : string.Empty;
        ViewModel.NameErrorMessage = validationResult.TryGetValue(nameof(user.Name), out string nameMessage) ? nameMessage : string.Empty;
        
        // When no errors are shown
        if (validationResult.Count == 0)
        {
            bool passwordUpdated = !string.IsNullOrEmpty(user.Password);
            SessionManager.Instance.UpdateSessionUser(user.Email, ((passwordUpdated) ? user.Password : null), user.Name);
            _userRepository.Update(sessionUser);
            MessageBox.Show("Gegevens zijn aangepast.");

            if (sessionUser.IsMember() || sessionUser.IsGameCommissioner())
            {
                _navigationManager.Navigate(() => new ReadIndexReservationPage(_navigationManager));
            }
            else if (sessionUser.IsMaterialCommissioner())
            {
                _navigationManager.Navigate(() => new ReadIndexBoatPage(_navigationManager));
            }
            else
            {
                _navigationManager.Navigate(() => new BanUserPage());
            }
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