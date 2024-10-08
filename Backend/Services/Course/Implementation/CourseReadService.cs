using System;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO;
using SharedLibrary.DTO.Course;

namespace Backend.Services.Course.Implementation;

public class CourseReadService : ICourseReadService
{
    private readonly ApplicationContext _context;

    public CourseReadService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<DynamicListResponse<Response>> AvailableCourses()
    {
        try
        {
            var courses = await _context.Courses.AsNoTracking().ToListAsync();

            var results = courses.Select(s => new SharedLibrary.DTO.Course.Response
            {
                CourseDescription = s.Description,
                CourseName = s.CourseName,
                CourseCode = s.CourseCode,
                ClassMembers = _context.StudentCourses.Count(c => c.CourseGID == s.GID)
            }).ToList();


            return new DynamicListResponse<Response>() { Result = results };
        }
        catch (Exception ex)
        {
            return new DynamicListResponse<Response>() { Error = ex.Message };
        }
    }

    public async Task<DynamicListResponse<Response>> EnrolledCourses(string studentNumber)
    {
        try
        {
            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(f => f.StudentNumber == studentNumber);

            if (student == null)
            {
                return new DynamicListResponse<Response>() { Error = "Student not found" };
            }

            var results = await _context.StudentCourses
                                .Include(sc => sc.Course)
                                .Where(w => w.StudentGID == student.GID)
                                .GroupBy(sc => new { sc.Course.CourseCode, sc.Course.CourseName, sc.Course.Description, sc.RegisteredDate })
                                .Select(g => new SharedLibrary.DTO.Course.Response
                                {
                                    CourseCode = g.Key.CourseCode,
                                    CourseName = g.Key.CourseName,
                                    CourseDescription = g.Key.Description,
                                    ClassMembers = g.Count(),
                                    RegisteredDate = g.Key.RegisteredDate
                                })
                                .ToListAsync();

            return new DynamicListResponse<Response>() { Result = results };
        }
        catch (Exception ex)
        {
            return new DynamicListResponse<Response>() { Error = ex.Message };
        }
    }

    public async Task<Response> GetCourse(string CourseCode)
    {
        try
        {
            var course = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(f => f.CourseCode == CourseCode);

            if (course == null)
            {
                return new Response() { Error = "Course not found" };
            }

            var results = new Response()
            {
                CourseName = course.CourseName,
                CourseDescription = course.Description,
                CourseCode = course.CourseCode,
                ClassMembers = await _context.StudentCourses.CountAsync(c => c.CourseGID == course.GID)
            };

            return results;
        }
        catch (Exception ex)
        {
            return new Response() { Error = ex.Message };
        }
    }
}
