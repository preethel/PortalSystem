using Microsoft.AspNetCore.Components.Authorization;
using PortalSystem.Client.Shared;
using System.Net.Http.Json;

namespace PortalSystem.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }

        //public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
        //{
        //    var result = await _http.PostAsJsonAsync("api/auth/change-password", request.Password);
        //    return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        //}

        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("https://testmongo.bdjobs.com/test_redwan/api/Auth/login", request);
            if (result.IsSuccessStatusCode && result is not null)
            {
                return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            }
            else
            {
                return (new ServiceResponse<string>
                {
                    Data = "",
                    Success = false,
                    Message = "Login Failed, Try again."
                });
            }
            
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync("https://testmongo.bdjobs.com/test_redwan/api/Auth/register", request);
            if (result.IsSuccessStatusCode && result is not null)
            {
                return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            }
            else
            {
                return (new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Login Failed, Try again."
                });
            }
        }
    }
}
