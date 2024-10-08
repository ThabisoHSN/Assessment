using System;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<DAO.Student> Students{ get; set; }
    public DbSet<DAO.Course> Courses{ get; set; }
    public DbSet<DAO.StudentCourses> StudentCourses{ get; set; }
}
