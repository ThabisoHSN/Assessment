using System;

namespace Backend.Services.Student;

public interface IStudentCreateService
{
    Task<bool> CreateStudent(SharedLibrary.DTO.Student.Request request);
}
