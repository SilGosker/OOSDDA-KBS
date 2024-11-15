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

        public AccountViewModel ViewModel => (AccountViewModel)DataContext;
        public AccountView(INavigationManager navigationManager)
        {
            InitializeComponent();
            this._navigationManager = navigationManager;
            ViewModel.InputEmail = SessionManager.Instance.Current.User.Email;
            EmailInput.Text = ViewModel.InputEmail;
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            if (sender != ConfirmButton) return;

            bool mailChange = false;
            bool mailCorrect = false;
            bool passwordChange = false;
            bool passwordCorrect = false;
            bool matching = true;

            if (!PasswordInput.Password.Equals(PasswordConfirmInput.Password)){
                PasswordConfirmErrorMessage.Visibility = Visibility.Visible;
                matching = false;
            }
            else
            {
                PasswordConfirmErrorMessage.Visibility = Visibility.Hidden;
            }

            if (EmailInput.Text.Length == 0 && PasswordInput.Password.Length == 0)
            {
                GlobalError.Visibility = Visibility.Visible;
            }
            else
            {
                GlobalError.Visibility = Visibility.Hidden;
                var user = new UserEntity()
                {
                    Email = ViewModel.InputEmail,
                    Password = ViewModel.InputPassword
                };
                var validationResult = _userValidator.ValidatorForUpdate(user);
                if (user.Email != null && user.Email.Length != 0)
                {
                    mailChange = true;
                    if (validationResult.TryGetValue(nameof(user.Email), out string emailMessage))
                    {
                        EmailErrorMessage.Content = emailMessage;
                        EmailErrorMessage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        EmailErrorMessage.Visibility = Visibility.Hidden;
                        mailCorrect = true;
                    }
                }

                if (user.Password != null && user.Password.Length != 0)
                {
                    passwordChange = true;
                    if (validationResult.TryGetValue(nameof(user.Password), out string passwordMessage))
                    {
                        PasswordErrorMessage.Text = passwordMessage;
                        PasswordErrorMessage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        PasswordErrorMessage.Visibility = Visibility.Hidden;
                        passwordCorrect = true;
                    }
                }

                if (validationResult.Count > 1 || !matching)
                {
                    return;
                }

                bool different = false;
                if (mailChange && mailCorrect)
                {

                    if (!user.Email.Equals(SessionManager.Instance.Current.User.Email))
                    {
                        different = true;
                        SessionManager.Instance.Current.User.Email = user.Email;
                    } else
                    {
                        mailChange = false;
                    }
                }
                if (passwordChange && passwordCorrect)
                {
                    user.Encrypt();
                    if (!user.Password.Equals(SessionManager.Instance.Current.User.Password))
                    {
                        different = true;
                        SessionManager.Instance.Current.User.Password = user.Password;
                    }
                }
                if (different)
                {
                    UserRepository repository = new UserRepository();
                    repository.Update(SessionManager.Instance.Current.User);
                    MessageBox.Show(((mailChange)? "Email" : "") + ((mailChange && passwordCorrect)? " en " : "") + ((passwordCorrect)? "Wachtwoord" : "") + ((mailChange && passwordCorrect) ? " zijn " : " is ") + "succesvol aangepast.");
                    _navigationManager.Navigate(() => new AccountView(_navigationManager));
                }
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            if (sender != CancelButton) return;
            //EmailInput.Clear();
            //EmailErrorMessage.Visibility = Visibility.Hidden;
            //PasswordInput.Clear();
            //PasswordErrorMessage.Visibility = Visibility.Hidden;
            //PasswordConfirmInput.Clear();
            //PasswordConfirmErrorMessage.Visibility = Visibility.Hidden;
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
