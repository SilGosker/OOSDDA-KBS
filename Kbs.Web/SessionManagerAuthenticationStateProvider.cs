using System.Security.Claims;
using Kbs.Business.Session;
using Kbs.Business.User;
using Microsoft.AspNetCore.Components.Authorization;

namespace Kbs.Web;

public class SessionManagerAuthenticationStateProvider : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (SessionManager.Instance.Current == null)
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        }
        
        string name = SessionManager.Instance.Current.User.Name;

        if (string.IsNullOrWhiteSpace(name))
        {
            name = SessionManager.Instance.Current.User.Email;
        }

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Role, SessionManager.Instance.Current.User.Role.ToDutchString()),
            new Claim(ClaimTypes.NameIdentifier, SessionManager.Instance.Current.User.UserId.ToString()),
            new Claim(ClaimTypes.Email, SessionManager.Instance.Current.User.Email),
        }, "SessionManager_ManagedAuthentication");

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}