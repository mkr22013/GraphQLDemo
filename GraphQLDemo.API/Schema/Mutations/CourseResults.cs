using GraphQLDemo.API.Models;

namespace GraphQLDemo.API.Schema.Mutations
{
    public class CourseResults
    {
        public Guid Id { get; set; }
        public Subject Subject { get; set; }
        public required string Name { get; set; }
        public Guid InstructorId { get; set; }
    }
}
