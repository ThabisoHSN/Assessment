using Blazored.SessionStorage;
using Client.Constants;

namespace Client.Utilities
{
    public class CustomHttpHandler : DelegatingHandler
    {
        private ISessionStorageService _sessionStorage;

        public CustomHttpHandler(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.AbsolutePath.ToLower().Contains(ClientConstants.AccountService.AccountLogin) ||
                request.RequestUri.AbsolutePath.ToLower().Contains(ClientConstants.AccountService.AccountRegister))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            var token = await _sessionStorage.GetItemAsync<string>("Token");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Add("Authorization", $"Bearer {token}");

            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
