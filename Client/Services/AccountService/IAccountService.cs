using System;

namespace Client.Services.AccountService;

public interface IAccountService
{
    Task<SharedLibrary.DTO.Account.SignIn.Response> Sign(SharedLibrary.DTO.Account.SignIn.Request request);
    Task<SharedLibrary.DTO.Account.SignIn.Response> Register(SharedLibrary.DTO.Account.Request request);
}
