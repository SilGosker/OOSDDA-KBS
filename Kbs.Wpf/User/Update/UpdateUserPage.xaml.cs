﻿using System.Windows;
using System.Windows.Controls;
using Kbs.Business.User;
using Kbs.Business.Session;
using Kbs.Data.User;
using Kbs.Wpf.Reservation.Read.Index;

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
            bool emailUpdated = !string.IsNullOrEmpty(user.Email) && !user.Email.Equals(sessionUser.Email);
            bool passwordUpdated = !string.IsNullOrEmpty(user.Password) && !user.Password.Equals(sessionUser.Password);
            bool nameUpdated = !user.Name.Equals(sessionUser.Name);

            string successMessage = "Er zijn geen aanpassingen gemaakt.";
               
            if (emailUpdated || passwordUpdated || nameUpdated)
            {
                successMessage = "Gegevens zijn aangepast.";
                SessionManager.Instance.UpdateSessionUser(((emailUpdated) ? user.Email : null), ((passwordUpdated) ? user.Password : null), user.Name);
                _userRepository.Update(sessionUser);
            }
            MessageBox.Show(successMessage);
            _navigationManager.Navigate(() => new ReadIndexReservationPage(_navigationManager));
            return;
        }
    }
    private void Cancel(object sender, RoutedEventArgs e)
    {
        _navigationManager.Navigate(() => new ReadIndexReservationPage(_navigationManager));
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