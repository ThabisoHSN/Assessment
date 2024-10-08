namespace Backend.Data
{
    public class DataSeeder
    {
        public static void Seed(ApplicationContext context)
        {
            if (context.Courses.Any())
            {
                return;
            }


            context.Courses.AddRange(
                 new DAO.Course { CourseName = "Mathematics", Description = "Study of numbers, quantities, and shapes.", CourseCode = "MA18983" },
                 new DAO.Course { CourseName = "Information Technology", Description = "Study of computer systems and networks.", CourseCode = "IT1234567" },
                 new DAO.Course { CourseName = "Physics", Description = "Study of matter, energy, and the fundamental forces of nature.", CourseCode = "PH145678" },
                 new DAO.Course { CourseName = "Chemistry", Description = "Study of substances and their interactions.", CourseCode = "CH234567" },
                 new DAO.Course { CourseName = "Biology", Description = "Study of living organisms and life processes.", CourseCode = "BI345678" },
                 new DAO.Course { CourseName = "English Literature", Description = "Study of written works in the English language.", CourseCode = "EL567899" },
                 new DAO.Course { CourseName = "History", Description = "Study of past events and societies.", CourseCode = "HI345678" },
                 new DAO.Course { CourseName = "Political Science", Description = "Study of government systems and political activities.", CourseCode = "PS456789" },
                 new DAO.Course { CourseName = "Economics", Description = "Study of production, consumption, and transfer of wealth.", CourseCode = "EC678902" },
                 new DAO.Course { CourseName = "Business Administration", Description = "Study of managing businesses and organizations.", CourseCode = "BA987654" },
                 new DAO.Course { CourseName = "Psychology", Description = "Study of the mind and behavior.", CourseCode = "PY098765" },
                 new DAO.Course { CourseName = "Sociology", Description = "Study of society and social behavior.", CourseCode = "SO876543" },
                 new DAO.Course { CourseName = "Mechanical Engineering", Description = "Study of designing and manufacturing mechanical systems.", CourseCode = "ME234567" },
                 new DAO.Course { CourseName = "Electrical Engineering", Description = "Study of electrical systems and circuits.", CourseCode = "EE345678" },
                 new DAO.Course { CourseName = "Civil Engineering", Description = "Study of designing and constructing infrastructure.", CourseCode = "CE456789" },
                 new DAO.Course { CourseName = "Software Engineering", Description = "Study of designing and developing software applications.", CourseCode = "SE987654" },
                 new DAO.Course { CourseName = "Environmental Science", Description = "Study of the environment and solutions to environmental problems.", CourseCode = "ES765432" },
                 new DAO.Course { CourseName = "Computer Science", Description = "Study of computer systems and computational processes.", CourseCode = "CS123456" },
                 new DAO.Course { CourseName = "Philosophy", Description = "Study of fundamental questions about existence, knowledge, and ethics.", CourseCode = "PH456789" },
                 new DAO.Course { CourseName = "Marketing", Description = "Study of promoting and selling products or services.", CourseCode = "MK234567" }
             );

            context.SaveChanges();
        }
    }
}
