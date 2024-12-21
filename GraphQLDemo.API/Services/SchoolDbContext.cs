using GraphQLDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Services
{
    public class SchoolDbContext(DbContextOptions<SchoolDbContext> options) : DbContext(options)
    {
        public DbSet<CourseDTO> Cources { get; set; }
        public DbSet<StudentDTO> Students { get; set; }
        public DbSet<InstructorDTO> Instructors { get; set; }
    }
}
