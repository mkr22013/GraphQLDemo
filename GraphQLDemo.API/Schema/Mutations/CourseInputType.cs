namespace GraphQLDemo.API.Schema.Mutations
{
    public class CourseInputType
    {
        public required string Name { get; set; }
        public required string Subject { get; set; }
        public required string InstructorId { get; set; }
    }
}
