using System.Security.Claims;
using Kbs.Business.Session;
using Kbs.Business.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Kbs.Web;

public class SessionManagerAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly NavigationManager _navigationManager;
    private readonly SessionManager _sessionManager;
    public SessionManagerAuthenticationStateProvider(NavigationManager navigationManager, SessionManager sessionManager)
    {
        _navigationManager = navigationManager;
        _sessionManager = sessionManager;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_sessionManager.Current == null)
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        }
        
        string name = _sessionManager.Current.User.Name;

        if (string.IsNullOrWhiteSpace(name))
        {
            name = _sessionManager.Current.User.Email;
        }

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Role, _sessionManager.Current.User.Role.ToDutchString()),
            new Claim(ClaimTypes.NameIdentifier, _sessionManager.Current.User.UserId.ToString()),
            new Claim(ClaimTypes.Email, _sessionManager.Current.User.Email),
        }, "SessionManager_ManagedAuthentication");

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }
    
    private void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void Logout()
    {
        _sessionManager.Logout();
        NotifyAuthenticationStateChanged();
        _navigationManager.NavigateTo("/");
    }

    public bool TryLogin(UserEntity user, out Session session)
    {
        if (_sessionManager.TryCreate(user, out session))
        {
            NotifyAuthenticationStateChanged();
            return true;
        }
        return false;
    }
    public Session Session => _sessionManager.Current;
}