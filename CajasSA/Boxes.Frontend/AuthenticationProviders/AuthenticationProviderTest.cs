using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Boxes.Frontend.AuthenticationProviders;

public class AuthenticationProviderTest : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        await Task.Delay(1000);
        var anonimous = new ClaimsIdentity();
        var empleado = new ClaimsIdentity(authenticationType: "test");
        var admin = new ClaimsIdentity(
            [
                new("FirstName", "Santiago"),
                new("LastName", "Gaviria"),
                new(ClaimTypes.Name, "Cosita@exaple.com"),
                new(ClaimTypes.Role, "Admin")
            ],
            authenticationType: "test");
        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(admin)));
    }
}