﻿using Kbs.Business.Helpers;
using Kbs.Business.User;
using Kbs.Wpf.Components;
using System.Collections.ObjectModel;

namespace Kbs.Wpf.User.ReadUser.ReadUserDetail
{
    public class ReadUserDetailedViewModel : ViewModel
    {
        private string _name;
        private string _role;
        private string _email;
        private int _userId;

        public ObservableCollection<ReadUserDetailedReservationViewModel> Reservations { get; } = new();

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public string Role
        {
            get => _role;
            set => SetField(ref _role, value);
        }

        public string Email
        {
            get => _email;
            set => SetField(ref _email, value);
        }

        public int UserId
        {
            get => _userId;
            set => SetField(ref _userId, value);
        }

        public ReadUserDetailedViewModel(UserEntity user)
        {
            ThrowHelper.ThrowIfNull(user);
            Name = user.Name;
            UserId = user.UserId;
            Role = user.Role.ToDutchString() ?? Role;
            Email = user.Email;
        }

        public ReadUserDetailedViewModel() { }
    }
}