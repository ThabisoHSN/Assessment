using System;

namespace Client.Services.CourseService;

public interface ICourseManagement
{
    Task<SharedLibrary.DTO.Course.RegistrationResponse> CourseDeRegistrationManagement(SharedLibrary.DTO.Course.Request request);
    Task<SharedLibrary.DTO.Course.RegistrationResponse> CourseRegistrationManagement(SharedLibrary.DTO.Course.Request request);
    Task<SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>> EnrolledCourses(string StudentNumber);
    Task<SharedLibrary.DTO.Course.Response> FindCourse(string CourseCode);
    Task<SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>> AvailableCourses();
}
