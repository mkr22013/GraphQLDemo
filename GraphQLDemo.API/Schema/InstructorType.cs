namespace GraphQLDemo.API.Schema
{
    /// <summary>
    /// Domain entity for Instructor
    /// </summary>
    public class InstructorType
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public required double Salary { get; set; }
    }
}
