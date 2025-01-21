using System.ComponentModel.DataAnnotations;

namespace GraphQLDemo.API.DTOs
{
    public class InstructorDTO
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public double Salary { get; set; }

        /// <summary>
        /// Navigation Property
        /// </summary>
        public IEnumerable<CourseDTO> Courses { get; set; }
    }
}
