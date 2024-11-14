using Kbs.Business.User;

namespace Kbs.Business.Session;

public class Session
{
    public Session(UserEntity user)
    {
        User = user;
    }
    public UserEntity User { get; }
}