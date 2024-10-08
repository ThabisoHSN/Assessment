using System;
using Backend.Services.Account;
using Backend.Services.Account.Implementation;
using Backend.Services.Course;
using Backend.Services.Course.Implementation;
using Backend.Services.Student;
using Backend.Services.Student.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Backend.Utility;

public static class SeviceExtension
{
   public static IServiceCollection AddExtension(this IServiceCollection services){
    
    services.AddScoped<IStudentReadService, StudentReadService>();
    services.AddScoped<IStudentCreateService, StudentCreateService>();
    services.AddScoped<IStudentManagementService, StudentManagementService>();
    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<ICourseReadService, CourseReadService>();
    services.AddScoped<ICourseManagementService, CourseManagementService>();
   

    return services;
   }
}
