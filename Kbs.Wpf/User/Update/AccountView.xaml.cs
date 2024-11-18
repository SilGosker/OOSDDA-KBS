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
        private UserEntity _sessionUser; 

        public AccountViewModel ViewModel => (AccountViewModel)DataContext;
        public AccountView(INavigationManager navigationManager)
        {
            InitializeComponent();
            this._navigationManager = navigationManager;
            ViewModel.InputEmail = SessionManager.Instance.Current.User.Email;
            EmailInput.Text = ViewModel.InputEmail;
            _sessionUser = SessionManager.Instance.Current.User;
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            if (sender != ConfirmButton) return;

            bool hasEmailChanged = false;
            bool isEmailValid = false;
            bool hasPasswordChanged = false;
            bool isPasswordCorrect = false;
            bool isPasswordMatching = true;

            if ((EmailInput.Text.Length == 0 || EmailInput.Text.Equals(_sessionUser.Email)) && PasswordInput.Password.Length == 0)
            {
                GlobalError.Visibility = Visibility.Visible; 
                EmailErrorMessage.Visibility = Visibility.Hidden;
                PasswordErrorMessage.Visibility = Visibility.Hidden;
            }
            else
            {
                GlobalError.Visibility = Visibility.Hidden;
                var user = new UserEntity()
                {
                    Email = ViewModel.InputEmail,
                    Password = ViewModel.InputPassword
                };
                var validationResult = _userValidator.ValidatorForUpdate(user, PasswordConfirmInput.Password, _userRepository);
                if (user.Email != null && user.Email.Length != 0 && !user.Email.Equals(_sessionUser.Email))
                {
                    hasEmailChanged = true;
                    if (validationResult.TryGetValue(nameof(user.Email), out string emailMessage))
                    {
                        EmailErrorMessage.Content = emailMessage;
                        EmailErrorMessage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        EmailErrorMessage.Visibility = Visibility.Hidden;
                        isEmailValid = true;
                    }
                } 

                if (user.Password != null && user.Password.Length != 0)
                {
                    hasPasswordChanged = true;
                    if (validationResult.TryGetValue(nameof(user.Password), out string passwordMessage))
                    {
                        PasswordErrorMessage.Text = passwordMessage;
                        PasswordErrorMessage.Visibility = Visibility.Visible;
                    } else if (validationResult.ContainsKey("Bevestiging"))
                    {
                        PasswordErrorMessage.Visibility = Visibility.Hidden;
                        isPasswordMatching = false;
                    }
                    else
                    {
                        PasswordErrorMessage.Visibility = Visibility.Hidden;
                        isPasswordCorrect = true;
                    }
                }

                if (validationResult.Count > 1 || !isPasswordMatching)
                {
                    return;
                }

                bool isInputDifferentThanExisting = false;
                if (hasEmailChanged && isEmailValid)
                {

                    if (!user.Email.Equals(_sessionUser.Email))
                    {
                        isInputDifferentThanExisting = true;
                        _sessionUser.Email = user.Email;
                    } else
                    {
                        hasEmailChanged = false;
                    }
                }
                if (hasPasswordChanged && isPasswordCorrect)
                {
                    user.Encrypt();
                    if (!user.Password.Equals(_sessionUser.Password))
                    {
                        isInputDifferentThanExisting = true;
                        _sessionUser.Password = user.Password;
                    }
                }
                if (isInputDifferentThanExisting)
                {
                    _userRepository.Update(_sessionUser);
                    MessageBox.Show(((isEmailValid)? "Email" : "") + ((isEmailValid && isPasswordCorrect)? " en " : "") + ((isPasswordCorrect)? "Wachtwoord" : "") + ((isEmailValid && isPasswordCorrect) ? " zijn " : " is ") + "succesvol aangepast.");
                    _navigationManager.Navigate(() => new AccountView(_navigationManager));
                }
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            if (sender != CancelButton) return;
            MessageBox.Show("Wijzigingen geannuleerd.");
            _navigationManager.Navigate(() => new AccountView(_navigationManager));
        }
        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender == PasswordInput) {
                ViewModel.InputPassword = ((PasswordBox)sender).Password;
            } else if (sender == PasswordConfirmInput)
            {
                ViewModel.InputConfirmPassword = ((PasswordBox)sender).Password;
            }
        }
    }
}
