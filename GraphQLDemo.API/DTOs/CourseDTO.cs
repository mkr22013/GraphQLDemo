using GraphQLDemo.API.Models;
using System.ComponentModel.DataAnnotations;

namespace GraphQLDemo.API.DTOs
{
    public class CourseDTO
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Subject Subject { get; set; }

        /// <summary>
        /// Foreign Key 
        /// </summary>
        public Guid InstructorId { get; set; }
        [Required]
        public InstructorDTO Instructor { get; set; }
        public IEnumerable<StudentDTO>? Students { get; set; }
    }
}
