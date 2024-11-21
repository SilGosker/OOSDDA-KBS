using System.Windows;
using System.Windows.Controls;
using Kbs.Business.User;
using Kbs.Business.Session;
using Kbs.Data.User;

namespace Kbs.Wpf.User.Update
{
    public partial class AccountView : Page
    {
        private readonly UserValidator _userValidator = new(); 
        private readonly INavigationManager _navigationManager;
        private readonly UserRepository _userRepository = new();
        private readonly UserEntity _sessionUser;
        private Func<Page> _navigationTarget;

        public AccountViewModel ViewModel => (AccountViewModel)DataContext;
        public AccountView(INavigationManager navigationManager)
        {
            InitializeComponent();
            this._navigationManager = navigationManager;
            _navigationTarget = () => new AccountView(_navigationManager); // change target to homescreen
            _sessionUser = SessionManager.Instance.Current.User;
            ViewModel.InputEmail = _sessionUser.Email;
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            var user = new UserEntity()
            {
                Email = ViewModel.InputEmail,
                Password = ViewModel.InputPassword
            };
            var validationResult = _userValidator.ValidatorForUpdate(user, ViewModel.InputConfirmPassword, _userRepository, _sessionUser.Email);
            
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
                bool emailUpdated = !string.IsNullOrEmpty(user.Email) && !user.Email.Equals(_sessionUser.Email);
                bool passwordUpdated = !string.IsNullOrEmpty(user.Password) && !user.Password.Equals(_sessionUser.Password);

                string successMessage = "Er zijn geen aanpassingen gemaakt.";
                if (emailUpdated && passwordUpdated)
                {
                    successMessage = "Email en Wachtwoord zijn succesvol aangepast.";
                }
                else if (emailUpdated)
                {
                    successMessage = "Email is succesvol aangepast.";
                }
                else if (passwordUpdated)
                {
                    successMessage = "Wachtwoord is succesvol aangepast.";
                }
               
                if (emailUpdated || passwordUpdated)
                {
                    SessionManager.Instance.UpdateSessionUser(((emailUpdated) ? user.Email : null), ((passwordUpdated) ? user.Password : null));
                    _userRepository.Update(_sessionUser);
                }
                MessageBox.Show(successMessage);
                _navigationManager.Navigate(_navigationTarget);
                return;
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            _navigationManager.Navigate(_navigationTarget);
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
}
