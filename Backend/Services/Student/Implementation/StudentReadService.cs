using System;
using Backend.Data;
using Backend.Helper;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Student.Implementation;

public class StudentReadService : IStudentReadService
{
    private readonly ApplicationContext _context;

    public StudentReadService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Backend.DAO.Student>> GetStudents()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<SharedLibrary.DTO.Student.Response> GetStudentByEmailOrStudentNumber(string Value)
    {
        var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(student => student.EmailAddress == Value || student.StudentNumber == Value);
        
        if(student == null)
        {
            return null;
        }
        
        return GlobalHelper.MapperFields(student, new SharedLibrary.DTO.Student.Response());
    }

    public async Task<SharedLibrary.DTO.Student.Response> GetStudentByGID(Guid studentGID)
    {
        var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(student => student.GID == studentGID);

        if (student == null)
        {
            return null;
        }

        return GlobalHelper.MapperFields(student, new SharedLibrary.DTO.Student.Response());
    }
}
