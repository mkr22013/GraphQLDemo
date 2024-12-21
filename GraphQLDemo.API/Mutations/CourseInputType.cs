using GraphQLDemo.API.Schema;

namespace GraphQLDemo.API.MutationResolver
{
    public class CourseInputType
    {
        public string Name { get; set; }
        public Subject Subject { get; set; }
        public Guid InstructorId { get; set; }
    }
}
