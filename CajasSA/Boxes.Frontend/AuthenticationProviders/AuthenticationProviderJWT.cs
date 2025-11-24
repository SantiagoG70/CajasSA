using Boxes.Frontend.Helpers;
using Boxes.Frontend.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Boxes.Frontend.AuthenticationProviders;

public class AuthenticationProviderJWT : AuthenticationStateProvider, ILoginService
{
    private readonly IJSRuntime _jSRuntime;
    private readonly HttpClient _httpClient;
    private readonly string _tokenKey;
    private readonly AuthenticationState _anonimous;

    public AuthenticationProviderJWT(IJSRuntime jSRuntime, HttpClient httpClient)
    {
        _jSRuntime = jSRuntime;
        _httpClient = httpClient;
        _tokenKey = "TOKEN_KEY";
        _anonimous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token = null;

        try
        {
            token = await _jSRuntime.GetLocalStorage(_tokenKey) as string;
        }
        catch (InvalidOperationException)
        {
            // Estamos en prerender -> usuario anónimo
            return _anonimous;
        }

        if (string.IsNullOrWhiteSpace(token))
        {
            return _anonimous;
        }

        return BuildAuthenticationState(token);
    }

    private AuthenticationState BuildAuthenticationState(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", token);

        var claims = ParseClaimsFromJWT(token);

        return new AuthenticationState(
            new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"))
        );
    }

    private IEnumerable<Claim> ParseClaimsFromJWT(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var unToken = handler.ReadJwtToken(token);
        return unToken.Claims;
    }

    public async Task LoginAsync(string token)
    {
        await _jSRuntime.SetLocalStorage(_tokenKey, token);

        var authState = BuildAuthenticationState(token);

        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public async Task LogoutAsync()
    {
        await _jSRuntime.RemoveLocalStorage(_tokenKey);
        _httpClient.DefaultRequestHeaders.Authorization = null;

        NotifyAuthenticationStateChanged(Task.FromResult(_anonimous));
    }
}