using System;

namespace SharedLibrary.DTO.Student;

public class Request
{
    public Guid GID { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? EmailAddress { get; set; }
    public string? StudentNumber { get; set; }
    public string? ContactNumber { get; set; }
    public string? Password { get; set; }
    public bool FirstTimeLogin { get; set; }
}
