using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.DAO;

public class Student
{
    [Key]
    public Guid GID { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? EmailAddress { get; set; }
    public string? StudentNumber { get; set; }
    public string? ContactNumber {get;set;}
    public string? Password { get; set; }
    public bool FirstTimeLogin {get; set;}
    
}
