using Kbs.Business.Helpers;
using Kbs.Business.User;

namespace Kbs.Business.Session;

public class Session
{
    public Session(UserEntity user)
    {
        ThrowHelper.ThrowIfNull(user);
        User = user;
    }
    public UserEntity User { get; }
}