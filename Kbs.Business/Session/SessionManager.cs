using Kbs.Business.User;

namespace Kbs.Business.Session;

public class SessionManager
{
    private readonly IUserRepository _userRepository;
    private static SessionManager _instance;
    public static SessionManager Instance
    {
        get => _instance;
        set
        {
            if (_instance != null)
            {
                throw new InvalidOperationException("Already initialized session manager");
            }

            _instance = value;
        }
    }
    public Session Current { get; private set; }
    public SessionManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool TryCreate(UserEntity user, out Session session)
    {
        session = default;
        var userFromDb = _userRepository.GetByCredentials(user.Email, user.Password);
        if (userFromDb == null)
        {
            return false;
        }

        session = new Session(userFromDb);
        Current = session;
        return true;
    }
}