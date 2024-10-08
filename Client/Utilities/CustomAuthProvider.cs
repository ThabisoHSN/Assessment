using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace Client.Utilities
{
    public class CustomAuthProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;

        public CustomAuthProvider(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwtToken = await _sessionStorage.GetItemAsync<string>("Token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return new AuthenticationState(
                    new ClaimsPrincipal(new ClaimsIdentity()));
            }
            return new AuthenticationState(new ClaimsPrincipal(

                new ClaimsIdentity(ParseClaimsFromJwt(jwtToken), "jwtAuth")
               ));
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwtToken)
        {

            var payload = jwtToken.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(k => new Claim(k.Key, k.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }

        public void NotifyAuthState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task SignOut()
        {
            await _sessionStorage.ClearAsync();
            var anonymousUse = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUse)));
        }
    }
}
