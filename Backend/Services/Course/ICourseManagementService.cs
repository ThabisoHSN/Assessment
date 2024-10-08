using System;

namespace Backend.Services.Course;

public interface ICourseManagementService
{
    Task<SharedLibrary.DTO.Course.RegistrationResponse> CourseDeRegistrationManagement(SharedLibrary.DTO.Course.Request request);
    Task<SharedLibrary.DTO.Course.RegistrationResponse> CourseRegistrationManagement(SharedLibrary.DTO.Course.Request request);

}
