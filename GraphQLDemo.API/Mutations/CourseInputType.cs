using GraphQLDemo.API.Models;

namespace GraphQLDemo.API.Mutations
{
    public class CourseInputType
    {
        public required string Name { get; set; }
        public Subject Subject { get; set; }
        public Guid InstructorId { get; set; }
    }
}
