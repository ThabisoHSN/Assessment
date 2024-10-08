using System;
using Blazored.SessionStorage;
using Blazored.Toast;
using Client.Services;
using Client.Services.AccountService;
using Client.Services.CourseService;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Utilities;

public static class ServiceExtensions
{
    public static IServiceCollection AddExtensions(this IServiceCollection services)
    {

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICourseManagement, CourseManagement>();
        services.AddBlazoredSessionStorage();
        services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
        services.AddAuthorizationCore();
        services.AddScoped<CustomHttpHandler>();
        services.AddBlazoredToast();

        return services;
    }
}
