using System;

namespace SharedLibrary.DTO.Course;

public record Response
{
    public string CourseCode {get;set;}
    public string CourseName {get;set;}
    public string CourseDescription {get;set; }
    public int ClassMembers {get;set;}
    public SharedLibrary.DTO.Student.Response[]? Students {get;set;}
    public DateTime? RegisteredDate {get;set;}
    public string Error { get; set;}

}
