using System;

namespace Backend.Services.Student;

public interface IStudentReadService
{
    Task<IEnumerable<Backend.DAO.Student>> GetStudents();
    Task<SharedLibrary.DTO.Student.Response> GetStudentByEmailOrStudentNumber(string Value);
    Task<SharedLibrary.DTO.Student.Response> GetStudentByGID(Guid studentGID);
}
