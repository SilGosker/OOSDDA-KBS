using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.User.Update
{
    public class AccountViewModel : ViewModel
    {
        private string _inputEmail;
        private string _inputPassword;
        private string _inputConfirmPassword;
        private string _emailErrorMessage;
        private string _passwordErrorMessage;

        public string InputEmail { get => _inputEmail; set => SetField(ref _inputEmail, value); }
        public string InputPassword { get => _inputPassword; set => SetField(ref _inputPassword, value); }
        public string InputConfirmPassword { get => _inputConfirmPassword; set => SetField(ref _inputConfirmPassword, value); }
        public string EmailErrorMessage { get => _emailErrorMessage; set => SetField(ref _emailErrorMessage, value); }
        public string PasswordErrorMessage { get => _passwordErrorMessage; set => SetField(ref _passwordErrorMessage, value); }
    }
}
