using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Boxes.Frontend.AuthenticationProviders
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(3000);
            var anonimous = new ClaimsIdentity();
            var user = new ClaimsIdentity(authenticationType: "test");
            var admin = new ClaimsIdentity(new List<Claim>
            {
                new Claim("Name:", "Tomas"),
                new Claim("LastName", "Arias"),
                new Claim(ClaimTypes.Name, "tomasariasuribe302@gmail.com"),
                new Claim(ClaimTypes.Role, "Admin")
             },
                authenticationType: "test");

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(admin)));
            /*return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonimous)));*/ // Anonimous user
        }
    }
}