using System;

namespace Backend.Services.Course;

public interface ICourseReadService
{
    Task<SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>> EnrolledCourses(string studentNumber);
    Task<SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>> AvailableCourses(string studentNumber);
    Task<SharedLibrary.DTO.Course.Response> GetCourse(string CourseCode);
}
