using Kbs.Business.Helpers;

namespace Kbs.Business.Session;

public class SessionTimeExpiredEventArgs
{
    public SessionTimeExpiredEventArgs(Session session)
    {
        ThrowHelper.ThrowIfNull(session);
        Session = session;
    }

    public Session Session { get; }
}