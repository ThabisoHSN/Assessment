using System;

namespace SharedLibrary.DTO.Course;

public class Request
{
    public string StudentNumber {get; set;}
    public string CourseCode { get; set;}
    public bool IsRegistered { get; set;}

}
