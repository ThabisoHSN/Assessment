using Backend.Data;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO.Course;
using System;

namespace Backend.Services.Course.Implementation;

public class CourseManagementService : ICourseManagementService
{
    private readonly ApplicationContext _context;

    public CourseManagementService(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<RegistrationResponse> CourseDeRegistrationManagement(Request request)
    {
        try
        {
            var (courseGID, studentGID) = await getDataAsync(request.StudentNumber, request.CourseCode);

            var registeredCourse = await _context.StudentCourses.AsNoTracking()
                                .FirstOrDefaultAsync(f => f.CourseGID == courseGID && f.StudentGID == studentGID);

            _context.StudentCourses.Remove(registeredCourse);

            await _context.SaveChangesAsync();

            return new RegistrationResponse() { IsRegistered = false };
        }
        catch (Exception ex) { 
            return new RegistrationResponse() { Error = ex.Message}; 
        }
    }

    public async Task<RegistrationResponse> CourseRegistrationManagement(Request request)
    {
        try
        {
            var (courseGID, studentGID) = await getDataAsync(request.StudentNumber, request.CourseCode);

            if (await _context.StudentCourses.AnyAsync(c => c.CourseGID == courseGID && c.StudentGID == studentGID))
            {
                return new RegistrationResponse() {Error = "Already registered for this course" };
            } 

            var registeredCourse = new DAO.StudentCourses
            {
                StudentGID = studentGID,
                CourseGID = courseGID,
                RegisteredDate = DateTime.Now
            };

            _context.StudentCourses.Add(registeredCourse);

            await _context.SaveChangesAsync();

            return new RegistrationResponse() { IsRegistered = true };
        }
        catch (Exception ex)
        {
            return new RegistrationResponse() { Error = ex.Message };
        }
    }

    private async Task<(Guid courseGID, Guid studentGID)> getDataAsync(string StudentNumber, string CourseCode)
    {
        var student = await _context.Students.FirstOrDefaultAsync(f => f.StudentNumber == StudentNumber);
        var course = await _context.Courses.FirstOrDefaultAsync(f => f.CourseCode == CourseCode);

        return (course.GID, student.GID);

    }
}
