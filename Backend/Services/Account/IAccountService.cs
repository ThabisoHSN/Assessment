using System;
using Backend.Services.Student;

namespace Backend.Services.Account;

public interface IAccountService
{
    Task<SharedLibrary.DTO.Account.SignIn.Response> RegisterStudent(SharedLibrary.DTO.Account.Request request);
    Task<SharedLibrary.DTO.Account.SignIn.Response> SignIn(SharedLibrary.DTO.Account.SignIn.Request request);
}
