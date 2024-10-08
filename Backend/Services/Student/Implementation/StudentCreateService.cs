using System;
using Backend.Data;
using Backend.Helper;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO.Student;


namespace Backend.Services.Student.Implementation;

public class StudentCreateService : IStudentCreateService
{
    private readonly ApplicationContext _context;

    public StudentCreateService(ApplicationContext context)
    {
        _context = context;
    }


    public async Task<bool> CreateStudent(Request request)
    {
        var student = GlobalHelper.MapperFields(request, new DAO.Student());
        
        student.StudentNumber = await GenerateStudentNumber();

        _context.Add(student);

        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<string> GenerateStudentNumber()
    {
        var highest = await _context.Students.OrderByDescending(c => c.StudentNumber).Select(s => s.StudentNumber).FirstOrDefaultAsync();
        var year = DateTime.Now.Year;
        if(highest == null)
        {
            return (year + (1).ToString("D5"));
        }

        var highestNumber = Int32.Parse(highest.Substring(4));
        highestNumber++;

        return year + highestNumber.ToString("D5");


    }
}
