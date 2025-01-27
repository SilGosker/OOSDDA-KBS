﻿using System.Collections.ObjectModel;
using Kbs.Business.Helpers;
using Kbs.Business.User;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.User.Read.Details
{
    public class ReadDetailsUserViewModel : ViewModel
    {
        private string _name;
        private string _role;
        private string _email;
        private int _userId;

        public ObservableCollection<ReadDetailsUserReservationViewModel> Reservations { get; } = new();
        public ObservableCollection<ReadDetailsUserMedalViewModel> Medals { get; } = new();

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
            set
            {
                SetField(ref _userId, value);
                OnPropertyChanged(nameof(TitleLabel));
            }
        }

        public string TitleLabel => "Lid #" + UserId;
        

        public ReadDetailsUserViewModel(UserEntity user)
        {
            ThrowHelper.ThrowIfNull(user);
            Name = user.Name;
            UserId = user.UserId;
            Role = user.Role.ToDutchString() ?? Role;
            Email = user.Email;
        }

        public ReadDetailsUserViewModel() { }
    }
}
