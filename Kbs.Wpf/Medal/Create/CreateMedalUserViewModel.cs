using Kbs.Wpf.Components;

namespace Kbs.Wpf.Medal.Create
{
    public class CreateMedalUserViewModel : ViewModel
    {
        private int _userId;
        private string _displayName;

        public CreateMedalUserViewModel(Business.User.UserEntity user)
        {
            UserId = user.UserId;
            if (user.Name == "")
            {
                DisplayName = user.Email;
            }
            else
            {
                DisplayName = user.Name;
            }
        }
        public int UserId
        {
            get => _userId;
            set => SetField(ref _userId, value);
        }

        public string DisplayName
        {
            get => _displayName;
            set => SetField(ref _displayName, value);
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
