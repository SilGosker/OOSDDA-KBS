using Kbs.Business.Helpers;
using Kbs.Business.User;
using System.Threading;

namespace Kbs.Business.Session;

public class SessionManager
{
    private readonly IUserRepository _userRepository;
    private readonly TimeSpan _sessionTime;
    private CancellationTokenSource _cancellationTokenSource = new();
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
    public event EventHandler<SessionTimeExpiredEventArgs> SessionTimeExpired;
    public SessionManager(IUserRepository userRepository, TimeSpan sessionTime)
    {
        ThrowHelper.ThrowIfNull(userRepository);
        _userRepository = userRepository;
        _sessionTime = sessionTime;
    }

    public bool TryCreate(UserEntity user, out Session session)
    {
        session =  default;

        var userFromDb = _userRepository.GetByCredentials(user.Email, user.Password);
        if (userFromDb == null)
        {
            return false;
        }

        session = new Session(userFromDb);
        Current = session;
        TrackSessionTime();
        return true;
    }

    private async void TrackSessionTime()
    {
        try
        {
            var current = Current;
            await Task.Delay(_sessionTime, _cancellationTokenSource.Token);
            if (Current != null && current == Current)
            {
                SessionTimeExpired?.Invoke(this, new SessionTimeExpiredEventArgs(Current));
            }
        }
        catch
        {
            // ignored
        }
    }

    public void ExtendSession()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new();
        TrackSessionTime();
    }

    public void Logout()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new();
        Current = null;
    }
}