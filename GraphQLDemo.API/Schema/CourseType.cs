namespace GraphQLDemo.API.Schema
{
    public enum Subject
    {
        Math,
        Science,
        History
    }

    /// <summary>
    /// Domain entity
    /// </summary>
    public class CourseType
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }       
        public Subject Subject { get; set; }
        [GraphQLNonNullType] // If you want to make the Instructor to be not nullable and want to throw an error if it is null
        public required InstructorType Instructor { get; set; }
        public IEnumerable<StudentType>? Students { get; set; }
    }
}
