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
            EmailErrorMessage.Content = "";
            PasswordErrorMessage.Text = "";
            var user = new UserEntity()
            {
                Email = EmailInput.Text,
                Password = PasswordInput.Password
            };
            var validationResult = _userValidator.ValidatorForUpdate(user, PasswordConfirmInput.Password, _userRepository);

            if (validationResult.TryGetValue(nameof(user.Email), out string emailMessage))
            {
                if (!emailMessage.Contains("verplicht") && !emailMessage.Contains("bestaat"))
                {
                    EmailErrorMessage.Content = emailMessage;
                }
            }
            if (validationResult.TryGetValue(nameof(user.Password), out string passwordMessage))
            {
                if (!passwordMessage.Contains("verplicht"))
                {
                    PasswordErrorMessage.Text = passwordMessage;
                }
            }

            if (validationResult.Count > 1)
            {
                if ((emailMessage.Contains("verplicht") || emailMessage.Contains("bestaat")) && passwordMessage.Contains("verplicht"))
                {
                    MessageBox.Show("Email en Wachtwoord zijn succesvol aangepast.");
                    _navigationManager.Navigate(_navigationTarget);
                }
                return;
            }

            // status (true = valid value) is allowed to get updated in the system
            bool emailStatus = !validationResult.ContainsKey(nameof(user.Email));
            bool passwordStatus = !validationResult.ContainsKey(nameof(user.Password));
            bool isSessionUserUpdated = SessionManager.Instance.UpdateSessionUser(((emailStatus) ? user.Email : null), ((passwordStatus) ? user.Password : null));

            if (isSessionUserUpdated)
            {
                _userRepository.Update(_sessionUser);
                MessageBox.Show(((emailStatus) ? "Email" : "") + ((emailStatus && passwordStatus) ? " en " : "") + ((passwordStatus) ? "Wachtwoord" : "") + ((emailStatus && passwordStatus) ? " zijn " : " is ") + "succesvol aangepast.");
                _navigationManager.Navigate(_navigationTarget);
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Wijzigingen geannuleerd.");
            _navigationManager.Navigate(_navigationTarget);
        }
    }
}
