using System;
using System.Net.Http.Json;
using System.Text.Json;
using Client.Constants;
using SharedLibrary.DTO.Account;

namespace Client.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly IHttpClientFactory httpClient;
    private readonly HttpClient _http;

    public AccountService(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory;
        _http = httpClient.CreateClient("AssessmentApi");
    }

    public async Task<SharedLibrary.DTO.Account.SignIn.Response> Register(Request request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync(ClientConstants.AccountService.AccountRegister, request);

            if (!response.IsSuccessStatusCode)
            {
                return new SharedLibrary.DTO.Account.SignIn.Response()
                {
                    Error = "Failed to register"
                };
            }

            var result = await response.Content.ReadFromJsonAsync<SharedLibrary.DTO.Account.SignIn.Response>();

            return result;

        }
        catch (Exception ex)
        {
            return new SharedLibrary.DTO.Account.SignIn.Response()
            {
                Error = ex.Message,
            };
        }
    }

    public async Task<SharedLibrary.DTO.Account.SignIn.Response> Sign(SharedLibrary.DTO.Account.SignIn.Request request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync<SharedLibrary.DTO.Account.SignIn.Request>(ClientConstants.AccountService.AccountLogin, request);

            if (!response.IsSuccessStatusCode)
            {
                return new SharedLibrary.DTO.Account.SignIn.Response()
                {
                    Error = "Failed to sign in"
                };
            }

            var result = await response.Content.ReadFromJsonAsync<SharedLibrary.DTO.Account.SignIn.Response>();

            return result;

        }
        catch (Exception ex)
        {
            return new SharedLibrary.DTO.Account.SignIn.Response()
            {
                Error = ex.Message,
            };
        }
    }
}
