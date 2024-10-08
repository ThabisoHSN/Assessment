using System;
using Backend.Data;
using Backend.Helper;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO.Student;

namespace Backend.Services.Student.Implementation;

public class StudentManagementService : IStudentManagementService
{
    private readonly ApplicationContext _context;

    public StudentManagementService(ApplicationContext context){
        _context = context;
    }

    public async Task<bool> UpdateStudent(Request request)
    {
        _context.Attach(GlobalHelper.MapperFields(request, new Backend.DAO.Student())).State = EntityState.Modified;

        return await _context.SaveChangesAsync() > 0;
    }
}
