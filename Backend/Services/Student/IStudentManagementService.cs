using System;

namespace Backend.Services.Student;

public interface IStudentManagementService
{
    Task<bool> UpdateStudent(SharedLibrary.DTO.Student.Request request);
}
