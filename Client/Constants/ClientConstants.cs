using System;

namespace Client.Constants;

public class ClientConstants
{
    public class CourseService{
        public const string CourseRegister = "/api/course/register";
        public const string CourseDeregister = "/api/course/deregister";
        public const string CoursesEnrolled = "/api/course/enrolled";
        public const string CoursesAvailable = "/api/course/available";
        public const string FindCourse = "/api/course/getcourse";
    }

    public class StudentService{
        public const string StudentCreate = "/create";
        public const string StudentUpdate = "/update";
        public const string StudentDelete = "/delete";
    }

    public class AccountService{
        public const string AccountRegister = "/api/account/register";
        public const string AccountLogin = "/api/account/signin";
        public const string AccountPasswordUpdate  = "/updatepassword";
    }
}
