using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kbs.Business.User;
using Kbs.Business.Session;
using Kbs.Data.User;
using Kbs.Wpf.Components;
using Kbs.Wpf.User.Login;

namespace Kbs.Wpf.User.Update
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : Page
    {
        private readonly UserValidator _userValidator = new UserValidator(); 
        private readonly INavigationManager _navigationManager;
        private readonly IUserRepository _userRepository = new UserRepository();
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
            var validationResult = _userValidator.ValidatorForUpdate(user, ViewModel.InputConfirmPassword, _userRepository, _sessionUser);
            
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
            if (!validationResult.ContainsKey(nameof(user.Email)) && !validationResult.ContainsKey(nameof(user.Password)))
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
            else
            {
                MessageBox.Show("Er zijn foutmeldingen. Corrigeer de aangegeven velden.");
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Wijzigingen geannuleerd.");
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
