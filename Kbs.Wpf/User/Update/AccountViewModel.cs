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

        public string InputEmail { get => _inputEmail; set => SetField(ref _inputEmail, value); }
        public string InputPassword { get => _inputPassword; set => _inputPassword = value; }
        public string InputConfirmPassword { get => _inputConfirmPassword; set => _inputConfirmPassword = value; }
    }
}
