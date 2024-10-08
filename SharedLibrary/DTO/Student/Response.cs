using System;

namespace SharedLibrary.DTO.Student;

public class Response
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? EmailAddress { get; set; }
    public string? StudentNumber { get; set; }
    public string? ContactNumber { get; set; }
    public string? Password { get; set; }
    public string? Error { get; set; }
}
