using GraphQLDemo.API.Schema;

namespace GraphQLDemo.API.Mutations
{
    public class CourseResults
    {
        public Guid Id { get; set; }
        public Subject Subject { get; set; }
        public required string Name { get; set; }
        public Guid InstructorId { get; set; }
    }
}
