using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Microsoft.JSInterop;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;
using System.Text.Json;
using Blazored.LocalStorage;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    private readonly IJSRuntime _jsRuntime;
    private readonly ILocalStorageService _localStorageService;

    public CustomAuthenticationStateProvider(HttpClient httpClient, NavigationManager navigationManager, IJSRuntime jsRuntime, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _jsRuntime = jsRuntime;
        _localStorageService = localStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string authToken = await _localStorageService.GetItemAsStringAsync("authToken");

        var identity = new ClaimsIdentity();
        _httpClient.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrEmpty(authToken))
        {
            try
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));
            }
            catch
            {
                await RemoveRefreshTokenAsync();
                identity = new ClaimsIdentity();
            }
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }
    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer
            .Deserialize<Dictionary<string, object>>(jsonBytes);

        var claims = keyValuePairs.Select(kvp =>
            new Claim(kvp.Key, kvp.Value.ToString()));

        // Assuming roles are included in the JWT as a comma-separated string
        if (keyValuePairs.ContainsKey("roles"))
        {
            var roles = keyValuePairs["roles"].ToString().Split(',');
            claims = claims.Concat(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        }

        return claims;
    }
    private async Task RemoveRefreshTokenAsync()
    {
        try
        {
            await _jsRuntime.InvokeAsync<object>("removeCookie", "authToken");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error removing refresh token cookie: {ex.Message}");
        }
    }
    private async Task<string> GetTokenByRefreshTokenAsync()
    {
        try
        {
            string authToken = await _localStorageService.GetItemAsStringAsync("authToken");

            var response = await _httpClient.PostAsJsonAsync<string>("https://testmongo.bdjobs.com/test_redwan/api/Auth/refresh-token", authToken);
            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadAsStringAsync();
                return tokenResponse; // Assuming the response contains the new access token
            }
            else
            {
                // Log the error status code and reason
                Console.WriteLine($"Refresh token request failed with status code: {response.StatusCode}, reason: {response.ReasonPhrase}");
                return null;
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            Console.WriteLine($"Error retrieving token: {ex.Message}");
            return null;
        }
    }
}
